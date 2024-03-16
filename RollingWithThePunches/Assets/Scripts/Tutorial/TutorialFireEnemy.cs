using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFireEnemy : MonoBehaviour, IEnemyController
{
    public GameObject target;
    [SerializeField] private GameObject projectilePrefab;
    public float fireballCooldown = 1.5f;
    private float lastFireballTime = 0.0f;

    private Rigidbody2D rb;
    private Animator animator;

    private bool stunned = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    public void SetUpProcess(GameObject targ)
    {
        this.target = targ;
    }

    public void Stun(bool stund)
    {
        this.stunned = stund;
        if (stund)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            animator.SetBool("Punch", false);
        }
    }

    private void Update()
    {
        if (stunned) return;

        this.lastFireballTime += Time.deltaTime;
        if (lastFireballTime > fireballCooldown)
        {
            this.lastFireballTime = 0.0f;
            animator.SetBool("Punch", true);
            Attack();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer >= 29 && collision.gameObject.transform.position.y <= transform.position.y + 0.3f)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerDamageEngine>().TakeDamage(this.gameObject, EffectTypes.Tutorial);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerDamageEngine>().TakeDamage(this.gameObject, EffectTypes.Tutorial);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer >= 29)
        {
            rb.gravityScale = 1;
        }
    }

    void Attack()
{
    Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y + 1.5f);

    GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
    EnemyFireball fireball = projectile.GetComponent<EnemyFireball>();

    if (fireball != null)
    {
        fireball.direction = Vector2.left;

        projectile.GetComponent<SpriteRenderer>().flipX = false;
    }
}
    public void Death()
    {
        Destroy(gameObject);
    }
}
