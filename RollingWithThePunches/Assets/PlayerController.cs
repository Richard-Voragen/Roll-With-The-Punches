using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Command;

public class PlayerController : MonoBehaviour
{
    private IPlayerCommand right;
    private IPlayerCommand left;
    private IPlayerCommand jump;

    bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        this.right = ScriptableObject.CreateInstance<MoveCharacterRight>();
        this.left = ScriptableObject.CreateInstance<MoveCharacterLeft>();
        this.jump = ScriptableObject.CreateInstance<MoveCharacterJump>();
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal Movement
        if (Input.GetAxis("Horizontal") > 0.01)
        {
            this.right.Execute(this.gameObject);
        }
        if (Input.GetAxis("Horizontal") < -0.01)
        {
            this.left.Execute(this.gameObject);
        }

        // Update Animator
        var animator = this.gameObject.GetComponent<Animator>();

        if (Input.GetAxis("Vertical") > 0.01 && !isJumping)
        {
            this.jump.Execute(this.gameObject);
            isJumping = true; // Set jumping flag to true
            animator.SetBool("IsJumping", true); // Trigger jumping animation
        }

        animator.SetFloat("Velocity", Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.x / 5.0f));
    }

    // Check if the player is grounded
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var animator = this.gameObject.GetComponent<Animator>();
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("IsJumping", false);
            isJumping = false; // Reset jumping flag when grounded
        }
    }
}

