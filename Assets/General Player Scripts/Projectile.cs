using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject ProjectileObject;
    public KeyCode attackButton;
    private double time;
    private PlayerMovement movementScript;
    public Vector3 way;
    // Start is called before the first frame update
    void Start()
    {
        movementScript = gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(attackButton))
        {
            FireProjectile();
        }
    }

    void FireProjectile()
    {

        GameObject newProjectile = Instantiate(ProjectileObject, transform.position, Quaternion.identity);
        ProjectileScript projectileScriptShoot = newProjectile.GetComponent<ProjectileScript>();
        if (movementScript.lookDirection())
        {
            projectileScriptShoot.way = Vector3.right;
        }else
        {
            projectileScriptShoot.way = Vector3.left;
        }
    }
}

