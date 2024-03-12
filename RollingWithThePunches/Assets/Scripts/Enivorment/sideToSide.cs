using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sideToSide : MonoBehaviour
{

    [SerializeField] private float speed = 3.5f;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(Vector2.right.x, Vector2.right.y, 0) * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        if (collision.gameObject.tag == "Bumper")
        {
            speed = -speed;
        }

        if (collision.gameObject.tag == "Player")
        {
            this.player = collision.gameObject;
        }
    }

    void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.name == "Player")
        {
            player.transform.position += new Vector3(Vector2.right.x, Vector2.right.y, 0) * speed * Time.deltaTime;
        }
    }
}
