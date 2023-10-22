using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SceneBase : MonoBehaviour 
{
	public virtual void Enter(object param)
	{
		Debug.Log("Enter");
	}

	public virtual void Exit()
	{
		Debug.Log("Exit");
	}
}
