using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemy : MonoBehaviour
{
    public float attackRange = 1.5f;
    public float attackCooldown = 2f;
    public LayerMask playerLayer;
    public int attackDamage = 10;
    public float walkSpeed = 2f;

    private Animator animator;
    private float attackTimer;
    private Transform playerTransform;
    private bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (isDead) return; // Stop updating if the enemy is dead

        if (PlayerInRange() && attackTimer >= attackCooldown)
        {
            Attack();
        }
        else
        {
            Walk();
        }

        attackTimer += Time.deltaTime;
    }

    private bool PlayerInRange()
    {
        return Vector3.Distance(transform.position, playerTransform.position) < attackRange;
    }

    private void Walk()
    {
        animator.SetBool("Walk", true);
        if (!PlayerInRange())
        {
            // Move towards the player
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, walkSpeed * Time.deltaTime);
        }
    }

    private void Attack()
    {
        animator.SetBool("Walk", false);
        attackTimer = 0f;
        animator.SetTrigger("Attack");
    }

    // This should be called via Animation Event at the moment of the actual sword hit
    public void DealDamage()
    {
        if (PlayerInRange())
        {
            PlayerHealth playerHealth = playerTransform.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        // Call this when the enemy gets hit
        animator.SetTrigger("Die");
        isDead = true;
        // Optionally, disable the enemy's collider here
        // GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1f); // Wait for death animation to finish before destroying
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Play player death animation and handle the death logic
        Debug.Log("Player died.");
        // This would typically involve disabling player controls, playing a death animation, etc.
    }
}
