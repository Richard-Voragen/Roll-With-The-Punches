using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Command;

public class PlayerController : MonoBehaviour
{
    private IPlayerCommand right;
    private IPlayerCommand left;
    private IPlayerCommand jump;

    private IPlayerCommand shoot;

    public GameObject fireballPrefab; 
    public float fireballSpawnDistance = 1f;

    bool isJumping = false;
    
        void Start()
    {
        this.right = ScriptableObject.CreateInstance<MoveCharacterRight>();
        this.left = ScriptableObject.CreateInstance<MoveCharacterLeft>();
        this.jump = ScriptableObject.CreateInstance<MoveCharacterJump>();
        this.shoot = new ShootFireball(fireballPrefab, fireballSpawnDistance);
    }

    void Update()
    {
        // Horizontal Movement
        if (Input.GetAxis("Horizontal") > 0.01)
        {
            this.right.Execute(this.gameObject);
        }
        else if (Input.GetAxis("Horizontal") < -0.01)
        {
            this.left.Execute(this.gameObject);
        }

        // Jumping
        var animator = this.gameObject.GetComponent<Animator>();
        if (Input.GetAxis("Vertical") > 0.01 && !isJumping)
        {
            this.jump.Execute(this.gameObject);
            isJumping = true; // Set jumping flag to true
            animator.SetBool("IsJumping", true); // Trigger jumping animation
        }

        // Shooting
        if (Input.GetKeyDown(KeyCode.E))
        {
            this.shoot.Execute(this.gameObject);
        }

        // Update Animator Velocity
        animator.SetFloat("Velocity", Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.x / 5.0f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var animator = this.gameObject.GetComponent<Animator>();
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("IsJumping", false);
            isJumping = false; // Reset jumping flag when grounded
        }
    }

    private class ShootFireball : IPlayerCommand
    {
        private readonly GameObject fireballPrefab;
        private readonly float spawnDistance;

        public ShootFireball(GameObject prefab, float spawnDistance)
        {
            this.fireballPrefab = prefab;
            this.spawnDistance = spawnDistance;
        }

        public void Execute(GameObject gameObject)
        {
            Vector3 spawnPosition = gameObject.transform.position + gameObject.transform.right * spawnDistance;
            Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);
        }
    }
}