using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;


class CSceneManager : UniSingleton<CSceneManager>
{
	SceneBase CurrentScene;
	string prevSceneName = "";
	bool isChanging;

	public async UniTask ChangePrevScene(object param = null) {
#if UNITY_EDITOR
		if(string.IsNullOrEmpty(prevSceneName)) {
			prevSceneName = "MainScene";
		}

#endif
		Change(prevSceneName, param);
	}
	
	public async UniTask Change(string sceneName, object param = null)
	{
		Debug.Log($"Change to {sceneName} isChaing: {isChanging}");
		if(isChanging) return;

		isChanging = true;

		if(CurrentScene != null)
		{
			prevSceneName = CurrentScene.GetType().Name;
			Debug.Log($"{prevSceneName} Exit");
			CurrentScene.Exit();
		}
		// ------------------------------------------------------------
		// LoadingScene
		// ------------------------------------------------------------
		//await SceneManager.LoadSceneAsync("LoadingScene");
		// ------------------------------------------------------------
		// Load data(excute Action method)
		// ------------------------------------------------------------
		//if (!GameDataManager.Instance.isLoadedData())
		//	await GameDataManager.Instance.Load();
		
		// ------------------------------------------------------------
		// dummy time
		// ------------------------------------------------------------
		await UniTask.Delay(100);
		// ------------------------------------------------------------
		// Load Scene
		// ------------------------------------------------------------
		await SceneManager.LoadSceneAsync(sceneName);

		var sceneObject = GameObject.FindGameObjectWithTag("Scene");
		Debug.Assert(sceneObject != null, "Scene is empty. need to set tag Scene");

		var scene = sceneObject.GetComponent<SceneBase>();
		Debug.Assert(scene != null, "Scene is empty. need to add SceneBase component");

		Debug.Log($"CurrentScene is {sceneName}");
		CurrentScene = scene;
		scene.Enter(param);

		isChanging = false;

	}

}
