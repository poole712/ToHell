using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHandler : MonoBehaviour
{
    public GameObject MainMenu, GameShop, LevelSelect, CoinDisplay, Settings;

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

    public void DisplaySettings()
    {
        Settings.SetActive(true);    
        MainMenu.SetActive(false);
    }

    public void DisplayMainMenu(GameObject toDeactivate)
    {
        MainMenu.SetActive(true);
        toDeactivate.SetActive(false);
    }
}
