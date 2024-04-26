using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinHandlerScript : MonoBehaviour
{
    private int coins;
    public ShopManager shopManager;
    public Text coinText;
    public DatabaseScript DBHandler;

    public void InitCoinDisplayer(String userName){
        coins = DBHandler.GetUserCoins(userName);
        updateText(coins);
    }
    public void AddCoin()
    {
        coins = coins + 10;
        shopManager.CheckPurchaseable();
        updateText(coins);
    }

    public int GetCoins(){
        return coins;
    }

    public void updateText(int coins){
        coinText.text = "Coins: " + coins + "";
    }

    public void SaveCoinToDatabase(String name){
        DBHandler.SaveUserData(name, coins);
    }
}
