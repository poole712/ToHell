using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Overlays;
using UnityEngine;

using UnityEngine.UI;


public class MainMenuScript : MonoBehaviour
{
    public Text[] leaderboardUsernames;
    public Text[] leaderboardScores;
    public DatabaseHandler databaseHandler;
    public CoinHandler CoinHandler;
   
    void OnEnable()
    {
        //Retrieve and Initialise Coin Displayer
        CoinHandler.InitCoinDisplayer();
        InitLeaderboard();
    }
    private void OnDisable() 
    {
        CoinHandler.SaveCoinToDatabase();   
        //add code here for disabling. potentially sound queue?
    }

    //Read local database and retrieve top five scores with leaderboardUsernames
    public void InitLeaderboard()
    {
        var topFive = databaseHandler.GetTopScores();

        for (int i = 0; i < leaderboardUsernames.Length; i++)
        {
            leaderboardUsernames[i].text = topFive[i].userName;
            leaderboardScores[i].text = topFive[i].score.ToString();
        }
    }
}
