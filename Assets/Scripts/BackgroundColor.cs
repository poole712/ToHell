using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundColor : MonoBehaviour
{
    public Color[] colors;
    public Transform player;
    public float LerpSpeed = 0.5f;

    private Image _image;
    private int _currentIndex = 0;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    public void ChangeColor()
    {
        StartCoroutine(ChangeColoring());
    }

    private IEnumerator ChangeColoring()
    {
        _currentIndex += 1;

        float timeElapsed = 0;



        while (timeElapsed < LerpSpeed)
        {
            timeElapsed += Time.deltaTime;


            yield return null;


            _image.color = Color.Lerp(colors[_currentIndex - 1], colors[_currentIndex], timeElapsed / LerpSpeed);
        }

    }
}
