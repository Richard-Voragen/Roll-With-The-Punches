using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADSRManager : MonoBehaviour
{
    [SerializeField] private GameObject sprite;
    [SerializeField] public float Speed = 6.5f;

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

    private enum Phase { Attack, Decay, Sustain, Release, None };

    private Phase CurrentPhase;

    private Rigidbody2D rb; 
    private Animator animator;

    private float velocity;
    private float jumpButtonActive = 0.15f;
    private float jumpTimer = 0.0f;
    public bool crouching = false;
    public bool IsCrouchJumping = false;
    private BoxCollider2D boxColl;

    void Awake()
    {
        this.CurrentPhase = Phase.None;
        rb = GetComponent<Rigidbody2D>(); 
        animator = this.sprite.GetComponent<Animator>();
        boxColl = GetComponent<BoxCollider2D>();
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
        Debug.Log(this.crouching);

        if (this.CurrentPhase != Phase.None && !(this.IsCrouchJumping))
        {
            var position = this.gameObject.transform.position;
            this.velocity = this.ADSREnvelope();
            this.rb.velocity = new Vector2(this.InputDirection * this.Speed * this.velocity, this.rb.velocity.y);
            this.gameObject.transform.position = position;
        }

        if (transform.position.y < -20f) {
            transform.position = new Vector3(5.5f, 24f, 0f);
            
        }

        animator.SetFloat("YVelocity", rb.velocity.y);
        animator.SetBool("Grounded", !IsJumping);
    }

    void CheckMovementInput()
    {
        float input = Input.GetAxis("Horizontal");
        if (animator.GetBool((Animator.StringToHash("Punch")))){
            CurrentPhase = Phase.Release;
        }

        if (Input.GetAxis("Vertical") < -0.1f && IsJumping == false)
        {
            CurrentPhase = Phase.Release;
            animator.SetBool("Crouch", true);
            this.crouching = true;
            Debug.Log(this.boxColl.offset.x);
            this.boxColl.size = new Vector2(0.8f, 1f);
            this.boxColl.offset = new Vector2(this.boxColl.offset.x, 0.495f);
        }
        else if (this.crouching && Input.GetAxis("Vertical") >= -0.1f && IsJumping == false)
        {
            this.ResetTimers();
            CurrentPhase = Phase.Attack;
            animator.SetBool("Crouch", false);
            this.crouching = false;
            this.boxColl.size = new Vector2(1.096168f, 1.871117f);
            this.boxColl.offset = new Vector2(this.boxColl.offset.x, 0.9351177f);
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
            if (this.crouching) 
            {
                float directionX = Mathf.Sign(gameObject.transform.localScale.x);
                rb.AddForce(new Vector2(directionX * 10.0f, 6.0f), ForceMode2D.Impulse);
                this.IsCrouchJumping = true;
            }
            else
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
        if (collision.gameObject.layer >= 29 && collision.gameObject.transform.position.y <= this.gameObject.transform.position.y + 0.3f)
        {
            IsJumping = false;
            this.IsCrouchJumping = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer >= 29)
        {
            IsJumping = false;
            this.IsCrouchJumping = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer >= 29)
        {
            IsJumping = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer >= 29)
        {
            IsJumping = true;
        }
    }

    public void SetPhaseRelease() 
    {
        this.CurrentPhase = Phase.Release;
    }
}