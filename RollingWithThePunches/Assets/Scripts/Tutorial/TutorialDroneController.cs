using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDroneController : MonoBehaviour, IEnemyController
{
    [SerializeField] private GameObject target;
    public float floatHeight;
    public float damping;
    [SerializeField] private float speed = 6.5f;
    [SerializeField] private float AttackDuration = 0.1f;
    [SerializeField] private AnimationCurve Attack;
    [SerializeField] private float ReleaseDuration = 0.3f;
    [SerializeField] private AnimationCurve Release;

    private float AttackTimer;
    private float ReleaseTimer;
    private Rigidbody2D rb;
    private float InputDirection = -1f;
    private enum Phase { Attack, Decay, Sustain, Release, None };

    private Phase CurrentPhase;

    private bool crashed = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetUpProcess(GameObject targ)
    {
        this.target = targ;
        this.speed = Random.Range(6f, 9f);
    }

    public void Stun(bool stund)
    {
    }

    void Update()
    {
        if (this.InputDirection > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (this.InputDirection < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (this.CurrentPhase != Phase.None && this.crashed == false)
        {
            var position = this.gameObject.transform.position;
            this.rb.velocity = new Vector2(this.InputDirection * this.speed * this.ADSREnvelope(), this.rb.velocity.y);
            this.gameObject.transform.position = position;
        }

        if (this.crashed == false) 
        {
            ChangeHeight();
        }
    }

    void ChangeHeight()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position - Vector3.up, -Vector2.up);

        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Ground"))
            {
                float distance = Mathf.Abs(hit.point.y - transform.position.y);


                // Apply the force to the rigidbody.
                if (distance < this.floatHeight)
                {
                    rb.velocity = new Vector2(rb.velocity.x, damping);
                    break;
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, -damping);
                }
            }
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
            velocity = 1f;
            if (this.InputDirection > 0.1f && this.transform.position.x > this.target.transform.position.x)
            {
                this.CurrentPhase = Phase.Release;
            }
            else if (this.InputDirection < -0.1f && this.transform.position.x < this.target.transform.position.x)
            {
                this.CurrentPhase = Phase.Release;
            }
        } 
        else if (Phase.Release == this.CurrentPhase)
        {
            velocity = this.Release.Evaluate(1 - this.ReleaseTimer / this.ReleaseDuration);
            this.ReleaseTimer += Time.deltaTime;
            if (this.ReleaseTimer > this.ReleaseDuration)
            {
                ResetTimers();
                this.CurrentPhase = Phase.Attack;
                this.InputDirection *= -1;
            }
        }
        return velocity;
    }

    private void ResetTimers()
    {
        this.AttackTimer = 0.0f;
        this.ReleaseTimer = 0.0f;
    }

    public void Death()
    {
        var impulse = (60 * Mathf.Deg2Rad) * rb.inertia;
        rb.AddTorque(impulse, ForceMode2D.Impulse);
        rb.gravityScale = 1f;
        this.crashed = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer >= 29 && this.crashed)
        {
            FindObjectOfType<SoundManager>().PlaySoundEffect("Explosion");
            Destroy(gameObject);
        }
    }
}
