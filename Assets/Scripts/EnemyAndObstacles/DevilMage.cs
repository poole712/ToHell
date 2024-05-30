using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class DevilMage : GameEntities
{
    public GameObject Fireball; // Tag for the fireball in the pool
    public Transform FirePoint;
    private float attackCooldown = 5f;
    private float nextAttackTime = 0f;
    private bool inView = false;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(WaitToBeInView());
    }

    protected override void Update()
    {
        base.Update();

        if (Time.time >= nextAttackTime && inView)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    IEnumerator WaitToBeInView()
    {
        yield return new WaitForSeconds(1);
        inView = true;
    }

    void Attack()
    {
        Instantiate(Fireball, FirePoint.transform.position, quaternion.identity); 
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fireball"))
        {
            Destroy(gameObject);
        }
    }

}
