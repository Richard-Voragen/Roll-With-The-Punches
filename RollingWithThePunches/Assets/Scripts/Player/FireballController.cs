using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    public float speed = 10f;
    public Vector2 direction = Vector2.right; 
    [SerializeField] private float damage = 10f; 
    [SerializeField] private EffectTypes projectileType; 
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.GetComponent<EnemyDamageEngine>().TakeDamage(damage, projectileType))
            {
                Destroy(gameObject);
            }
        }
    }
}
