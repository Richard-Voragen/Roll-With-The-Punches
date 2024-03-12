using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sideToSide : MonoBehaviour
{

    [SerializeField] private float speed = 3.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(Vector2.right.x, Vector2.right.y, 0) * speed * Time.deltaTime;
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
