using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float MaxHealth = 100f;
    public Image HealthBar;
    public GameObject DeathMenu;
    public User CoinHandler;

    private float _health;
    private int _coins;

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
        }
        HealthBar.fillAmount = _health / MaxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.CompareTag("Coin"))
        {
            CoinHandler.AddCoin(1);
            Destroy(other.gameObject);
        }
    }

}
