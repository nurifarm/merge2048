using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailPopup : PopupBase
{
    public void OnClickRetry() {
        PopupManager.Instance.Hide(PlayScene.PopupCloseResultType.Retry);

    }

    public void OnClickHome() {
        PopupManager.Instance.Hide(PlayScene.PopupCloseResultType.Home);

    }
}
