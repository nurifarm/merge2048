using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class UserCreateSkinSet : MonoBehaviour
{
    [SerializeField] private UserCreateSkinElement[] skinElements;

    public void Init() {
        for(int i = 0; i < skinElements.Length; ++i) {
            skinElements[i].Init(i);
        }
    }
}
