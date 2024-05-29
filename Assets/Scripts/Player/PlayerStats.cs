using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public TextMeshProUGUI LiveDistanceText;
    public TextMeshProUGUI FinalDistanceText;
    public TextMeshProUGUI FinalLayersText;
    public TextMeshProUGUI OverallScore;
    public DatabaseHandler databaseHandler;
    public GameObject GameOverUI, LeaderboardPrompt;
    private float _overallScore;
    private GameObject _player;

    public int maxHealth = 100;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerHealth>().gameObject;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        LiveDistanceText.text = _player.transform.position.x.ToString("0.00" + "m");
    }

    public void UpdateFinalStats()
    {
        float finalDistance = _player.transform.position.x;
        FinalDistanceText.text = finalDistance.ToString("0.00" + "m");

        string finalLayerName = FindObjectOfType<SegmentManager>().name.Substring(5);
        float layerFloat = float.Parse(finalLayerName);
        FinalLayersText.text = layerFloat.ToString();

        _overallScore = finalDistance * layerFloat;
        OverallScore.text = _overallScore.ToString("0.00");

        if(_overallScore > databaseHandler.GetLowestLeaderboardScore()) {
            LeaderboardPrompt.SetActive(true);  
        } 

        GameOverUI.SetActive(true); 
    }

    public int GetCurrentScore() {
        return (int) _overallScore;
    }

    

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
