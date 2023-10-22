using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : UniSingleton<PopupManager>
{
	Dictionary<string, PopupBase> popups = new Dictionary<string, PopupBase>();

	Stack<PopupBase> openPopups = new Stack<PopupBase>();

    public void Show(string popupName, object param = null)
	{
		if(popups.ContainsKey(popupName) == false)
		{
			var popup = Resources.Load<PopupBase>(popupName);

			popups.Add(popupName, Instantiate(popup));
		}

		openPopups.Push(popups[popupName]);
		popups[popupName].gameObject.SetActive(true);
		popups[popupName].Show(param);
	}

	public void Hide()
	{
		var currentPopup = openPopups.Peek();
		currentPopup.Hide();
		currentPopup.gameObject.SetActive(false);
		openPopups.Pop();
	}

	public bool IsActive(string popupName)
	{
		if (popups.ContainsKey(popupName) == false) return false;

		return openPopups.Contains(popups[popupName]);
	}
}
