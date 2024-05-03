using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour
{
    public  SceneHandler SceneHandler;
    public GameObject LevelSelect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickedReturn()
    {
        SceneHandler.DisplayMainMenu(LevelSelect);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
}
