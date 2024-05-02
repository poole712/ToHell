using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class User : MonoBehaviour
{
    private int _coins;
    private string _username;
    public Text CoinText;
    public DatabaseHandler DBHandler;

    public void InitCoinDisplayer()
    {
        _coins = DBHandler.GetUserCoins(_username);
        UpdateCoinDisplayer(_coins);
    }
    public void AddCoin()
    {
        _coins = _coins + 10;
        UpdateCoinDisplayer(_coins);
    }

    public void SubtractCoin(int toSubtract) 
    {
        _coins = _coins - toSubtract;
        UpdateCoinDisplayer(_coins);
    }

    public int GetCoins()
    {
        return _coins;
    }

    public void UpdateCoinDisplayer(int coins)
    {
        CoinText.text = "Coins: " + coins + "";
    }

    public void SaveCoinToDatabase()
    {
        DBHandler.SaveUserData(_username, _coins);
    }

    public void SetUsername(string username)
    {
        this._username = username;
    }
    public string GetUsername() 
    {
        return _username;
    }
}
