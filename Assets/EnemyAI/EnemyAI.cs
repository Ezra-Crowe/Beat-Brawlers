using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float attackDistance = 1f; // Distance from which the enemy can attack
    public float attackDelay = 2f; // Time between attacks
    public float attackDamage = 15f; // Damage done by each attack
    private Rigidbody rb;
    public GameObject RespawnAnchorAI;
    public GameObject ProjectileObject;
    public float projectileSpeed = 1;
    double projTimeSinceAttack = 0;
    public double projcooldown = 5f;


    public Stats Script;
    private Melee meleeScript;
    private Projectile projectileScipt; 

    public Transform target; // Player's transform
    public float maxHealth = 100f; // The enemy's maximum health
    private float currentHealth; // The enemy's current health
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        projectileScipt = gameObject.GetComponent<Projectile>();
        rb = gameObject.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        projTimeSinceAttack += Time.deltaTime;
            bool lookDirection = true;
            if(target.position.x < transform.position.x) {
                lookDirection = false;
            }


        // Check if the player is within attack distance
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget <= attackDistance)
        {
            
        }
        else 
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position,.01f);
            if(projTimeSinceAttack >= projcooldown) {
                projTimeSinceAttack = 0;
                Vector3 spawnPos = new Vector3(transform.position.x + ((lookDirection) ? 1 : -1), transform.position.y, transform.position.z);
                GameObject newProjectile = Instantiate(ProjectileObject, spawnPos, Quaternion.identity);
                ProjectileScript projectileScriptShoot = newProjectile.GetComponent<ProjectileScript>();
                if (lookDirection) {
                projectileScriptShoot.way = new Vector3(projectileSpeed, 0, 0);
                }
                else {
                projectileScriptShoot.way = new Vector3(projectileSpeed * -1, 0, 0);
                }
            }
        }
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
