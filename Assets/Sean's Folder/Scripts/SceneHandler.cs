using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHandler : MonoBehaviour
{
    public GameObject mainMenu, loginPage, gameShop, levelSelect, coinDisplay;

    public void DisplayLevelSelect() {
        levelSelect.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void DisplayShop(){
        gameShop.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void DisplayLoginScreen(){
        coinDisplay.SetActive(false);
        loginPage.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void DisplayMainMenu(GameObject toDeactivate){
        mainMenu.SetActive(true);
        toDeactivate.SetActive(false);
    }

    public void StartGame(){
        coinDisplay.SetActive(true);
        mainMenu.SetActive(true);
        loginPage.SetActive(false);
    }
}
