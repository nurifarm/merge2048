using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using TMPro;

public class IntroScene : SceneBase
{
    [SerializeField] TMP_Text loading;
    CancellationTokenSource cts;

    public async void Start() {
        cts = new CancellationTokenSource();

        UpdateLoading();

        await CSceneManager.Instance.Change("MainScene");

        cts?.Cancel();
    }

    private async void UpdateLoading() {
        try {
            while(true) {
                loading.text = "Loading.";
                await UniTask.Delay(200, cancellationToken: cts.Token);
                loading.text = "Loading..";
                await UniTask.Delay(200, cancellationToken: cts.Token);
                loading.text = "Loading...";
                await UniTask.Delay(200, cancellationToken: cts.Token);
            }
        } catch {

        }
    }
    
}
