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
    private int index;
    
    public void Create(int idx) {
        rig.simulated = false;
        UpdateIndex(idx);
    }

    public void UpdateIndex(int idx) {
        index = idx;
        txt.text = $"{Mathf.Pow(2, idx+1)}";
        image.transform.localScale = Vector3.one * Mathf.Pow(ScaleFactor, idx+1);
        image.color = GetColor(idx);
        col.radius = BaseRadius * Mathf.Pow(ScaleFactor, idx+1);
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

    public Color GetColor(int index)
    {
        // index 값에 따라 R, G, B 값을 변경
        float r = (index % 2) * 0.5f + 0.5f; // 1, 3, 5일 때 1.0 (빨강), 2, 4일 때 0.5
        float g = ((index + 1) % 2) * 0.5f; // 1, 3, 5일 때 0.0, 2, 4일 때 0.5 (초록)
        float b = (index / 5.0f);           // 1부터 5까지 0.2씩 증가 (파랑)

        return new Color(r, g, b);
    }
}
