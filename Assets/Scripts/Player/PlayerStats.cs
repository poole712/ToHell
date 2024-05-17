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

    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerHealth>().gameObject;
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

        float overallScore = finalDistance * layerFloat;
        OverallScore.text = overallScore.ToString("0.00");

        GameOverUI.SetActive(true); 

        if(overallScore > databaseHandler.GetLowestLeaderboardScore()) {
            LeaderboardPrompt.SetActive(true);  
        } 

    }

    public int GetCurrentScore() {
        float finalDistance = _player.transform.position.x;
        FinalDistanceText.text = finalDistance.ToString("0.00" + "m");

        string finalLayerName = FindObjectOfType<SegmentManager>().name.Substring(5);
        float layerFloat = float.Parse(finalLayerName);
        FinalLayersText.text = layerFloat.ToString();

        float overallScore = finalDistance * layerFloat;

        return (int) overallScore;

    }
}
