using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardHandler : MonoBehaviour
{
    public Text[] usernames;
    public Text[] score;

    void OnEnable()
    {
        InitializeUsernames();
        InitializseScores();
    }

    void OnDisable()
    {
        
    }

    public void InitializeUsernames()
    {
        //GET TOP 10 USERNAMES FROM DATABASE HANDLER AND STORE IN  String[];
        //INITIALISE EACH TEXT[] WITH STRING[]
        for (int i = 0; i < usernames.Length; i++)
        {
            usernames[i].text = "Username";
        }
    }

    public void InitializseScores()
    {
        //GET TOP 10 SCORES FROM DATABASE HANDLER AND STORE IN String[];
        //INITIALISE EACH TEXT[] WITH STRING[]
        //GET TOP 10 USERNAMES FROM DATABASE HANDLER AND STORE IN  String[];
        //INITIALISE EACH TEXT[] WITH STRING[]
        for (int i = 0; i < score.Length; i++)
        {
            score[i].text = "123456";
        }
    }
}
