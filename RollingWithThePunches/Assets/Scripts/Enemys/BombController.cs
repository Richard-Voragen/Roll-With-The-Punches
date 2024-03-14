using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float damper = 4f;
    public float lifetime = 10f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime); 
    }

    void Update()
    {
        this.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Rad2Deg*Mathf.Atan(rb.velocity.x/Mathf.Abs(rb.velocity.y)));
        if (rb.velocity.x > 0.5)
        {
            rb.velocity = new Vector2(rb.velocity.x-(damper*Time.deltaTime), rb.velocity.y);
        }
        else if (rb.velocity.x < -0.5)
        {
            rb.velocity = new Vector2(rb.velocity.x+(damper*Time.deltaTime), rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
