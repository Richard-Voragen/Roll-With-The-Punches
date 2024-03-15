using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private GameObject player;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void PhaseThrough()
    {
        boxCollider.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.y >= this.gameObject.transform.position.y - 0.3f)
            {
                boxCollider.isTrigger = false;
            }
            else 
            {
                boxCollider.isTrigger = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.y > this.gameObject.transform.position.y)
            {
                boxCollider.isTrigger = false;
            }
            else 
            {
                boxCollider.isTrigger = true;
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
            boxCollider.isTrigger = true;
        }
    }
}
