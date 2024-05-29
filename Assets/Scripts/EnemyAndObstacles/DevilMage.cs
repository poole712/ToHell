using UnityEngine;

public class DevilMage : GameEntities
{
    public string fireballTag; // Tag for the fireball in the pool
    public Transform firePoint;
    private float attackCooldown = 5f;
    private float nextAttackTime = 0f;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void Attack()
    {
        ObjectPooler.Instance.SpawnFromPool(fireballTag, firePoint.position, firePoint.rotation);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            Destroy(gameObject);
        }
    }
}
