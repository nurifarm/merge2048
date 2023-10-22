using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : SceneBase
{
    [SerializeField] private Board board;

    // TODO: use SceneManager
    public void Start() {
        Enter(null);
    }

    public override void Enter(object param)
	{
		Debug.Log("Enter");
        board.GameReady();
        board.GameStart();
	}
}
