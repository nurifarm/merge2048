using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinScene : SceneBase
{
    [SerializeField] private SkinSet skinSetPrefab;
    [SerializeField] private Transform skinListRoot;

    private List<string> skinList = new List<string>() {
        "Fruits",
        "Food"
    };

    private List<SkinSet> skinSets = new List<SkinSet>();

    public override void Enter(object param) {
        for(int i=0; i<this.skinList.Count;i++){
            var skinSet = Instantiate(skinSetPrefab, skinListRoot) as SkinSet;
            skinSet.Init(this.skinList[i]);
            skinSets.Add(skinSet);
        }

        skinSetPrefab.gameObject.SetActive(false);
    }
}
