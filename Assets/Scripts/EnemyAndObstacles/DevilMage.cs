using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class DevilMage : GameEntities
{
    public GameObject Fireball; // Tag for the fireball in the pool
    public Transform FirePoint;
    public float AttackCooldown = 5f;
    public float SpeedUpMultiplier = 5f;
    public float AttackDelay = 1;
    private float nextAttackTime = 0f;
    private Transform _player;
    private bool _canAttack = false;

    public bool CanAttack => _canAttack;

    protected override void Start()
    {
        base.Start();
        _player = FindObjectOfType<Player2DMovement>().transform;
    }

    protected override void Update()
    {
        CheckOutOfScreen();

        transform.position = new Vector2(_player.transform.position.x + 3.25f, _player.transform.position.y + 0.25f);
        if (Time.time >= nextAttackTime && _canAttack)
        {
            Attack();
            nextAttackTime = Time.time + AttackCooldown;
        }
    }

    public void EnableAttack()
    {
        _canAttack = true;
        Debug.Log("Enabling mage attack");
    }

    public void Attack()
    {
        Instantiate(Fireball, FirePoint.transform.position, quaternion.identity);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fireball"))
        {
            Destroy(gameObject);
        }
    }

}
