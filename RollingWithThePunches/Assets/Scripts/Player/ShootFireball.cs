using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFireball : MonoBehaviour, IPlayerCommand
{
    [SerializeField] private GameObject sprite;
    [SerializeField] private GameObject fireballPrefab; 
    [SerializeField] private float spawnDistance = 1f;
    [SerializeField] private float spawnHeight = 0.5f;
    [SerializeField] private float PunchDuration = 0.3f;

    private float punchTimer = 0.0f;
    private Animator animator;
    private Rigidbody2D rb; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        animator = this.sprite.GetComponent<Animator>();
    }

    void LateUpdate()
    {
        CheckAttackInput();
    }

    void CheckAttackInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (this.punchTimer > this.PunchDuration)
            {
                Execute(this.gameObject);
                animator.SetBool("Punch", true);
            }
            this.punchTimer = 0.0f;
        }

        else if (Input.GetButton("Fire1"))
        {
            if(this.punchTimer > this.PunchDuration)
            {
                animator.SetBool("Punch", true);
                Execute(this.gameObject);
                this.punchTimer = 0.0f;
            }
        }

        else if (Input.GetButtonUp("Fire1"))
        {
            animator.SetBool("Punch", false);
            this.punchTimer = 0.0f;
        }

        this.punchTimer += Time.deltaTime;
    }

    public void Execute(GameObject gameObject)
    {
        Vector2 spawnOffset = new Vector2(Mathf.Sign(gameObject.transform.localScale.x) * spawnDistance, spawnHeight);
    Vector3 spawnPosition = gameObject.transform.position + new Vector3(spawnOffset.x, spawnOffset.y, 0);
    GameObject fireball = Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);

    Fireball fireballScript = fireball.GetComponent<Fireball>();
    if (fireballScript != null)
    {
        //Set the direction for the fireball
        float directionX = Mathf.Sign(gameObject.transform.localScale.x);
        fireballScript.direction = new Vector2(directionX, 0);

        //Flip the fireball sprite when shooting left
        if (directionX < 0)
        {
            fireball.transform.localScale = new Vector3(-1 * fireball.transform.localScale.x, fireball.transform.localScale.y, fireball.transform.localScale.z);
        }
    }
    }
}
