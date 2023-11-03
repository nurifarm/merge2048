using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SkinSet : MonoBehaviour
{
    [SerializeField] protected SkinElement[] skinElements;
    [SerializeField] private GameObject selected;
    [SerializeField] private Button selectButton;

    public static Action<string> OnSelect;
    private string skinPath;

    public void Init(string path) {
        skinPath = path;

        gameObject.SetActive(true);
        
        for(int i = 0; i < skinElements.Length; ++i) {
            skinElements[i].Init(i, path);
        }

        SetSelected(UserDataManager.Instance.SelectedSkinPath);
    }

    public void SetSelected(string userSkinPath) {
        selected.SetActive(userSkinPath == skinPath);
    }

    public void OnClickSelect() {
        OnSelect?.Invoke(skinPath);
    }
}
