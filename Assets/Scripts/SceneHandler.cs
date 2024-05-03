using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHandler : MonoBehaviour
{
    public GameObject MainMenu, LoginPage, GameShop, LevelSelect, CoinDisplay;

    public void DisplayLevelSelect()
    {
        LevelSelect.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void DisplayShop()
    {
        GameShop.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void DisplayLoginScreen()
    {
        CoinDisplay.SetActive(false);
        LoginPage.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void DisplayMainMenu(GameObject toDeactivate)
    {
        MainMenu.SetActive(true);
        toDeactivate.SetActive(false);
    }

    public void StartGame()
    {
        CoinDisplay.SetActive(true);
        MainMenu.SetActive(true);
        LoginPage.SetActive(false);
    }
}
