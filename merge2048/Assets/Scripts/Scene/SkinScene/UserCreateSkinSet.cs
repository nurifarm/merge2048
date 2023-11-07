using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class UserCreateSkinSet : MonoBehaviour
{
    [SerializeField] private UserCreateSkinElement[] skinElements;

    private static string filepath = null;
    private static string BaseFilepath => filepath!= null? filepath : filepath = Path.Combine(Application.persistentDataPath, "Custom");


    public void Init() {
        for(int i = 0; i < skinElements.Length; ++i) {
            skinElements[i].Init(i);
        }
    }

    public void Save() {
        if (!Directory.Exists(BaseFilepath)) {
            Directory.CreateDirectory(BaseFilepath);
        }
        // TODO: unique
        var randIdx = Random.Range(0, 10000);
        var myFilePath = $"{BaseFilepath}/{randIdx}";
        if (!Directory.Exists(myFilePath)) {
            Directory.CreateDirectory(myFilePath);
        }

        for(int i = 0; i < skinElements.Length; ++i) {
            var data = skinElements[i].Serialize();
            
            if(data != null) {
                Debug.Log($"{myFilePath}/{i}.png is saved");
                File.WriteAllBytes($"{myFilePath}/{i}.png", data);
            }
        }

        UserDataManager.Instance.AddCustomSkin(randIdx);
    }
}
