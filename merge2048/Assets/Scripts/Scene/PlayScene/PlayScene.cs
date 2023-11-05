using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : SceneBase
{
    public enum PopupCloseResultType {
        Retry,
        Quit,   // from pause popup
        Home,   // from clear, fail popup
        Back
    }

    [SerializeField] private Board board;
    [SerializeField] private SkinElement nextElement;

    public override void Enter(object param)
	{
        Board.OnGameFinish += OnGameFinish;
        Board.OnChangeNextElement += OnChangeNextElement;
		Debug.Log("Enter");
        GameStart();
	}

    public override void Exit()
    {
        base.Exit();
        Board.OnGameFinish -= OnGameFinish;
        Board.OnChangeNextElement -= OnChangeNextElement;
    }

    private void GameStart() {
        board.GameReady();
        board.GameStart();
    }

    private void OnGameFinish(Board.GameState gameState) {
        if(gameState == Board.GameState.Clear) {
            PopupManager.Instance.Show("ClearPopup", callback:OnClosePopup);
        } else if(gameState == Board.GameState.Fail) {
            PopupManager.Instance.Show("FailPopup", callback:OnClosePopup);
        }
    }

    private void OnChangeNextElement(int idx) {
        nextElement.Init(idx, UserDataManager.Instance.SelectedSkinPath);
    }

    public void OnClosePopup(object param) {
        var closeType = (PopupCloseResultType) param;
        Debug.Log(closeType);
        if (closeType == PopupCloseResultType.Back) return;
        
        if(closeType == PopupCloseResultType.Retry) {
            board.ResetGame();
            GameStart();
        } else if(closeType == PopupCloseResultType.Quit || closeType == PopupCloseResultType.Home) {
            board.ResetGame();
            CSceneManager.Instance.Change("MainScene");
        }
    }

    public void OnClickPause() {
        PopupManager.Instance.Show("PausePopup", callback:OnClosePopup);
    }
}
