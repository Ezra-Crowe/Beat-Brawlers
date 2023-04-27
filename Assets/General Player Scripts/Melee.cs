using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Melee : MonoBehaviour
{
    public KeyCode attackButton;
    public GameObject hitbox;
    private double time;
    public double meleeTime;
    private PlayerMovement movementScript;

    // Start is called before the first frame update
    void Start()
    {
        movementScript = gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(attackButton)){
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
