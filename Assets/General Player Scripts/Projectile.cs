using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject ProjectileObject;
    public KeyCode attackButton;
    public double cooldown;
    private double time;
    private PlayerMovement movementScript;
    private Vector3 way;
    //this is a multiplier for projectile speed
    public float projectileSpeed = 1;
    private double timeSinceAttack = 0;

    // Start is called before the first frame update
    void Start()
    {
        movementScript = gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceAttack += Time.deltaTime;
        if (Input.GetKeyDown(attackButton) && timeSinceAttack >= cooldown)
        {
            timeSinceAttack = 0;
            FireProjectile();
        }
    }

    void FireProjectile()
    {
        Vector3 spawnPos = new Vector3(transform.position.x + ((movementScript.lookDirection()) ? 1 : -1), transform.position.y, transform.position.z);
        GameObject newProjectile = Instantiate(ProjectileObject, spawnPos, Quaternion.identity);
        ProjectileScript projectileScriptShoot = newProjectile.GetComponent<ProjectileScript>();
        if (movementScript.lookDirection())
        {
            projectileScriptShoot.way = new Vector3(projectileSpeed, 0, 0);
        }else
        {
            projectileScriptShoot.way = new Vector3(projectileSpeed * -1, 0, 0);
        }
    }
}

