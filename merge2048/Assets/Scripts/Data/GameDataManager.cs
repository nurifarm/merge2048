using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

/// <summary>
/// 데이터 관리 클래스
/// </summary>
public class GameDataManager : UniSingleton<GameDataManager>
{
    
    // User Data
    UserData userData;

    // Monster Master Data
    MonsterMasterData monsterMasterData;
    
    // Stage Master Data


    // Save
    public void Save()
    {
        // User Data
    }

    // Load
    public async UniTask Load() 
    {
        await Task.Run(() => {
            // Monster Master Data
            Debug.Log("Monster Master Data");
            // Stage Master Data
            Debug.Log("Stage Master Data");
        });
        
    }


    // User Data Save
    public async UniTask SaveUserData()
    {
        await Task.Run(() => {
            // User Data
            Debug.Log("Save User Data");

        });
    }

    // User Data Load
    public async UniTask LoadUserData(ClientOutput clientOutput)
    {
        await Task.Run(() => {
            // User Data
            Debug.Log("Load User Data");
            string rs = clientOutput.rs.ToString();
            userData = Utils.JsonToObject<UserData>(rs);
            Debug.Log(userData.userName);
        });
        
    }

    public bool isLoadedData()
    {
        // TODO
        if (userData == null || monsterMasterData == null)
            return false;
        
        return true;
    }
    

}
