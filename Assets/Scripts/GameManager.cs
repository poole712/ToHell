using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu, pauseBtn, playBtn, leaderboardPrompt;
    public TMP_InputField inputField;
    public DatabaseHandler databaseHandler;
    
    //sort out later
    int score = 89;
    
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

    public void UpdateLeaderboard()
    {
        String username = inputField.text;
        databaseHandler.SaveUserData(username, score);
        leaderboardPrompt.SetActive(false);
        QuitToMenu();
    }
}
