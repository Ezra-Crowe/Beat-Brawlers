using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Stats : MonoBehaviour
{
    // Start is called before the first frame update
    public double health = 100;
    public double melee_damage = 10;
    public double ranged_damage = 2;
    public float maxHealth = 100; // The enemy's maximum health
    public int stocks;
    private double currentHealth;

    void Start()
    {
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
        public void TakeDamege(float attackDamage)
    {
        double damage = attackDamage;

        if (attackDamage > melee_damage)
        {
            damage -= melee_damage;
            damage += ranged_damage;
        }

        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        // Destroy the enemy game object
        Destroy(gameObject);
    }
}
