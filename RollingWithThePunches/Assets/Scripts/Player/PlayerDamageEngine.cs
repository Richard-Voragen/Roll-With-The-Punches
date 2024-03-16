using System.Collections;
using UnityEngine;

public class PlayerDamageEngine : MonoBehaviour
{
    [SerializeField] private GameObject sprite;
    [SerializeField] private float health;
    [SerializeField] private float invincibilityTime; 
    [SerializeField] private bool knockback; 

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

        FindObjectOfType<SoundManager>().PlaySoundEffect("Laser");
        canTakeDamage = false;
        colorpicker.material.color = Color.red;
        i_time = 0.0f;
        if (projectileType != EffectTypes.Tutorial)
        {
            this.health -= 1f;
        }

        if (knockback)
        {
            float directionX = (attack.transform.position.x < this.transform.position.x)? 7f : -7f;
            float jumpForce = (rb.velocity.y > 0.1f) ? 0f : 4f;
            GetComponent<ADSRManager>().OverrideWithForce(new Vector2(directionX, jumpForce));
        }

        if (health <= 0.0f)
        {
            Destroy(gameObject);
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
        GetComponent<ADSRManager>().enabled = false;
        yield return new WaitForSeconds(2f);
        GetComponent<ADSRManager>().enabled = true;
    }
}
