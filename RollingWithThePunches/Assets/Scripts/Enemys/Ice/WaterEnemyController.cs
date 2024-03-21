using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEnemyMovement : MonoBehaviour, IEnemyController
{
    public GameObject target;
    public float jumpForce = 6.0f;
    [SerializeField] private GameObject projectilePrefab;
    public float waterballCooldown = 1.5f;
    private float lastWaterballTime = 3f;

    private Rigidbody2D rb;

    private float jumpCooldown = 2f;
    private float lastJumpTime = -2f;

    private Animator animator;

    private bool stunned = false;
    private BoxCollider2D boxColl;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        boxColl = GetComponent<BoxCollider2D>();
    }

    public void SetUpProcess(GameObject targ)
    {
        this.target = targ;
        this.waterballCooldown = UnityEngine.Random.Range(5f, 10f);
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

        if (Vector2.Distance(target.transform.position, this.transform.position) > 30f)
        {
            animator.SetBool("Punch", false);
            rb.velocity = new Vector2(0f, rb.velocity.y);
            return;
        }

        if (transform.position.y < -18f) {
            Destroy(gameObject);
        }

        Move();

        this.lastWaterballTime += Time.deltaTime;
        if (lastWaterballTime > waterballCooldown)
        {
            this.lastWaterballTime = 0.0f;
            animator.SetBool("Punch", true);
            Attack();
        }
        
        if (lastWaterballTime > 0.5f)
        {
            animator.SetBool("Punch", false);
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
    }

    void Move()
    {
        if (target.transform.position.x < transform.position.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer >= 29 && rb.velocity.y < 0.0f)
        {
            boxColl.isTrigger = false;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerDamageEngine>().TakeDamage(this.gameObject, EffectTypes.Water);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer >= 29 && rb.velocity.y < 0.0f)
        {
            boxColl.isTrigger = false;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerDamageEngine>().TakeDamage(this.gameObject, EffectTypes.Water);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerDamageEngine>().TakeDamage(this.gameObject, EffectTypes.Water);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerDamageEngine>().TakeDamage(this.gameObject, EffectTypes.Water);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer >= 29)
        {
            boxColl.isTrigger = true;
        }
    }

    void Attack()
{
    Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y + 1.5f);

    GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
    EnemyWaterball waterball = projectile.GetComponent<EnemyWaterball>();

    if (waterball != null)
    {
        waterball.SetAngle(target);
    }
}
    public void Death()
    {
        Destroy(gameObject);
    }
}