using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float MaxHealth = 100f;
    public Image HealthBar;

    private CapsuleCollider2D _capsuleCollider;
    private float _health;

    private void Start()
    {
        _health = MaxHealth;
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        HealthBar.fillAmount = _health / MaxHealth;
    }

    public void Damage(float dmg)
    {
        _health -= dmg;
        HealthBar.fillAmount = _health / MaxHealth;
    }


}
