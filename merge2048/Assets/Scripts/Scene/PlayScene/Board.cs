using System;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public enum GameState {
        Ready,
        Falling,
        Play,
        Clear,
        Fail
    }
    
    [SerializeField] private Transform startLine;
    [SerializeField] private SpriteRenderer boardInner;

    public GameState State {
        get => state;
        private set {
            Debug.Log($"Set State {state} -> {value}");
            state = value;
        }
    }
    GameState state;

    public static Action<GameState> OnGameFinish;

    private const float lineY = 3.7f;
    private MergeCircle CurrentCircle = null;

    private List<MergeCircle> circleList = new List<MergeCircle>();

    void Start() {
        MergeCircle.OnCollision = Merge;
        var pos = startLine.transform.position;
        pos.y = lineY;
        startLine.transform.position = pos;
    }

    public void GameReady() {
        State = GameState.Ready;
    }

    public void GameStart() {
        State = GameState.Play;
    }

    public void GameEnd() {
        OnGameFinish?.Invoke(State);
    }

    public void ResetGame() {
        CurrentCircle?.Abandon();
        CurrentCircle = null;

        foreach(var circle in circleList) {
            circle.Abandon();
        }
        circleList.Clear();
    }

    public void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log($"{State} {IsStable()} {circleList.Count} {CurrentCircle == null} {PopupManager.Instance.IsAnyPopup} {IsTouchPointInsideBoard()} {startTouching}");
        }
        if(State != GameState.Play) return;
        // TODO: 클릭영역 확인후, 잘못된영역이면 return
        
        if(IsStable()) {
            CheckGameOver();
        }

        if(State == GameState.Play) {
            HandleInput();
        } else if(State == GameState.Fail) {
            GameEnd();
        } else if(State == GameState.Clear) {
            GameEnd();
        }
    }


    bool startTouching = false;

    private void HandleInput() {
        if(CurrentCircle == null) {
            CreateObject();
        }

        if(PopupManager.Instance.IsAnyPopup) return;

        if(Input.GetMouseButtonDown(0)) {
            if(IsTouchPointInsideBoard()) {
                startTouching = true;
            } else {
                startTouching = false;
            }
            return;
        }

        if(startTouching == false) return;

        if (Input.GetMouseButton(0)) {
            MoveObject();
        }

        if (Input.GetMouseButtonUp(0)) {
            // Vector3 inputPosition = Input.mousePosition;
            // inputPosition.z = Camera.main.transform.position.z;
            // var position = Camera.main.ScreenToWorldPoint(inputPosition);
            // RaycastHit2D hit = Physics2D.Raycast(position, transform.forward, 5);
            // Debug.Log($"1 {hit.collider == null}");
            // if(hit.collider == null) return;
            // Debug.Log($"2 {hit.collider.gameObject.layer != LayerMask.NameToLayer("board")}");
            // if(hit.collider.gameObject.layer != LayerMask.NameToLayer("board")) return;

            if(CanDrop()) Drop();
        }

        
    }

    // collider 충돌 판정필요없을거같아서 위치판정으로 함
    bool IsTouchPointInsideBoard() {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return boardInner.bounds.Contains(mouseWorldPos);
    }
    private void CheckGameOver() {
        foreach(var circle in circleList) {
            if(circle.IsUpper(lineY)) {
                State = GameState.Fail;
                Debug.Log("GAME OVER");
                return;
            }
        }

    }

    private bool IsStable() {
        foreach(var circle in circleList) {
            if(circle == null) return false;
            if(circle.IsStable() == false) return false;
        }
        return true;
    }

    private void CreateObject() {
        // TODO: pooling
        var prefab = Resources.Load<MergeCircle>("MergeCircle");
        CurrentCircle = Instantiate(prefab, transform) as MergeCircle;
        CurrentCircle.Create(UnityEngine.Random.Range(0, 5));
        CurrentCircle.transform.position = new Vector3(0f, startLine.position.y, 0f);
        CurrentCircle.OnCollisionFirst = TouchGround;
    }

    private void MoveObject() {
        Vector3 inputPosition = Input.mousePosition;
        inputPosition.z = Camera.main.transform.position.z;
        var position = Camera.main.ScreenToWorldPoint(inputPosition);
        position.y = startLine.position.y;
        position.z = 0f;
        position.x = Mathf.Clamp(position.x, -2f, 2f);
        CurrentCircle.transform.position = position;
    }

    private bool CanDrop() {
        if(IsTouchPointInsideBoard() == false) return false;
        return true;

        // 겹치면 못놓게 할지
        foreach(var circle in circleList) {
            // 하나라도 겹치면 drop 안함
            if(circle.IsOverlap(CurrentCircle)) {
                var i = UnityEngine.Random.Range(0,10);
                circle.name = $"H {i}";
                Debug.Log($"collision {i} {Vector2.Distance(circle.transform.position, CurrentCircle.transform.position)} {circle.Radius} {CurrentCircle.Radius}");
                return false;
            }
        }
        return true;
    }

    private void Drop() {

        circleList.Add(CurrentCircle);
        CurrentCircle.StartSimulate();
        CurrentCircle = null;

        State = GameState.Falling;

    }

    private void TouchGround() {
        if(State == GameState.Falling) {
            State = GameState.Play;
        }
    }

    private void Merge(MergeCircle circle1, MergeCircle circle2) {
        if(circle1.Index != circle2.Index) return;

        circle1.UpdateIndex(circle1.Index + 1);

        circleList.Remove(circle2);

        circle2.Abandon();

        // TODO: check clear
        if(circle1.Index == 9) {
            State = GameState.Clear;
        }
    }
}
