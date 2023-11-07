using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

/// <summary>
/// User Data 관리 클래스
/// </summary>
public class UserDataManager : Singleton<UserDataManager>
{
    // User Data
    UserData userData;
    public string SelectedSkinPath => userData?.selectedSkinPath;
    public List<int> CustomSkins => userData?.customSkins;
    

	protected override void Init() {
        Debug.Log("Load");
        Load();
    }

    // Save
    public void Save()
    {
        var jsonData = JsonUtility.ToJson(userData);
        PlayerPrefs.SetString("UserData", jsonData);
        PlayerPrefs.Save();
    }

    // Load
    public void Load() 
    {
        if(PlayerPrefs.HasKey("UserData") == false) {
            userData = new UserData();
            Save();
        } else {
            var data = PlayerPrefs.GetString("UserData");
            userData = JsonUtility.FromJson<UserData>(data);
        }
    }

    void RequestCompleted(ClientOutput clientOutput)
    {
        // TODO
    }

    public void SetSkin(string selectedSkinPath) {
        userData.selectedSkinPath = selectedSkinPath;
        Save();
    }

    public void AddCustomSkin(int customSkinPath) {
        userData.customSkins.Add(customSkinPath);
        Save();
    }
}
