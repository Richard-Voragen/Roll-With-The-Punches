using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaterball : MonoBehaviour
{
    public EffectTypes projectileType = EffectTypes.Water; 
    private GameObject player;
    public float speed = 5f;
    public float lifetime = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime); 
    }

    public void SetAngle(GameObject target) 
    {
        this.player = target;

        Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y + 0.2f);
        float angle = Mathf.Atan2(playerPosition.y + 1f - this.transform.position.y, playerPosition.x - this.transform.position.x ) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1f);
    }

    void Update()
    {
        Vector2 playerPosition = new Vector2(player.transform.position.x, player.transform.position.y + 0.2f);
        float angle = Mathf.Atan2(playerPosition.y + 1f - this.transform.position.y, playerPosition.x - this.transform.position.x ) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed/2 * Time.deltaTime);
        transform.position += transform.right * speed * Time.deltaTime;
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
        if (collision.gameObject.layer >= 29)
        {
            Destroy(gameObject);
        }
    }
}
