using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    public float speed = 10f;
    public Vector2 direction = Vector2.right; 
    public float lifetime = 0.5f;


    void Start()
    {
        Destroy(gameObject, lifetime); 
    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
