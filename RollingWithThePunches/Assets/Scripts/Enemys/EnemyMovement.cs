using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public float jumpForce = 5.0f;
    public LayerMask playerLayer;
    public GameObject projectilePrefab;
    public float fireballCooldown = 1.5f;
    private float lastFireballTime = -5f;

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
        if (isAttacking)
        {
            Attack();
        }
        else
        {
            Move();
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
        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isAttacking = true;
            player = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isAttacking = false;
            player = null;
        }
    }

    void Attack()
    {
        //Shoot projectile towards the player
        Debug.Log("ATTACK");
        if (Time.time - lastFireballTime > fireballCooldown && projectilePrefab != null && player != null)
    {
        Vector2 direction = (player.position - transform.position).normalized;
        direction.y = 0; //Ensure fireball moves strictly horizontally

        //Calculate spawn position at enemy's center
        Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y + GetComponent<Collider2D>().bounds.extents.y);

        gameObject.GetComponent<SpriteRenderer>().flipX = direction.x < 0;

        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        EnemyFireball fireball = projectile.GetComponent<EnemyFireball>();

        if (fireball != null)
        {
            fireball.direction = direction; // Set the fireball's direction towards the player

            //Flip the fireball sprite if shooting to the right
            projectile.GetComponent<SpriteRenderer>().flipX = direction.x < 0;
        }

        lastFireballTime = Time.time; //Update the time a fireball was last shot
    }


        //Random jump
        if (Time.time - lastJumpTime > jumpCooldown)
        {
            if (Random.value > 0.05f) //5% chance to jump
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
            Debug.Log("OOB");
            Destroy(gameObject); 
        }
    }
}
