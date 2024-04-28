using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualLayerDamage : MonoBehaviour
{
    public Sprite[] Cracks;

    private SpriteRenderer _spriteRend;
    private int _currentIndex = 0;

    private void Start()
    {
        _spriteRend = GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite()
    {
        _currentIndex += 1;

        if(_currentIndex < Cracks.Length)
        {
            _spriteRend.sprite = Cracks[_currentIndex];
        }
    }
}
