using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MergeCircle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private CircleCollider2D col;
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private TMP_Text txt;

    public System.Action OnCollisionFirst;
    public static System.Action<MergeCircle, MergeCircle> OnCollision;

    private const float ScaleFactor = 1.1f;
    private const float BaseRadius = 0.5f;
    public int Index {
        get => index;
        set {
            index = value;
        }
    }
    public float Radius => BaseRadius * Mathf.Pow(ScaleFactor, index+1);

    private int index;
    
    public void Create(int idx) {
        rig.simulated = false;
        UpdateIndex(idx);
    }

    public void UpdateIndex(int idx) {
        index = idx;
        txt.text = "";// $"{Mathf.Pow(2, idx+1)}";

        var spriteName = $"Skins/{UserDataManager.Instance.SelectedSkinPath}/{idx+1}";
        image.sprite = GameDataManager.Instance.GetCircleSprite(UserDataManager.Instance.SelectedSkinPath, idx);// SpriteManager.Instance.GetSprite(spriteName);
        SetSpriteSize();
        if(UserDataManager.Instance.SelectedSkinPath.Contains("Custom")) {
            image.transform.localScale *= 100;
        }
       // image.transform.localScale *= Mathf.Pow(ScaleFactor, idx+1);
        col.radius = Radius;
    }

    private void SetSpriteSize() {
         // 스프라이트의 원본 크기를 가져옵니다.
        float originalWidth = GameDataManager.SpriteSize;// image.sprite.bounds.size.x;

        // 원하는 너비와 원본 가로 크기 비교
        float scaleRatio = (Radius*2) / (originalWidth);

        // 스케일 값을 설정하여 원하는 너비로 조절
        image.transform.localScale = new Vector3(scaleRatio, scaleRatio, 1);
    }

    public void StartSimulate() {
        rig.simulated = true;
    }

    void OnCollisionEnter2D(Collision2D coll) {
        OnCollisionFirst?.Invoke();
        OnCollisionFirst = null;

        if(coll.gameObject.CompareTag("MergeObject") == false) return;

        var other = coll.gameObject.GetComponent<MergeCircle>();
        OnCollision.Invoke(this, other);
    }

    public bool IsStable() {
        return rig.angularVelocity <= 0.1f;
    }

    public void Abandon() {
        // TODO: pooling
        Destroy(gameObject);
    }

    public Color GetColor(int index)
    {
        // index 값에 따라 R, G, B 값을 변경
        float r = (index % 2) * 0.5f + 0.5f; // 1, 3, 5일 때 1.0 (빨강), 2, 4일 때 0.5
        float g = ((index + 1) % 2) * 0.5f; // 1, 3, 5일 때 0.0, 2, 4일 때 0.5 (초록)
        float b = (index / 5.0f);           // 1부터 5까지 0.2씩 증가 (파랑)

        return new Color(r, g, b);
    }

}
