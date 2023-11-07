using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class UserCreateSkinElement : MonoBehaviour
{
    [SerializeField] private RawImage rawImage; // 불러온 이미지를 보여줄 RawImage
    [SerializeField] private TMP_Text number;
    [SerializeField] private GameObject uploadText;

    Texture2D texture = null;
    

    public void Init(int idx) {
        number.text = $"{Mathf.Pow(2, idx+1)}";
        uploadText.SetActive(rawImage.texture == null);

        // var spriteName = $"Skins/{path}/{idx+1}";
        // var sprite = SpriteManager.Instance.GetSprite(spriteName);
        // image.sprite = sprite;
    }

    public byte[] Serialize() {
        if(texture != null) {
            var data = texture.EncodeToPNG();
            return data;
        }
        return null;
    }

      void RenderTextureSave()
     {
     	// RenderTexture.active = DrawTexture;
        // var texture2D = new Texture2D(DrawTexture.width, DrawTexture.height);
        // texture2D.ReadPixels(new Rect(0, 0, DrawTexture.width, DrawTexture.height), 0, 0);
        // texture2D.Apply();
        // var data = texture2D.EncodeToPNG();
        // File.WriteAllBytes("C:/Example/Image.png", data);
     }

    public void OnClickLoad() {
        GetImage();
    }

    public void GetImage () {
        NativeGallery.GetImageFromGallery((image) => 
        {
            FileInfo selectedImage = new FileInfo(image);
        
            if (!string.IsNullOrEmpty(image))
                StartCoroutine(LoadImage(image));

        });
    }

    //이미지 로드 코루틴            
    IEnumerator LoadImage(string imagePath)
    {
        byte[] imageData = File.ReadAllBytes(imagePath);
        string imageName = Path.GetFileName(imagePath).Split('.')[0];
       // string saveImagePath = Application.persistentDataPath + "/Custom";

       // File.WriteAllBytes(saveImagePath + imageName + ".jpg", imageData);

       // var tempImage = File.ReadAllBytes(imagePath);

        texture = new Texture2D(1080, 1440);
        texture.LoadImage(imageData);

        rawImage.texture = texture;
        uploadText.SetActive(rawImage.texture == null);
        
        yield return null;
    }
}
