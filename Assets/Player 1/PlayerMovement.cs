using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed = 5;
    public int jumpSpeed = 5;
    public Rigidbody rb;
    public GameObject RespawnAnchor;
    public double jumpCooldown = 1;

    private bool canJump = false;
    private double timeElapsed = 0;
    private double timeSinceLastJump = double.PositiveInfinity;
    private bool lookingRight = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        timeSinceLastJump += Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
        {
            lookingRight = true;
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            lookingRight = false;
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W) && canJump && timeSinceLastJump >= jumpCooldown)
        {
            timeSinceLastJump = 0;
            //a lot of force is needed to make the player jump so the * 100 seems to work well for this
            rb.AddForce(Vector3.up * jumpSpeed * 100);
        }
    }

    private void death()
    { 
        transform.position = RespawnAnchor.transform.position;
        rb.velocity = Vector3.zero;
    }

    //Returns true if the character is looking right and false if the character is looking left
    public bool lookDirection()
    {
        return lookingRight;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //layer 3 is the stage layer
        if (collision.gameObject.layer == 3)
        {
            canJump = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //layer 3 is the stage layer
        if (collision.gameObject.layer == 3)
        {
            canJump = false;
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
