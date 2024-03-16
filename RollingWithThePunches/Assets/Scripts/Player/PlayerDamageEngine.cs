using System.Collections;
using UnityEngine;

public class PlayerDamageEngine : MonoBehaviour
{
    [SerializeField] private GameObject sprite;
    [SerializeField] private float health;
    [SerializeField] private float invincibilityTime; 

    private bool canTakeDamage = true;
    private float i_time = 0.0f;

    private Renderer colorpicker;
    private Rigidbody2D rb; 

    private float originalSpeed;
    public float slowMultiplier = 0.5f;
    public float speedMultiplier = 1.5f;
    public float effectDuration = 2f;

    void Start()
    {
        colorpicker = sprite.GetComponent<Renderer>();
        rb = GetComponent<Rigidbody2D>(); 
        originalSpeed = GetComponent<ADSRManager>().Speed; 
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

    public bool TakeDamage(EffectTypes projectileType)
    {
        if (GetComponent<ADSRManager>().IsCrouchJumping || !canTakeDamage)
        {
            return false;
        }

        FindObjectOfType<SoundManager>().PlaySoundEffect("Laser");
        canTakeDamage = false;
        colorpicker.material.color = Color.red;
        i_time = 0.0f;
        health -= 1f;

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
                Destroy(gameObject);
                break;
        }
    }

    IEnumerator ModifySpeed(float multiplier, float duration)
    {
        ADSRManager playerController = GetComponent<ADSRManager>();
        playerController.Speed *= multiplier;
        yield return new WaitForSeconds(duration);
        playerController.Speed = originalSpeed; 
    }
        IEnumerator DisableMovementTemporarily()
    {
        GetComponent<ADSRManager>().enabled = false;
        yield return new WaitForSeconds(2f);
        GetComponent<ADSRManager>().enabled = true;
    }
}
