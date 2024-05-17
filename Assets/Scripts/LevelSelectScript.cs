using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour
{
    public SceneHandler SceneHandler;
    public GameObject LevelSelect, warning;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnDisable()
    {
        warning.SetActive(false);
    }

    public void ClickedReturn()
    {
        SceneHandler.DisplayMainMenu(LevelSelect);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ClickedUnlockLevel()
    {
        warning.SetActive(true);
    }
}
