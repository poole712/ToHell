using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float MaxHealth = 100f;
    public Image HealthBar;
    public GameObject DeathMenu;

    private float _health;

    private void Start()
    {
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


}
