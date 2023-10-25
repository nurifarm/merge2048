using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSet : MonoBehaviour
{
    [SerializeField] private SkinElement[] skinElements;

    public void Init(string path) {
        gameObject.SetActive(true);
        
        for(int i = 0; i < skinElements.Length; ++i) {
            skinElements[i].Init(i, path);
        }

    }
}
