using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireball : MonoBehaviour
{
    public EffectTypes projectileType = EffectTypes.Fire; 
    public float speed = 10f;
    public Vector2 direction = Vector2.right; 
    public float lifetime = 2f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime); 
    }

    void Update()
    {
        rb.velocity = direction.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerDamageEngine>().TakeDamage(this.gameObject, projectileType))
            {
                Destroy(gameObject);
            }
        }
    }
}
