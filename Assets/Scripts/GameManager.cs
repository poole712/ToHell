using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu, pauseBtn, playBtn;
    
    public void OnEnable() 
    {
        Time.timeScale = 1;
    }
    
    public void Retry()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ClickedPause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        pauseBtn.SetActive(false);
        playBtn.SetActive(true);    
    }
    
    public void ClickedPlay()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false); 
        pauseBtn.SetActive(true);
        playBtn.SetActive(false);  
    }
}
