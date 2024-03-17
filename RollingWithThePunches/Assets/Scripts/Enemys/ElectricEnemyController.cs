using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricEnemyMovement : MonoBehaviour, IEnemyController
{
    public GameObject target;
    public float speed = 4.0f;
    [SerializeField] private GameObject projectilePrefab;
    public float fireballCooldown = 1.5f;
    private float lastFireballTime = 0.0f;

    private Rigidbody2D rb;
    private bool active = true;

    private Animator animator;
    private bool stunned = false;
    private BoxCollider2D boxColl;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        boxColl = GetComponent<BoxCollider2D>();
        speed *= -1.0f;
    }

    public void SetUpProcess(GameObject targ)
    {
        this.target = targ;
        this.speed = UnityEngine.Random.Range(4f, 6f);
        this.fireballCooldown = UnityEngine.Random.Range(2.5f, 4.5f);
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
        if (stunned || target == null) return;

        if (Vector2.Distance(target.transform.position, this.transform.position) > 20f)
        {
            animator.SetBool("Punch", false);
            rb.velocity = new Vector2(0f, rb.velocity.y);
            animator.SetFloat("XVelocity", Mathf.Abs(rb.velocity.x));
            return;
        }

        if (transform.position.y < -18f) {
            Destroy(gameObject);
        }

        this.lastFireballTime += Time.deltaTime;
        if (lastFireballTime > fireballCooldown && UnityEngine.Random.Range(0f, 10f) > 7f)
        {
            this.lastFireballTime = 0.0f;
            animator.SetBool("Punch", true);
            Attack();
        }
        
        if (lastFireballTime > 0.5f)
        {
            animator.SetBool("Punch", false);
            Move();
        }

        CheckToTurn();

        animator.SetFloat("XVelocity", Mathf.Abs(rb.velocity.x));
    }

    void CheckToTurn()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + transform.right*0.25f, -Vector2.up, 1f);
        if (hits.Length < 2 && this.active)
        {
            this.speed *= -1f;
            this.active = false;
            if (this.speed < 0f)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (this.speed > 0f)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        if (hits.Length >= 2)
        {
            this.active = true;
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer >= 29 && rb.velocity.y < 0.0f)
        {
            boxColl.isTrigger = false;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerDamageEngine>().TakeDamage(this.gameObject, EffectTypes.Fire);
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
            collision.gameObject.GetComponent<PlayerDamageEngine>().TakeDamage(this.gameObject, EffectTypes.Fire);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerDamageEngine>().TakeDamage(this.gameObject, EffectTypes.Fire);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerDamageEngine>().TakeDamage(this.gameObject, EffectTypes.Fire);
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
        rb.velocity = new Vector2(0f, rb.velocity.y);

        Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y + 1.5f);

        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        EnemyFireball fireball = projectile.GetComponent<EnemyFireball>();

        if (fireball != null)
        {
            bool isPlayerLeft = target.transform.position.x < transform.position.x;
            fireball.direction = isPlayerLeft ? Vector2.left : Vector2.right;

            projectile.GetComponent<SpriteRenderer>().flipX = !isPlayerLeft;
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
