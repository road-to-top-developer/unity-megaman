using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public Animator anim2;

    int health;

    private void Start()
    {
        anim2 = GetComponent<Animator>();
    }

    public void damage(int dam)
    {
        health -= dam;

        Debug.Log("acertou");

        if(health <= 0)
        {
            anim2.SetTrigger("die");
        }
    }
}
