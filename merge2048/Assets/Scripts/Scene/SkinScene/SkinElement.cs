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
        
        // file exsits 체크
        if(path != "Fruits") return;

        var spriteName = $"Skins/{path}";
        var sprites = Resources.LoadAll<Sprite>(spriteName);
        image.sprite = sprites[idx];
    }
}
