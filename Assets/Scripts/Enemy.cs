using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Shoot"))
        {
            Destroy(collision.gameObject);
            damage(20);
        }
    }
}
