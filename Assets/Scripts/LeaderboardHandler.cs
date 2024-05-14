using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardHandler : MonoBehaviour
{
    public Text[] usernames;
    public Text[] score;
    public DatabaseHandler databaseHandler;

    void OnEnable()
    {
        InitializeUsernames();
    }

    void OnDisable()
    {
        
    }

    public void InitializeUsernames()
    {
        var topFive = databaseHandler.GetTopScores();

        for (int i = 0; i < usernames.Length; i++)
        {
            usernames[i].text = topFive[i].userName;
            score[i].text = topFive[i].score.ToString();
        }
    }
}
