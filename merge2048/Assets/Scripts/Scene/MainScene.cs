using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : SceneBase
{
    public void OnClickStart() {
        CSceneManager.Instance.Change("PlayScene");
    }

    public void OnClickSkin() {
        CSceneManager.Instance.Change("SkinScene");
    }
}
