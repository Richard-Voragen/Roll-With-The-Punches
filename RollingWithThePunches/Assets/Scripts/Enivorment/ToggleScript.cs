using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleScript : MonoBehaviour
{
    
    [SerializeField] private GameObject player; 
    [SerializeField] private UnityEvent callWhenOn, callWhenOff;


    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if (player.transform.position.y < -20f) {
            callWhenOff.Invoke();   
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Lightning(Clone)")
        {
            callWhenOn.Invoke();
        }
        if (collision.gameObject.name == "Waterball(Clone)")
        {
            callWhenOff.Invoke();
        }
    }

    
}
