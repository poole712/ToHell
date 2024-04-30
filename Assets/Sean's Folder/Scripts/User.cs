using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class User : MonoBehaviour
{
    private int coins;
    private string username;
    public Text coinText;
    public DatabaseHandler DBHandler;

    public void InitCoinDisplayer(){
        coins = DBHandler.GetUserCoins(username);
        UpdateCoinDisplayer(coins);
    }
    public void AddCoin()
    {
        coins = coins + 10;
        UpdateCoinDisplayer(coins);
    }

    public void SubtractCoin(int toSubtract) {
        coins = coins - toSubtract;
        UpdateCoinDisplayer(coins);
    }

    public int GetCoins(){
        return coins;
    }

    public void UpdateCoinDisplayer(int coins){
        coinText.text = "Coins: " + coins + "";
    }

    public void SaveCoinToDatabase(){
        DBHandler.SaveUserData(username, coins);
    }

    public void SetUsername(string username){
        this.username = username;
    }
    public string GetUsername() {
        return username;
    }
}
