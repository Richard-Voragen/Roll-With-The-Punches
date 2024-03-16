using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageEngine : MonoBehaviour
{
    [SerializeField] private EffectTypes userType; 
    [SerializeField] private float health;
    [SerializeField] private float invincibilityTime; 
    [SerializeField] private float stunTime; 

    private bool canTakeDamage = false;
    private float i_time = 0.0f;
    private float stunTimer = 999.0f;
    private Renderer colorpicker;
    private Rigidbody2D rb; 

    void Start()
    {
        colorpicker = this.GetComponent<Renderer>();
        rb = GetComponent<Rigidbody2D>(); 

        //IEnemyController[] tests = ConvertToArray<IEnemyController>(GetComponents(typeof(IEnemyController)));
        //Debug.Log(tests.Length, this);
    
        //IEnemyController test = GetComponent(typeof(IEnemyController)) as IEnemyController;
        //Debug.Log(test != null ? "Found ITest" : "ITest Not Found", this);
    }

    void Update()
    {
        i_time += Time.deltaTime;
        if (i_time > invincibilityTime && stunTimer == 0.0f)
        {
            this.canTakeDamage = true;
            this.colorpicker.material.color = Color.white;
        }

        if (stunTimer > 0.0f)
            stunTimer += Time.deltaTime;
        if (stunTimer > stunTime)
        {
            stunTimer = 0.0f;
            GetComponent<IEnemyController>().Stun(false); 
            this.colorpicker.material.color = Color.white;
            GetComponent<Animator>().enabled = true;
        }
    }

    public bool TakeDamage(float damage, EffectTypes projectileType)
    {
        if (this.canTakeDamage == false)
        {
            return false;
        }
        FindObjectOfType<SoundManager>().PlaySoundEffect("Laser");
        this.canTakeDamage = false;
        this.colorpicker.material.color = Color.red;
        this.i_time = 0.0f;
        this.health -= damage * TypeFactor(userType, projectileType);
        if (projectileType == EffectTypes.Electric)
        {
            stunTimer = 0.001f;
            GetComponent<IEnemyController>().Stun(true); 
            this.colorpicker.material.color = Color.grey;
            GetComponent<Animator>().enabled = false;
        }
        if (this.health <= 0.0f)
        {
            Destroy(gameObject);
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
