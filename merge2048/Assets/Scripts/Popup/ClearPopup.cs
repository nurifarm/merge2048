using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPopup : PopupBase
{
    public void OnClickRetry() {
        PopupManager.Instance.Hide(PlayScene.PopupCloseResultType.Retry);

    }

    public void OnClickHome() {
        PopupManager.Instance.Hide(PlayScene.PopupCloseResultType.Home);
    }
}
