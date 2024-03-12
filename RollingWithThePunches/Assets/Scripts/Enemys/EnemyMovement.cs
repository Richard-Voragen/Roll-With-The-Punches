using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2.0f;
    public float attackRadius = 5.0f;
    public LayerMask playerLayer;

    private Rigidbody2D rb;
    private bool isMovingLeft = true;
    private bool isAttacking = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckForPlayer();

        if (!isAttacking)
        {
            Move();
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
        isAttacking = hits.Length > 0; //Switch to attack mode if player is in radius
    }

    void Attack()
    {
        Debug.Log("Attacking Player!");
    }

    void ChangeDirection()
    {
        isMovingLeft = !isMovingLeft;
        Debug.Log("Hit Boundary!");
    }

    private void OnDrawGizmosSelected()
    {
        //To visually see the attack radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
