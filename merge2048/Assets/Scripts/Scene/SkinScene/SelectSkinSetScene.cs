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
        // default skinset
        for(int i=0; i<this.skinList.Count;i++){
            CreateSkinSet(this.skinList[i]);
        }
        // user custom skinset
        foreach(var skinSetPath in UserDataManager.Instance.CustomSkins) {
            CreateSkinSet($"Custom/{skinSetPath}");
        }

        skinSetPrefab.gameObject.SetActive(false);
        moreButton.transform.SetAsLastSibling();

        void CreateSkinSet(string filepath) {
            var skinSet = Instantiate(skinSetPrefab, skinListRoot) as SkinSet;
            skinSet.Init(filepath);
            skinSets.Add(skinSet);
        }
    }


    public void OnClickCreateSkinSet() {
        CSceneManager.Instance.Change("CreateSkinSetScene");
    }

    public void OnClickMore() {
       // GetImage();
    }
    
}
