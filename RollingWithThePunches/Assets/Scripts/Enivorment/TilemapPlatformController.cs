using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePlatformController : MonoBehaviour
{
    private GameObject player;
    private CompositeCollider2D tileCollider;

    // Start is called before the first frame update
    void Start()
    {
        tileCollider = GetComponent<CompositeCollider2D>();
    }

    public void PhaseThrough()
    {
        tileCollider.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.y >= this.gameObject.transform.position.y-0.1)
            {
                tileCollider.isTrigger = false;
            }
            else 
            {
                tileCollider.isTrigger = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.y > this.gameObject.transform.position.y)
            {
                tileCollider.isTrigger = false;
            }
            else 
            {
                tileCollider.isTrigger = true;
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
            tileCollider.isTrigger = true;
        }
    }
}
