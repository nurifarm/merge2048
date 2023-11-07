using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class SkinElement : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text number;
    

    public void Init(int idx, string path) {
        number.text = $"{Mathf.Pow(2, idx+1)}";
        image.sprite = GameDataManager.Instance.GetCircleSprite(path, idx);
return;
        if(path.Contains("Custom")) {
            // TODO: file read
            var filePath = Path.Combine(Application.persistentDataPath, path);
            filePath = $"{filePath}/{idx}.png";
            Debug.Log($"{filePath} try load");

             if (File.Exists(filePath)) {
                Debug.Log($"{filePath} is loaded");
                byte[] bytes = File.ReadAllBytes(filePath);
                Texture2D loadedTexture = new Texture2D(2, 2); // You can set the dimensions accordingly
                loadedTexture.LoadImage(bytes);
                // Use the loadedTexture as needed
                image.sprite = Sprite.Create(loadedTexture, new Rect(0, 0, GameDataManager.SpriteSize, GameDataManager.SpriteSize), new Vector2(0.5f, 0.5f));
            }
        } else {
            var spriteName = $"Skins/{path}/{idx+1}";
            var sprite = SpriteManager.Instance.GetSprite(spriteName);
            image.sprite = sprite;
        }
    }


    public void OnClickLoad() {
        
    }
}
