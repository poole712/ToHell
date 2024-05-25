using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCostumeSetup : MonoBehaviour
{
    public List<Sprites> CostumeSprites;
    public List<Sprite> HammerSprites;

    public SpriteRenderer HammerSprite;

    private int _costumeIndex = 0;
    private int _hammerIndex = 0;

    private void Awake()
    {
        _costumeIndex = PlayerPrefs.GetInt("EquippedCharacter", 0) - 1;
        Debug.Log("Costume" + _costumeIndex);
        if (_costumeIndex >= 0)
        {
            for (int i = 0; i < transform.childCount - 2; i++)
            {
                Transform tform = transform.GetChild(i);
                tform.GetComponent<SpriteRenderer>().sprite = CostumeSprites[_costumeIndex].SpriteCostumeSprites[i];
            }
        }
        _hammerIndex = PlayerPrefs.GetInt("EquippedHammer", 0);
        if (_hammerIndex >= 0)
        {
            HammerSprite.sprite = HammerSprites[_hammerIndex];
        }

    }
}

[System.Serializable]
public class Sprites
{
    public List<Sprite> SpriteCostumeSprites;

}
