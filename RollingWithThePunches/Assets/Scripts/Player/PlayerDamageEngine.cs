using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerDamageEngine : MonoBehaviour
{
    [SerializeField] private GameObject sprite;
    [SerializeField] private float health;
    [SerializeField] private float invincibilityTime; 
    [SerializeField] private bool knockback;
    [SerializeField] private TMP_Text healthText;

    private bool canTakeDamage = true;
    private float i_time = 0.0f;

    private Renderer colorpicker;
    private Rigidbody2D rb; 

    private float originalSpeed;
    public float slowMultiplier = 0.5f;
    public float speedMultiplier = 1f;
    public float effectDuration = 2f;

    void Start()
    {
        colorpicker = sprite.GetComponent<Renderer>();
        rb = GetComponent<Rigidbody2D>(); 
        originalSpeed = GetComponent<ADSRManager>().speed;
        healthText.text = "" + health;
    }

    void Update()
    {
        i_time += Time.deltaTime;
        if (i_time > invincibilityTime)
        {
            canTakeDamage = true;
            colorpicker.material.color = Color.white;
        }
    }

    public bool TakeDamage(GameObject attack, EffectTypes projectileType)
    {
        if (GetComponent<ADSRManager>().overrideInput || !canTakeDamage)
        {
            return false;
        }

        if (projectileType == EffectTypes.Electric) 
        {
            FindObjectOfType<SoundManager>().PlaySoundEffect("Laser");
            canTakeDamage = false;
            colorpicker.material.color = Color.grey;
            i_time = 0.0f;
            ApplyEffect(projectileType);
            return true;
        }

        FindObjectOfType<SoundManager>().PlaySoundEffect("Hit");
        canTakeDamage = false;
        colorpicker.material.color = Color.red;
        i_time = 0.0f;
        if (projectileType != EffectTypes.Tutorial)
        {
            this.health -= 1f;
            healthText.text = "" + this.health;
        }

        if (knockback)
        {
            float directionX = (attack.transform.position.x < this.transform.position.x)? 7f : -7f;
            float jumpForce = 4f;
            if (rb.velocity.y > 0.01f)
            {
                rb.velocity = new Vector2(0f, 0f);
            }
            GetComponent<ADSRManager>().OverrideWithForce(new Vector2(directionX, jumpForce));
        }

        if (health <= 0.0f)
        {
            this.GetComponent<ADSRManager>().PlayerDied();
        }

        ApplyEffect(projectileType);
        return true;
    }

    void ApplyEffect(EffectTypes projectileType)
    {
        switch (projectileType)
        {
            case EffectTypes.Water:
                StartCoroutine(ModifySpeed(slowMultiplier, effectDuration));
                break;
            case EffectTypes.Fire:
                StartCoroutine(ModifySpeed(speedMultiplier, effectDuration));
                break;
            case EffectTypes.Electric:
                StartCoroutine(DisableMovementTemporarily());
                break;
            default:
                break;
        }
    }

    IEnumerator ModifySpeed(float multiplier, float duration)
    {
        ADSRManager playerController = GetComponent<ADSRManager>();
        playerController.speed *= multiplier;
        yield return new WaitForSeconds(duration);
        playerController.speed = originalSpeed; 
    }
    IEnumerator DisableMovementTemporarily()
    {
        GetComponent<ADSRManager>().frozen = true;
        yield return new WaitForSeconds(2f);
        GetComponent<ADSRManager>().frozen = false;
    }

    IEnumerator Died()
    {
        this.sprite.GetComponent<Animator>().SetBool("Dead", true);
        this.sprite.GetComponent<Animator>().SetTrigger("Died");
        this.GetComponent<ADSRManager>().overrideInput = true;
        yield return new WaitForSeconds(2f);
        this.GetComponent<BoxCollider2D>().isTrigger = true;
    }
}
