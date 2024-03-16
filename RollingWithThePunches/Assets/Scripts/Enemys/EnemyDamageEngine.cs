using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageEngine : MonoBehaviour
{
    [SerializeField] private GameObject sprite;
    [SerializeField] private EffectTypes userType; 
    [SerializeField] private float health;
    [SerializeField] private float invincibilityTime; 
    [SerializeField] private bool knockback; 

    private bool canTakeDamage = false;
    private float i_time = 0.0f;

    private Renderer colorpicker;
    private Rigidbody2D rb; 

    void Start()
    {
        colorpicker = this.sprite.GetComponent<Renderer>();
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        i_time += Time.deltaTime;
        if (i_time > invincibilityTime)
        {
            this.canTakeDamage = true;
            this.colorpicker.material.color = Color.white;
        }
    }

    public bool TakeDamage(float damage, EffectTypes projectileType)
    {
        if (GetComponent<ADSRManager>().IsCrouchJumping || this.canTakeDamage == false)
        {
            return false;
        }
        FindObjectOfType<SoundManager>().PlaySoundEffect("Laser");
        this.canTakeDamage = false;
        this.colorpicker.material.color = Color.red;
        this.i_time = 0.0f;
        this.health -= 1f;
        if (this.health <= 0.0f)
        {
            Destroy(gameObject);
        }

        if (knockback)
        {
            float directionX = Mathf.Sign(gameObject.transform.localScale.x);
            rb.AddForce(new Vector2(directionX * -6.0f, 3.5f), ForceMode2D.Impulse);
            GetComponent<ADSRManager>().IsCrouchJumping = true;
        }
        return true;
    }

    private static float[,] damageTable = { 
        {0.25f, 1f, 0f},
        {0.25f, 0.5f, 0.5f},
        {0.5f, 0.25f, 0f},
    };

    public static float TypeFactor(EffectTypes userType, EffectTypes projectileType)
    {
        return damageTable[(int)userType, (int)projectileType];
    }
}
