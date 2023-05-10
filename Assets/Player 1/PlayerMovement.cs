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
    private Stats stats;

    private bool canJump = false;
    private double timeElapsed = 0;
    private double timeSinceLastJump = double.PositiveInfinity;
    private bool lookingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        stats = gameObject.GetComponent<Stats>();
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
        stats.health = 0;
        stats.stocks--;
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

        if (collision.gameObject.GetComponent<ProjectileScript>() != null)
        {
            if (collision.gameObject.transform.position.x > gameObject.transform.position.x)
            {
                Debug.Log("adding force");
                rb.AddForce(new Vector3(-1 * (1 + ((int)stats.health /10)) * 5, 0, 0), ForceMode.Impulse);
                stats.health += 6;
            }
            else
            {
                Debug.Log("adding force");
                rb.AddForce(new Vector3(1 * (1 + ((int)stats.health / 10)) * 5, 0, 0), ForceMode.Impulse);
                stats.health += 6;
            }
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.GetComponent<DeleteObj>() != null && collision.gameObject.transform.parent != gameObject.transform)
        {
            if (collision.gameObject.transform.position.x > gameObject.transform.position.x)
            {
                Debug.Log("adding force");
                rb.AddForce(new Vector3(-1 * (1 + ((int)stats.health / 10)) * 5, 0, 0), ForceMode.Impulse);
                stats.health += 12;
            }
            else
            {
                Debug.Log("adding force");
                rb.AddForce(new Vector3(1 * (1 + ((int)stats.health / 10)) * 5, 0, 0), ForceMode.Impulse);
                stats.health += 12;
            }
            Destroy(collision.gameObject);
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
