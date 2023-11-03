using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinElement : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text number;
    

    public void Init(int idx, string path) {
        number.text = $"{Mathf.Pow(2, idx+1)}";

        var spriteName = $"Skins/{path}/{idx+1}";
        var sprite = SpriteManager.Instance.GetSprite(spriteName);
        image.sprite = sprite;
    }

    public void OnClickLoad() {
        
    }
}
