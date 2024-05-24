using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCostumeSetup : MonoBehaviour
{
    public List<Sprites> sprites;

    private int _costumeIndex = -1;

    private void Awake()
    {
        if (_costumeIndex >= 0)
        {
            for (int i = 0; i < transform.childCount - 2; i++)
            {
                Transform tform = transform.GetChild(i);
                tform.GetComponent<SpriteRenderer>().sprite = sprites[_costumeIndex].sprites[i];
            }
        }

    }
}

[System.Serializable]
public class Sprites
{
    public List<Sprite> sprites;
}
