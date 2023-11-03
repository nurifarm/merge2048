using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : Singleton<SpriteManager>
{
    Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
    public Sprite GetSprite(string spriteName) {
        if(sprites.ContainsKey(spriteName) == false) {
            var sprite = Resources.Load<Sprite>(spriteName);
            sprites.Add(spriteName, sprite);
        }
        
        return sprites[spriteName];
    }
}
