using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upAndDown : MonoBehaviour
{

    [SerializeField] private float speed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("hjer");
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Bumper")
        {
            speed = -speed;
        }
    }
}
