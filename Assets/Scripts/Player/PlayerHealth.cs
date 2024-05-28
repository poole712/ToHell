using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float MaxHealth = 100f;
    public Image HealthBar;
    public GameObject DeathMenu;
    public CoinHandler CoinHandler;
    public PlayerStats PlayerStats;

    private float _health;
    private int _coins;

    //
    private bool isInvincible = false;

    private void Start()
    {
        CoinHandler.InitCoinDisplayer();
        _health = MaxHealth;
        HealthBar.fillAmount = _health / MaxHealth;
    }

    public void Damage(float dmg)
    {
        _health -= dmg;
        if(_health <= 0 )
        {
            Time.timeScale = 0.0f;
            DeathMenu.SetActive(true);
            PlayerStats.UpdateFinalStats();
        }
        HealthBar.fillAmount = _health / MaxHealth;
    }

    public void EnableInvincibility(float duration)
    {
        StartCoroutine(Invincibility(duration));
    }

    private IEnumerator Invincibility(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
        Debug.Log("Invincibility ended");
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.CompareTag("Coin"))
        {
            CoinHandler.AddCoin(1);
            Destroy(other.gameObject);
        }
    }

}
