using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SkinScene : SceneBase
{
    [SerializeField] private SkinSet skinSetPrefab;
    [SerializeField] private Transform skinListRoot;
    [SerializeField] private Button moreButton;

    private List<string> skinList = new List<string>() {
        "animal",
        "animal2",
        "animal3",
    };

    private List<SkinSet> skinSets = new List<SkinSet>();

    private void Start() {
        SkinSet.OnSelect = OnSelectSkinSet;
    }

    private void OnSelectSkinSet(string path) {
        Debug.Log(path);
        UserDataManager.Instance.SetSkin(path);

        foreach(var skinSet in skinSets) {
            skinSet.SetSelected(UserDataManager.Instance.SelectedSkinPath);
        }
    }

    public override void Enter(object param) {
        for(int i=0; i<this.skinList.Count;i++){
            var skinSet = Instantiate(skinSetPrefab, skinListRoot) as SkinSet;
            skinSet.Init(this.skinList[i]);
            skinSets.Add(skinSet);
        }

        skinSetPrefab.gameObject.SetActive(false);
        moreButton.transform.SetAsLastSibling();
    }

    public void OnClickMore() {
        GetImage();
    }

    RawImage rawImage; // 불러온 이미지를 보여줄 RawImage

    public void GetImage () {
        return;
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
        
        yield return null;
    }
}
