using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADSRManager : MonoBehaviour
{
    [SerializeField] private GameObject sprite;
    [SerializeField] public float speed = 6.5f;

    [SerializeField] private float JumpForce = 5.0f;

    [SerializeField] private float AttackDuration = 0.1f;
    [SerializeField] private AnimationCurve Attack;

    [SerializeField] private float SustainDuration = 5.0f;
    [SerializeField] private AnimationCurve Sustain;

    [SerializeField] private float ReleaseDuration = 0.3f;
    [SerializeField] private AnimationCurve Release;

    private float AttackTimer;
    private float SustainTimer;
    private float ReleaseTimer;

    private float InputDirection = 0.0f;
    private bool IsJumping = false;

    public enum Phase { Attack, Decay, Sustain, Release, None };

    public Phase CurrentPhase;

    private Rigidbody2D rb; 
    private Animator animator;

    private float velocity;
    private float jumpButtonActive = 0.15f;
    private float jumpTimer = 0.0f;
    [HideInInspector] public bool crouching = false;
    private bool IsCrouchJumping = false;
    public bool overrideInput = false;
    private Vector2 overrideForce;
    private BoxCollider2D boxColl;
    private BoxCollider2D defaultBoxColl;

    public bool frozen = false;

    void Awake()
    {
        this.CurrentPhase = Phase.None;
        rb = GetComponent<Rigidbody2D>(); 
        animator = this.sprite.GetComponent<Animator>();
        boxColl = GetComponent<BoxCollider2D>();
        this.defaultBoxColl = this.boxColl;
    }

    void Update()
    {
        CheckMovementInput();
        CheckJumpInput();

        if (this.InputDirection < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (this.InputDirection > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (transform.position.y < -18f) {
            this.overrideInput = false;
            boxColl.isTrigger = false;
            transform.position = new Vector3(5.5f, 24f, 0f);
        }

        animator.SetFloat("YVelocity", rb.velocity.y);
        animator.SetBool("Grounded", !IsJumping);
    }

    void LateUpdate() 
    {
        if (!(this.overrideInput))
        {
            var position = this.gameObject.transform.position;
            this.velocity = this.ADSREnvelope();
            this.rb.velocity = new Vector2(this.InputDirection * this.speed * this.velocity, this.rb.velocity.y);
            this.gameObject.transform.position = position;
        }
    }

    void CheckMovementInput()
    {
        float input = Input.GetAxis("Horizontal");
        if (frozen) input = 0.0f;
        if (animator.GetBool((Animator.StringToHash("Punch")))){
            CurrentPhase = Phase.Release;
        }

        if (Input.GetAxis("Vertical") < -0.1f && IsJumping == false)
        {
            CurrentPhase = Phase.Release;
            animator.SetBool("Crouch", true);
            this.crouching = true;
            this.boxColl.size = new Vector2(0.8f, 1f);
            this.boxColl.offset = new Vector2(this.boxColl.offset.x, 0.495f);
        }
        else if (this.crouching && Input.GetAxis("Vertical") >= -0.1f && IsJumping == false)
        {
            this.ResetTimers();
            CurrentPhase = Phase.Attack;
            animator.SetBool("Crouch", false);
            this.crouching = false;
            this.boxColl.size = this.defaultBoxColl.size;
            this.boxColl.offset = this.defaultBoxColl.offset;
        }

        if (this.velocity < 0.1f && this.CurrentPhase != Phase.Attack && input > 0.01)
        {
            this.ResetTimers();
            this.CurrentPhase = Phase.Attack;
            animator.SetBool("Running", true);
        }

        if (this.InputDirection > 0.0f && input < 0.1f)
        {
            this.CurrentPhase = Phase.Release;
            animator.SetBool("Running", false);
        }

        if (this.velocity < 0.1f && this.CurrentPhase != Phase.Attack && input < -0.01)
        {
            this.ResetTimers();
            this.CurrentPhase = Phase.Attack;
            animator.SetBool("Running", true);
        }

        if (this.InputDirection < 0.0f && input > -0.1f)
        {
            this.CurrentPhase = Phase.Release;
            animator.SetBool("Running", false);
        }

        this.InputDirection = input;
    }

    void CheckJumpInput()
    {
        if (Input.GetButtonDown("Jump") && !IsJumping)
        {
            if (this.crouching && this.IsJumping == false) 
            {
                float directionX = Mathf.Sign(gameObject.transform.localScale.x);
                OverrideWithForce(new Vector2(directionX * 12.0f, 6.0f));
            }
            else if (this.crouching == false)
            {
                rb.AddForce(new Vector2(0, JumpForce/4.0f), ForceMode2D.Impulse);
                this.jumpTimer = 0.0f;
                this.IsJumping = true;
            }
        }

        if (Input.GetButton("Jump") && this.jumpTimer < this.jumpButtonActive && !this.crouching)
        {
            this.jumpTimer += Time.deltaTime;
            rb.AddForce(new Vector2(0, (JumpForce)*(Time.deltaTime/this.jumpButtonActive)), ForceMode2D.Impulse);
        }
    }

    float ADSREnvelope()
    {
        float velocity = 0.0f;

        if(Phase.Attack == this.CurrentPhase)
        {
            velocity = this.Attack.Evaluate(this.AttackTimer / this.AttackDuration);
            this.AttackTimer += Time.deltaTime;
            if(this.AttackTimer > this.AttackDuration)
            {
                this.CurrentPhase = Phase.Sustain;
            }
        } 
        else if (Phase.Sustain == this.CurrentPhase)
        {
            velocity = this.Sustain.Evaluate(this.SustainTimer / this.SustainDuration);
            this.SustainTimer += Time.deltaTime;
        } 
        else if (Phase.Release == this.CurrentPhase)
        {
            velocity = this.Release.Evaluate(this.ReleaseTimer / this.ReleaseDuration);
            this.ReleaseTimer += Time.deltaTime;
            if (this.ReleaseTimer > this.ReleaseDuration)
            {
                this.CurrentPhase = Phase.None;
            }
        }
        return velocity;
    }

    private void ResetTimers()
    {
        this.AttackTimer = 0.0f;
        this.SustainTimer = 0.0f;
        this.ReleaseTimer = 0.0f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer >= 29)
        {
            if (rb.velocity.y < -0.1f)
            {
                IsJumping = false;
                this.overrideInput = false;
                boxColl.isTrigger = false;
            }
            if (collision.gameObject.layer == 29)
            {
                boxColl.isTrigger = false;
                IsJumping = false;
                this.overrideInput = false;
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 30)
        {
            if (rb.velocity.y < -0.1f)
            {
                IsJumping = false;
                this.overrideInput = false;
                boxColl.isTrigger = false;
            }
        }
        else if (collision.gameObject.layer == 29)
        {
            boxColl.isTrigger = false;
            IsJumping = false;
            this.overrideInput = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer >= 29)
        {
            IsJumping = false;
            this.overrideInput = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer >= 29 && !crouching)
        {
            IsJumping = false;
            this.overrideInput = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer >= 29)
        {
            IsJumping = true;
            boxColl.isTrigger = true;
            if (this.IsCrouchJumping)
            {
                this.overrideInput = true;
                rb.AddForce(this.overrideForce, ForceMode2D.Impulse);
                this.IsCrouchJumping = false;
            }
        }
    }

    public void OverrideWithForce(Vector2 force) {
        this.overrideForce = new Vector2(force.x, 0f);
        rb.AddForce(new Vector2(0f, force.y), ForceMode2D.Impulse);
        this.IsCrouchJumping = true;
        //boxColl.isTrigger = true;
    }

    public void SetPhaseRelease() 
    {
        this.CurrentPhase = Phase.Release;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube((Vector2)transform.position, boxColl.size);
        Debug.Log("HELLo");
    }
}