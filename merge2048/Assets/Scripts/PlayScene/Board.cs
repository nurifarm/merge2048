using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public enum GameState {
        Ready,
        Falling,
        Play,
        GameOver
    }
    
    [SerializeField] private Transform startLine;

    public GameState State {
        get => state;
        private set {
            state = value;
        }
    }
    GameState state;

    private MergeCircle CurrentCircle = null;

    private List<MergeCircle> circleList = new List<MergeCircle>();

    void Start() {
        MergeCircle.OnCollision += Merge;
    }

    public void GameReady() {
        State = GameState.Ready;
    }

    public void GameStart() {
        State = GameState.Play;
    }

    public void GameEnd() {
        State = GameState.GameOver;
    }

    public void Update() {
        if(State != GameState.Play) return;
        
        if(CurrentCircle == null) {
            CreateObject();
        }

        if (Input.GetMouseButton(0)) {
            MoveObject();
        }

        if (Input.GetMouseButtonUp(0)) {
            State = GameState.Falling;

            circleList.Add(CurrentCircle);
            CurrentCircle.StartSimulate();
            CurrentCircle = null;
        }
    }

    private void CreateObject() {
        // TODO: pooling
        var prefab = Resources.Load<MergeCircle>("MergeCircle");
        CurrentCircle = Instantiate(prefab, transform) as MergeCircle;
        CurrentCircle.Create(UnityEngine.Random.Range(0, 5));
        CurrentCircle.transform.position = new Vector3(0f, startLine.position.y, 0f);
        CurrentCircle.OnCollisionFirst = Falled;
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

    private void Falled() {
        State = GameState.Play;
    }

    private void Merge(MergeCircle circle1, MergeCircle circle2) {
        if(circle1.Index != circle2.Index) return;

        circle1.UpdateIndex(circle1.Index + 1);

        circleList.Remove(circle2);

        // TODO: pooling
        Destroy(circle2.gameObject);
    }
}
