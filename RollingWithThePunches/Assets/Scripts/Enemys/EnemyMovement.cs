using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public float jumpForce = 5.0f;
    public float attackRadius = 5.0f;
    public LayerMask playerLayer;
    public GameObject projectilePrefab;

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
        CheckForPlayer();

        if (!isAttacking)
        {
            CheckForPlayer();
        }
        else
        {
            Attack();
        }
    }

    void Move()
    {
        if (isMovingLeft)
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

    void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRadius, playerLayer);
        if (hits.Length > 0)
        {
            isAttacking = true;
            player = hits[0].transform;
        }
        else
        {
            isAttacking = false;
            player = null;
        }
    }

    void Attack()
    {
        // Shoot projectile towards the player
        Debug.Log("ATTACK");
        if (projectilePrefab != null && player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = direction * speed;

            // Flip projectile if shooting to the left
            projectile.GetComponent<SpriteRenderer>().flipX = direction.x < 0;
        }

        // Random jump
        if (Time.time - lastJumpTime > jumpCooldown)
        {
            if (Random.value > 0.1f) // 50% chance to jump
            {
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                lastJumpTime = Time.time;
            }
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
            Destroy(gameObject); 
        }
    }

    private void OnDrawGizmosSelected()
    {
        //To visually see the attack radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
