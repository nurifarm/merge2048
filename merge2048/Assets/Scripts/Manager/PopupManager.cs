using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : UniSingleton<PopupManager>
{
	Dictionary<string, PopupBase> popups = new Dictionary<string, PopupBase>();

	Stack<PopupBase> openPopups = new Stack<PopupBase>();
	Stack<Action<object>> openPopupCallbacks = new Stack<Action<object>>();

	public bool IsAnyPopup => PopupCount > 0;
	public int PopupCount => openPopups.Count;

    public void Show(string popupName, object param = null, Action<object> callback = null)
	{
		if(popups.ContainsKey(popupName) == false)
		{
			var popup = Resources.Load<PopupBase>(popupName);

			var popupObject = Instantiate(popup);
			popupObject.name = popupName;
			popups.Add(popupName, popupObject);
		}

		openPopups.Push(popups[popupName]);
		openPopupCallbacks.Push(callback);
		popups[popupName].gameObject.SetActive(true);
		popups[popupName].Show(param);
	}

	public void Hide(object param = null)
	{
		var currentPopup = openPopups.Peek();
		currentPopup.Hide();
		var currentPopupCallback = openPopupCallbacks.Peek();
		currentPopup.gameObject.SetActive(false);
		openPopups.Pop();
		openPopupCallbacks.Pop();
		currentPopupCallback?.Invoke(param);

		// TODO: 씬전환시 자동삭제만됨. 풀링해서 쓰거나, 씬전환시 popups를 비우거나 등등
		popups.Remove((currentPopup.name));
	}

	public bool IsActive(string popupName)
	{
		if (popups.ContainsKey(popupName) == false) return false;

		return openPopups.Contains(popups[popupName]);
	}
}
