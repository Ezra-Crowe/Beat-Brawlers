using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float attackDistance = 5f; // Distance from which the enemy can attack
    public float attackDelay = 2f; // Time between attacks
    public float attackDamage = 15f; // Damage done by each attack
    public float attackCooldown = 0f; // Time until the next attack can be 
    public Stats Script;

    public Transform target; // Player's transform
    public float maxHealth = 100f; // The enemy's maximum health
    private float currentHealth; // The enemy's current health
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is within attack distance
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget <= attackDistance)
        {
            // Attack if the attack cooldown is over
            if (attackCooldown <= 0f)
            {
                Attack();
                attackCooldown = attackDelay;
            }
            else
            {
                attackCooldown -= Time.deltaTime;
            }
        }
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Get a reference to the Stats component on the opponent GameObject
            Stats opponentStats = FindObjectOfType<Stats>();

            // Call the TakeDamege method on the opponentStats component, passing in the attack damage
            opponentStats.TakeDamege(attackDamage);
        }
        // Deal damage to the player
        // target.GetComponent<Stats>().TakeDamage(attackDamage);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // Check if the enemy has died
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        // Destroy the enemy game object
        Destroy(gameObject);
    }
}
