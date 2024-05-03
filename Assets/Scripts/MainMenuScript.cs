using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UIElements;


public class MainMenuScript : MonoBehaviour
{
    private UIDocument _doc;
    private Button _playButton, _settingsButton, _shopButton, _logoutButton;
    public DatabaseHandler DBHandler;
    public User UserHandler;
    public SceneHandler SceneManager;
   
    void OnEnable()
    {
        //set initial user text coin here
        UserHandler.InitCoinDisplayer();
        InitialiseUI();
        GetUserStats();
    }
    private void OnDisable() 
    {
        UnregisterUI();
        //add code here for disabling. potentially sound queue?
    }

    private void InitialiseUI() 
    {
        _doc = GetComponent<UIDocument>();

        //referencing and registering evt per button
        _playButton = _doc.rootVisualElement.Q("Start") as Button;
        _playButton.RegisterCallback<ClickEvent>(ClickedPlay);

        _settingsButton = _doc.rootVisualElement.Q("Settings") as Button;
        _settingsButton.RegisterCallback<ClickEvent>(ClickedSettings);

        _shopButton = _doc.rootVisualElement.Q("Shop") as Button;
        _shopButton.RegisterCallback<ClickEvent>(ClickedShop);

        _logoutButton = _doc.rootVisualElement.Q("Logout") as Button;
        _logoutButton.RegisterCallback<ClickEvent>(ClickedLogout);
    }

    private void UnregisterUI() 
    {
        _playButton.UnregisterCallback<ClickEvent>(ClickedPlay);
        _settingsButton.UnregisterCallback<ClickEvent>(ClickedSettings);
        _shopButton.UnregisterCallback<ClickEvent>(ClickedShop);
        _logoutButton.UnregisterCallback<ClickEvent>(ClickedLogout);
    }

    private void GetUserStats() 
    {
         // for user saving
        Boolean exist = DBHandler.CheckUserExist(UserHandler.GetUsername());
        if (exist) {
            Debug.Log("Welcome back, " + UserHandler.GetUsername());
        } else {
            Debug.Log("New User, welcome to the family: "+UserHandler.GetUsername());
        }
    }

    private void ClickedPlay(ClickEvent evt) 
    {
        SceneManager.DisplayLevelSelect();
    }

    private void ClickedSettings(ClickEvent evt) 
    {
        Debug.Log("Clicked Settings");
    }

    private void ClickedShop(ClickEvent evt) 
    {
        SceneManager.DisplayShop();
    }

    private void ClickedLogout(ClickEvent evt) 
    {
        UserHandler.SaveCoinToDatabase();
        SceneManager.DisplayLoginScreen();  
    }
}
