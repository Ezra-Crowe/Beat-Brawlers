using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Melee : MonoBehaviour
{
    public KeyCode attackButton;
    public GameObject hitbox;
    private double time;
    public double meleeTime;
    public double meleeCooldown;
    private PlayerMovement movementScript;
    private double timeSinceMelee = 0;

    // Start is called before the first frame update
    void Start()
    {
        movementScript = gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceMelee -= Time.deltaTime;
        if(Input.GetKeyDown(attackButton) && (timeSinceMelee + meleeCooldown) <= 0){
            Debug.Log("attempting to attack");
            timeSinceMelee = meleeTime;
            if(movementScript.lookDirection()){
                GameObject melee = Instantiate(hitbox, new Vector3 (gameObject.transform.position.x+1 , gameObject.transform.position.y , gameObject.transform.position.z), gameObject.transform.rotation) as GameObject;
                melee.transform.parent = gameObject.transform;
                DeleteObj deleteScript = melee.GetComponent<DeleteObj>();
                deleteScript.deletionTime = meleeTime;
                Debug.Log("attacking");
            }
            else{
                GameObject melee = Instantiate(hitbox, new Vector3 (gameObject.transform.position.x-1 , gameObject.transform.position.y , gameObject.transform.position.z), gameObject.transform.rotation) as GameObject;
                melee.transform.parent = gameObject.transform;
                DeleteObj deleteScript = melee.GetComponent<DeleteObj>();
                deleteScript.deletionTime = meleeTime;
                Debug.Log("attacking");
            }
        }

    }
    //Sphere needs to appear to the left or right of the player depending on the lookDirection
    //If the opponent touches the sphere, health goes down
    //Sphere disappears
    //
}
