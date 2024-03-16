using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject target;
    public float speed = 2.0f;
    public float jumpForce = 5.0f;
    public LayerMask playerLayer;
    public GameObject projectilePrefab;
    public float fireballCooldown = 1.5f;
    private float lastFireballTime = 0.0f;

    private Rigidbody2D rb;
    private Transform player;
    private bool isMovingLeft = true;
    private bool isAttacking = false;

    private float jumpCooldown = 2f;
    private float lastJumpTime = -2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        this.lastFireballTime += Time.deltaTime;
        if (lastFireballTime > fireballCooldown && Random.Range(0, 10) < 7)
        {
            this.lastFireballTime = 0.0f;
            Attack();
        }
        
        if (Mathf.Abs(target.transform.position.x - transform.position.x) > 1.5f)
        {
            Move();
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        if (Time.time - lastJumpTime > jumpCooldown)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            lastJumpTime = Time.time;
            if (Random.value > 0.05f) //5% chance to jump
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
            isAttacking = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer >= 29)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isAttacking = false;
            player = null;
        }
        if (collision.gameObject.layer >= 29)
        {
            rb.gravityScale = 1;
        }
    }

    void Attack()
    {
        //Shoot projectile towards the player
        Debug.Log("ATTACK");

        Vector2 direction = (player.position - transform.position).normalized;
        direction.y = 0; //Ensure fireball moves strictly horizontally

        //Calculate spawn position at enemy's center
        Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y + GetComponent<Collider2D>().bounds.extents.y);

        gameObject.GetComponent<SpriteRenderer>().flipX = direction.x < 0;

        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        EnemyFireball fireball = projectile.GetComponent<EnemyFireball>();

        if (fireball != null)
        {
            fireballScript.player = target; // Set the fireball's direction towards the player

            //Flip the fireball sprite if shooting to the right
            projectile.GetComponent<SpriteRenderer>().flipX = direction.x < 0;
        }
    }

    void ChangeDirection()
    {
        isMovingLeft = !isMovingLeft;
        Debug.Log("Hit Boundary!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 31) //Current "Out of Bounds" layer #
        {
            Debug.Log("OOB");
            Destroy(gameObject); 
        }
    }
}
