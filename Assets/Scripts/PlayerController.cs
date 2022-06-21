using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    Rigidbody2D rig;
    Animator anim;
    public GameObject shoot;

    

    private PlayerState playerState = PlayerState.grounded;
    public SideState sideState = SideState.right;
    public float speed, jumpForce, dashForce;
    public bool isDash = false;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Dash();
        Jumping();
        Shoot();
    }

    void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("shoot");
            if(sideState == SideState.right)
            {
                Instantiate(shoot, transform.position + Vector3.right, transform.rotation).GetComponent<Shoot>().isRight = true;
            } else
            {
                Instantiate(shoot, transform.position + Vector3.left, transform.rotation).GetComponent<Shoot>().isRight = false;
            }
        }
    }

    void Dash()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            isDash = true;
            if(sideState == SideState.right)
            {
                rig.AddForce(new Vector2(dashForce, 0), ForceMode2D.Impulse);
                anim.SetTrigger("Dash");
            }
            StartCoroutine("DashTimeOut");
        }
    }

    IEnumerator DashTimeOut()
    {
        yield return new WaitForSeconds(0.7f);
        isDash = false;
    }

    void Move()
    {
        if(!isDash)
        {
            float horizontal = Input.GetAxis("Horizontal");

            if (horizontal != 0)
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }

            if (horizontal < 0)
            {
                transform.eulerAngles = new Vector2(0, -180);
                sideState = SideState.left;
            }
            else if (horizontal > 0)
            {
                transform.eulerAngles = new Vector2(0, 0);
                sideState = SideState.right;
            }

            rig.velocity = new Vector2(horizontal * speed, rig.velocity.y);
        }
    }

    void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerState == PlayerState.grounded)
        {
            playerState = PlayerState.jumping;
            anim.SetBool("isJumping", true);
            rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        } else if (Input.GetKeyDown(KeyCode.Space) && playerState == PlayerState.jumping)
        {
            playerState = PlayerState.doubleJump;
            rig.AddForce(new Vector2(0, jumpForce/ 2), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerState = PlayerState.grounded;
            anim.SetBool("isJumping", false);
        }

        if(collision.gameObject.CompareTag("Enemy"))
        {
            damage(1);
        }
        
    }

    public enum PlayerState
    {
        jumping,
        doubleJump,
        grounded,
        running,
    }

    public enum SideState
    {
        right,
        left,
    }

}
