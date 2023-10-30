using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePopup : PopupBase
{
    public void OnClickQuit() {
        PopupManager.Instance.Hide(PlayScene.PopupCloseResultType.Quit);
    }

    public void OnClickRetry() {
        PopupManager.Instance.Hide(PlayScene.PopupCloseResultType.Retry);
    }
    
    public void OnClickBack() {
        PopupManager.Instance.Hide(PlayScene.PopupCloseResultType.Back);
    }
}
