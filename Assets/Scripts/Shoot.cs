using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public float speed;
    public bool isRight;

    Rigidbody2D rig;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 4);
    }

    void Update()
    {
        if (isRight)
        {
            rig.velocity = new Vector2(1 * speed, 0);
        } else
        {
            rig.velocity = new Vector2(-1 * speed, 0);
        }
        
    }
}
