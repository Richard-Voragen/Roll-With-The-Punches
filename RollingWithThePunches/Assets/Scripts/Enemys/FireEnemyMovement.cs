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

    public bool stunned = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    public void Stun(bool stund)
    {
        this.stunned = stund;
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
        
        if (Mathf.Abs(target.transform.position.x - transform.position.x) > 1.5f && lastFireballTime > 0.5f)
        {
            animator.SetBool("Punch", false);
            Move();
        }
        else if (Mathf.Abs(target.transform.position.x - transform.position.x) <= 1.5f)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            animator.SetBool("Punch", true);
            if (lastFireballTime > fireballCooldown && UnityEngine.Random.Range(0, 10) > 7)
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
            if (UnityEngine.Random.value < 0.01f) //5% chance to jump
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
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
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
        if (collision.gameObject.layer >= 29)
        {
            //rb.gravityScale = 0;
            //rb.velocity = new Vector2(rb.velocity.x, 0f);
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
        //Calculate spawn position at enemy's center
        Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y + 1.5f);

        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        EnemyFireball fireball = projectile.GetComponent<EnemyFireball>();

        if (fireball != null)
        {
            if (gameObject.GetComponent<SpriteRenderer>().flipX == true)
            {
                fireball.direction = -transform.right;
                projectile.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else 
            {
                fireball.direction = transform.right;
            }
        }
    }
    
    void OnDrawGizmos()
    {
        // Draw a green box at the transform's position
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        Gizmos.color = Color.green;
        Gizmos.DrawCube((Vector2)transform.position + box.offset, box.size);
    }
}
