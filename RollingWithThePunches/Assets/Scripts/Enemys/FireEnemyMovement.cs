using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnemyMovement : MonoBehaviour, IEnemyController
{
    public GameObject target;
    public float speed = 2.0f;
    public float jumpForce = 5.0f;
    [SerializeField] private GameObject projectilePrefab;
    public float fireballCooldown = 1.5f;
    private float lastFireballTime = 0.0f;

    private Rigidbody2D rb;

    private float jumpCooldown = 2f;
    private float lastJumpTime = -2f;

    private Animator animator;

    private bool stunned = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
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
        if (lastFireballTime > fireballCooldown && UnityEngine.Random.Range(0, 10) > 7)
        {
            this.lastFireballTime = 0.0f;
            animator.SetBool("Punch", true);
            Attack();
        }
        
        if (Mathf.Abs(target.transform.position.x - transform.position.x) > 1f && lastFireballTime > 0.5f)
        {
            animator.SetBool("Punch", false);
            Move();
        }
        else if (Mathf.Abs(target.transform.position.x - transform.position.x) <= 1f)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            animator.SetBool("Punch", true);
            if (lastFireballTime > fireballCooldown)
            {
                this.lastFireballTime = 0.0f;
                animator.SetBool("Punch", true);
                Attack();
            }
        }


        if (Time.time - lastJumpTime > jumpCooldown && target.transform.position.y > transform.position.y + 2f)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            lastJumpTime = Time.time;
            if (UnityEngine.Random.value < 0.01f)
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                lastJumpTime = Time.time;
            }
        }

        animator.SetFloat("XVelocity", Mathf.Abs(rb.velocity.x));
    }

    void Move()
{
    if (target.transform.position.x < transform.position.x)
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y);
        gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }
    else
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
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
            collision.gameObject.GetComponent<PlayerDamageEngine>().TakeDamage(EffectTypes.Fire);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerDamageEngine>().TakeDamage(EffectTypes.Fire);
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
        bool isPlayerLeft = target.transform.position.x < transform.position.x;
        fireball.direction = isPlayerLeft ? Vector2.left : Vector2.right;

        projectile.GetComponent<SpriteRenderer>().flipX = isPlayerLeft;
    }
}
    public void Death()
    {
        Destroy(gameObject);
    }
}
