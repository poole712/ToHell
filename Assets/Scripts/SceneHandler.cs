using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneHandler : MonoBehaviour
{
    public GameObject MainMenu, GameShop, LevelSelect, CoinDisplay, Settings;
    public MenuSoundHandler menuSoundHandler;

    public void DisplayLevelSelect()
    {
        LevelSelect.SetActive(true);
        MainMenu.SetActive(false);
        menuSoundHandler.PlayButtonPressSFX();
    }

    public void DisplayShop()
    {
        GameShop.SetActive(true);
        MainMenu.SetActive(false);
        menuSoundHandler.PlayButtonPressSFX();
    }

    public void DisplaySettings()
    {
        Settings.SetActive(true);    
        MainMenu.SetActive(false);
        menuSoundHandler.PlayButtonPressSFX();
    }

    public void DisplayMainMenu(GameObject toDeactivate)
    {
        MainMenu.SetActive(true);
        toDeactivate.SetActive(false);
        menuSoundHandler.PlayButtonPressSFX();
    }
}
