using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkinSetScene : SceneBase
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

    public void OnClickCreateSkinSet() {
        CSceneManager.Instance.Change("CreateSkinSetScene");
    }

    public void OnClickMore() {
       // GetImage();
    }
    
}
