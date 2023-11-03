using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSkinSetScene : SceneBase
{
    [SerializeField] UserCreateSkinSet skinSet;

    public override void Enter(object param)
    {
        base.Enter(param);
        skinSet.Init();
    }
}
