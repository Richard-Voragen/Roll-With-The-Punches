using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADSRManager : MonoBehaviour
{
    [SerializeField] private GameObject sprite;
    [SerializeField] private bool ShowPhases = true;

    [SerializeField] private float Speed = 6.5f;

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
    private IPlayerCommand shoot;

    void Start()
    {
        this.CurrentPhase = Phase.None;
        rb = GetComponent<Rigidbody2D>(); 
        animator = this.sprite.GetComponent<Animator>();
        this.shoot = GetComponent<ShootFireball>();
    }

    void Update()
    {
        CheckMovementInput();
        CheckJumpInput();
        CheckAttackInput();

        if (this.InputDirection < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (this.InputDirection > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (this.CurrentPhase != Phase.None)
        {
            var position = this.gameObject.transform.position;
            this.velocity = this.ADSREnvelope();
            this.rb.velocity = new Vector2(this.InputDirection * this.Speed * this.velocity, this.rb.velocity.y);
            this.gameObject.transform.position = position;
        }

        if (this.ShowPhases)
        {
            this.SetColorByPhase();
        }

        animator.SetFloat("YVelocity", rb.velocity.y);
        animator.SetBool("Grounded", !IsJumping);
    }

    void CheckAttackInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            this.shoot.Execute(this.gameObject);
            animator.SetBool("Punch", true);
            this.InputDirection = 0f;
        }

        if (Input.GetButton("Fire1"))
        {
            this.shoot.Execute(this.gameObject);
            this.velocity = 0f;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            this.shoot.Execute(this.gameObject);
            animator.SetBool("Punch", false);
            this.velocity = 0f;
        }
    }

    void CheckMovementInput()
    {
        if (this.velocity < 0.1f && this.CurrentPhase != Phase.Attack && Input.GetAxis("Horizontal") > 0.01)
        {
            this.ResetTimers();
            this.CurrentPhase = Phase.Attack;
            animator.SetBool("Running", true);
        }

        if (this.InputDirection > 0.0f && Input.GetAxis("Horizontal") < 0.1f)
        {
            this.CurrentPhase = Phase.Release;
            animator.SetBool("Running", false);
        }

        if (this.velocity < 0.1f && this.CurrentPhase != Phase.Attack && Input.GetAxis("Horizontal") < -0.01)
        {
            this.ResetTimers();
            this.CurrentPhase = Phase.Attack;
            animator.SetBool("Running", true);
        }

        if (this.InputDirection < 0.0f && Input.GetAxis("Horizontal") > -0.1f)
        {
            this.CurrentPhase = Phase.Release;
            animator.SetBool("Running", false);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.InputDirection = -1.0f;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            this.InputDirection = -1.0f;
            this.CurrentPhase = Phase.Release;
            animator.SetBool("Running", false);
        }

        this.InputDirection = Input.GetAxis("Horizontal");
    }

    void CheckJumpInput()
    {
        if (Input.GetButtonDown("Jump") && !IsJumping)
        {
            rb.AddForce(new Vector2(0, JumpForce/4.0f), ForceMode2D.Impulse);
            this.jumpTimer = 0.0f;
            IsJumping = true;
        }

        if (Input.GetButton("Jump") && this.jumpTimer < this.jumpButtonActive)
        {
            this.jumpTimer += Time.deltaTime;
            rb.AddForce(new Vector2(0, (JumpForce)*(Time.deltaTime/this.jumpButtonActive)), ForceMode2D.Impulse);
        }
    }

    //Not super happy with the current movement code but it works. A bit over complicated, plan on changing to "Horizontal" so we can use controllers.

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

    private void SetColorByPhase()
    {
        var color = Color.white;

        if(Phase.Attack == this.CurrentPhase)
        {
            color = Color.green;
        }
        else if (Phase.Decay == this.CurrentPhase) 
        {
            color = Color.yellow;
        }
        else if (Phase.Sustain == this.CurrentPhase)
        {
            color = Color.blue;
        }
        else if (Phase.Release == this.CurrentPhase)
        {
            color = Color.grey;
        }
        this.sprite.GetComponent<Renderer>().material.color = color;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsJumping = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsJumping = true;
        }
    }
}