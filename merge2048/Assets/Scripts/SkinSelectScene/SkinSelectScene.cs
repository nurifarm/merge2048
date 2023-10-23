using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelectScene : SceneBase
{
    [SerializeField] private SkinSet skinSet;

    void Start() {
        Enter(null);
    }

    public override void Enter(object param) {
        skinSet.Init("fruits");
    }
}
