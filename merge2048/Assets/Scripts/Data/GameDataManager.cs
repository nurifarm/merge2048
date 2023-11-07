using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using System.IO;

/// <summary>
/// 데이터 관리 클래스
/// </summary>
public class GameDataManager : UniSingleton<GameDataManager>
{
    public const int SpriteSize = 512;

    public Sprite GetCircleSprite(string path, int idx) {
        if(path.Contains("Custom")) {
            // TODO: file read
            var filePath = Path.Combine(Application.persistentDataPath, path);
            filePath = $"{filePath}/{idx}.png";
            Debug.Log($"{filePath} try load");

             if (File.Exists(filePath)) {
                Debug.Log($"{filePath} is loaded");
                byte[] bytes = File.ReadAllBytes(filePath);
                Texture2D loadedTexture = new Texture2D(2, 2); // You can set the dimensions accordingly
                loadedTexture.LoadImage(bytes);
                // Use the loadedTexture as needed
                return Sprite.Create(loadedTexture, new Rect(0, 0, GameDataManager.SpriteSize, GameDataManager.SpriteSize), new Vector2(0.5f, 0.5f));
            }
        } else {
            var spriteName = $"Skins/{path}/{idx+1}";
            return SpriteManager.Instance.GetSprite(spriteName);
        }
        return null;
    }
    
    // User Data
    UserData userData;


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
        if (userData == null)
            return false;
        
        return true;
    }
    

}
