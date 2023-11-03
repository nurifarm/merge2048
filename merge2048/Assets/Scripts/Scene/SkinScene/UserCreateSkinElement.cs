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
    

    public void Init(int idx) {
        number.text = $"{Mathf.Pow(2, idx+1)}";
        uploadText.SetActive(rawImage.texture == null);

        // var spriteName = $"Skins/{path}/{idx+1}";
        // var sprite = SpriteManager.Instance.GetSprite(spriteName);
        // image.sprite = sprite;
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
        string saveImagePath = Application.persistentDataPath + "/Image";

        File.WriteAllBytes(saveImagePath + imageName + ".jpg", imageData);

        var tempImage = File.ReadAllBytes(imagePath);

        Texture2D texture = new Texture2D(1080, 1440);
        texture.LoadImage(tempImage);

        rawImage.texture = texture;
        uploadText.SetActive(rawImage.texture == null);
        
        yield return null;
    }
}
