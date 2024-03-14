using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private GameObject player;
    private BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    public void PhaseThrough()
    {
        collider.isTrigger = true;
        Debug.Log("FALL");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision.gameObject.transform.position.y");

        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.y >= this.gameObject.transform.position.y-0.1)
            {
                collider.isTrigger = false;
            }
            else 
            {
                collider.isTrigger = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("collision.gameObject.transform.position.y");

        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.y > this.gameObject.transform.position.y)
            {
                collider.isTrigger = false;
            }
            else 
            {
                collider.isTrigger = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.player = other.gameObject;
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Player")
        {
            collider.isTrigger = true;
        }
    }
}
