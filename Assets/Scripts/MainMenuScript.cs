using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UIElements;


public class MainMenuScript : MonoBehaviour
{
    public DatabaseHandler DBHandler;
    public CoinHandler UserHandler;
    public SceneHandler SceneManager;
   
    void OnEnable()
    {
        //Retrieve and Initialise Coin Displayer
        UserHandler.InitCoinDisplayer();
    }
    private void OnDisable() 
    {
        UserHandler.SaveCoinToDatabase();   
        //add code here for disabling. potentially sound queue?
    }
}
