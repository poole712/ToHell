using TMPro;
using UnityEngine;

public class User : MonoBehaviour
{
    private int _coins;
    public TextMeshProUGUI CoinDisplayer;

    public void InitCoinDisplayer()
    {
        _coins = PlayerPrefs.GetInt("Coins", 0);    
        UpdateCoinDisplayer(_coins);
    }

    //Debugging Purposes
    public void AddCoin(int toAdd)
    {
        _coins = _coins + toAdd;
        SaveCoinToDatabase();
        UpdateCoinDisplayer(_coins);
    }

    public void SubtractCoin(int toSubtract) 
    {
        _coins = _coins - toSubtract;
        SaveCoinToDatabase();
        UpdateCoinDisplayer(_coins);
    }

    public int GetCoins()
    {
        return _coins;
    }

    public void UpdateCoinDisplayer(int coins)
    {
        CoinDisplayer.text = "Coins: " + coins + "";
    }

    public void SaveCoinToDatabase()
    {
        PlayerPrefs.SetInt("Coins", _coins);
        PlayerPrefs.Save();
    }

}
