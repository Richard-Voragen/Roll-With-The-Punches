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
        Vector3 spawnPosition = gameObject.transform.position + gameObject.transform.right * spawnDistance + new Vector3(0, spawnHeight, 0);
        Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);
    }
}
