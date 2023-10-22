using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;


public class LoginManager : UniSingleton<LoginManager>
{
	private bool isBusy = false;

    public async UniTask<ClientOutput> Login(string userId, string password)
    {
		// ------------------------------------------------------------
		// check for already processing
		// ------------------------------------------------------------
		if (!isBusy)
		{
			isBusy = true;

			// ------------------------------------------------------------
			// make parameters
			// ------------------------------------------------------------
			Dictionary<string, string> parameters = new Dictionary<string, string>()
			{
				{"serviceId", "gws.auth"},
				{"commandId", "doLogin"},
				{"userId", userId},
				{"password", password}
			};

			// ------------------------------------------------------------
			// excute request
			// ------------------------------------------------------------
			ClientOutput clientOutput = await GWSClient.Request(parameters);
			isBusy = false;

			return clientOutput;
		}
		else
		{
			Debug.Log("이미 처리 중 입니다.");
			return default;
		}

    }


	
}
