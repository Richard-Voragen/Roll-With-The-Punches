using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFireball : MonoBehaviour, IPlayerCommand
{
    [SerializeField] private GameObject sprite;
    [SerializeField] private GameObject fireballPrefab; 
    [SerializeField] private GameObject waterballPrefab; 
    [SerializeField] private GameObject electricballPrefab; 
    [SerializeField] private float spawnDistance = 1f;
    [SerializeField] private float spawnHeight = 0.5f;
    [SerializeField] private float PunchDuration = 0.3f;

    private float punchTimer = 0.0f;
    private Animator animator;
    private Rigidbody2D rb; 
    private GameObject[] prefabs = new GameObject[3];
    private String[] sounds = new string[3];
    private int currentPrefab = 0;
    private float currentSpawn;
    private float currentHeight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        animator = this.sprite.GetComponent<Animator>();
        this.currentSpawn = spawnDistance;
        this.currentHeight = spawnHeight;
        prefabs[0] = fireballPrefab;
        prefabs[1] = waterballPrefab;
        prefabs[2] = electricballPrefab;
        sounds[0] = "Fireball";
        sounds[1] = "Waterball";
        sounds[2] = "Laser";
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
                if (this.currentPrefab == 2) {
                    this.currentSpawn += 1;
                }
                animator.SetBool("Punch", true);
                Execute(this.gameObject);
                this.punchTimer = 0.0f;
            }
        }

        else if (Input.GetButtonUp("Fire1"))
        {
            currentSpawn = spawnDistance;
            animator.SetBool("Punch", false);
            this.punchTimer = 0.0f;
        }

        this.punchTimer += Time.deltaTime;

        if (Input.GetButtonDown("Fire2"))
        {
            currentPrefab -= 1;
            currentHeight = spawnHeight;
            if (currentPrefab < 0) 
            {
                currentPrefab = 2;
                currentHeight = 0;
            }
        }
    }

    public void Execute(GameObject gameObject)
    {
        Vector2 spawnOffset = new Vector2(Mathf.Sign(gameObject.transform.localScale.x) * currentSpawn, currentHeight);
        Vector3 spawnPosition = gameObject.transform.position + new Vector3(spawnOffset.x, spawnOffset.y, 0);
        GameObject fireball = Instantiate(prefabs[currentPrefab], spawnPosition, Quaternion.identity);
        FindObjectOfType<SoundManager>().PlaySoundEffect(sounds[currentPrefab]);

        FireballController fireballScript = fireball.GetComponent<FireballController>();
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
