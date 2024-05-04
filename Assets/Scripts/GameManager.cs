using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
