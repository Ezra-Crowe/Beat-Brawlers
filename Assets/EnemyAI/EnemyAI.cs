using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float attackDistance = 5f; // Distance from which the enemy can attack
    public float attackDelay = 2f; // Time between attacks
    public float attackDamage = 15f; // Damage done by each attack
    public float attackCooldown = 0f; // Time until the next attack can be 
    private Rigidbody rb;
    public GameObject RespawnAnchorAI;
    public Stats Script;

    public Transform target; // Player's transform
    public float maxHealth = 100f; // The enemy's maximum health
    private float currentHealth; // The enemy's current health
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb = gameObject.GetComponent<Rigidbody>();

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
        else 
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position,.01f);
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
            death();
        }
    }

    private void death()
    { 
        transform.position = RespawnAnchorAI.transform.position;
        rb.velocity = Vector3.zero;
        Script.stocks--;
        Script.health = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<ProjectileScript>() != null)
        {
            if (collision.gameObject.transform.position.x > gameObject.transform.position.x)
            {
                Debug.Log("adding force");
                rb.AddForce(new Vector3(-1 * (1 + ((int)Script.health / 10)) * 5, 0, 0), ForceMode.Impulse);
                Script.health += 6;
            }
            else
            {
                Debug.Log("adding force");
                rb.AddForce(new Vector3(1 * (1 + ((int)Script.health / 10)) * 5, 0, 0), ForceMode.Impulse);
                Script.health += 6;
            }
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.GetComponent<DeleteObj>() != null && collision.gameObject.transform.parent != gameObject.transform)
        {
            if (collision.gameObject.transform.position.x > gameObject.transform.position.x)
            {
                Debug.Log("adding force");
                rb.AddForce(new Vector3(-1 * (1 + ((int)Script.health / 10)) * 5, 0, 0), ForceMode.Impulse);
                Script.health += 12;
            }
            else
            {
                Debug.Log("adding force");
                rb.AddForce(new Vector3(1 * (1 + ((int)Script.health / 10)) * 5, 0, 0), ForceMode.Impulse);
                Script.health += 12;
            }
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //layer 6 is the boundry layer
        if (other.gameObject.layer == 6)
        {
            death();
        }
    }
}
