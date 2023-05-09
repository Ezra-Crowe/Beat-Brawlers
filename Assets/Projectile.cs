using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public GameObject ProjectileObject;
    public float Speed = 5f;
    public Vector3 way;

    void Update()
    {
        transform.position += Speed * way * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(ProjectileObject);
    }

    private void OnTriggerExit(Collider other)
    {
        //layer 6 is the boundry layer
        if (other.gameObject.layer == 6)
        {
            Destroy(gameObject);
        }
    }
}