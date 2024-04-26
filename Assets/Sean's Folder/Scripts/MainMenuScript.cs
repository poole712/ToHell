using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UIElements;


public class MainMenuScript : MonoBehaviour
{
    private UIDocument doc;
    private Button playButton, settingsButton, shopButton, logoutButton;
    public LoginPageScript userDetails;
    public DatabaseScript DBHandler;
    public CoinHandlerScript userCoins;
    public GameObject mainPage, loginPage, gameShop, levelSelect, coinDisplayer;


    void OnEnable()
    {
        //set initial user text coin here
        userCoins.InitCoinDisplayer(userDetails.GetUsername());
        InitialiseUI();
        GetUserStats();
    }
    private void OnDisable() {
        UnregisterUI();
        //add code here for disabling. potentially sound queue?
    }

    private void InitialiseUI() {
        doc = GetComponent<UIDocument>();

        //referencing and registering evt per button
        playButton = doc.rootVisualElement.Q("Start") as Button;
        playButton.RegisterCallback<ClickEvent>(ClickedPlay);

        settingsButton = doc.rootVisualElement.Q("Settings") as Button;
        settingsButton.RegisterCallback<ClickEvent>(ClickedSettings);

        shopButton = doc.rootVisualElement.Q("Shop") as Button;
        shopButton.RegisterCallback<ClickEvent>(ClickedShop);

        logoutButton = doc.rootVisualElement.Q("Logout") as Button;
        logoutButton.RegisterCallback<ClickEvent>(ClickedLogout);
    }

    private void UnregisterUI() {
        playButton.UnregisterCallback<ClickEvent>(ClickedPlay);
        settingsButton.UnregisterCallback<ClickEvent>(ClickedSettings);
        shopButton.UnregisterCallback<ClickEvent>(ClickedShop);
        logoutButton.UnregisterCallback<ClickEvent>(ClickedLogout);
    }

    private void GetUserStats() {
         // for user saving
        Boolean exist = DBHandler.CheckUserExist(userDetails.GetUsername());
        if (exist) {
            Debug.Log("Welcome back, " + userDetails.GetUsername());
        } else {
            Debug.Log("New User, welcome to the family: "+userDetails.GetUsername());
        }
    }

    private void ClickedPlay(ClickEvent evt) {
        levelSelect.SetActive(true);
        mainPage.SetActive(false);
    }

    private void ClickedSettings(ClickEvent evt) {
        Debug.Log("Clicked Settings");
    }

    private void ClickedShop(ClickEvent evt) {
        gameShop.SetActive(true);
        mainPage.SetActive(false);
    }

    private void ClickedLogout(ClickEvent evt) {
        userCoins.SaveCoinToDatabase(userDetails.GetUsername());
        coinDisplayer.SetActive(false);
        loginPage.SetActive(true);
        mainPage.SetActive(false);
    }
}
