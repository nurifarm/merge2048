using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;


class CSceneManager : UniSingleton<CSceneManager>
{
	SceneBase CurrentScene;

	public async UniTask Change(string sceneName, object param = null)
	{
		if(CurrentScene != null)
		{
			CurrentScene.Exit();
		}
		// ------------------------------------------------------------
		// LoadingScene
		// ------------------------------------------------------------
		await SceneManager.LoadSceneAsync("LoadingScene");
		// ------------------------------------------------------------
		// Load data(excute Action method)
		// ------------------------------------------------------------
		if (!GameDataManager.Instance.isLoadedData())
			await GameDataManager.Instance.Load();
		
		// ------------------------------------------------------------
		// dummy time
		// ------------------------------------------------------------
		await UniTask.Delay(2000);
		// ------------------------------------------------------------
		// Load Scene
		// ------------------------------------------------------------
		await SceneManager.LoadSceneAsync(sceneName);

		var sceneObject = GameObject.FindGameObjectWithTag("Scene");
		Debug.Assert(sceneObject != null, "Scene is empty. need to set tag Scene");

		var scene = sceneObject.GetComponent<SceneBase>();
		Debug.Assert(scene != null, "Scene is empty. need to add SceneBase component");

		CurrentScene = scene;
		scene.Enter(param);
	}

}
