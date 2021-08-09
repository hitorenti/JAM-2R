using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Movement : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb2d;
    private Animator anim; 
    private bool IsJump;
    public float RayDistance;
    public float JumpForce;


    private void Awake()
    {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        anim = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        //! Animations
        anim.SetBool("jump", IsJump);

        if (rb2d.velocity.x > 0)
        {
            anim.SetBool("run", true);
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;

        }
        else if(rb2d.velocity.x < 0)
        {
            anim.SetBool("run", true);
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;

        }
        else
        {
            anim.SetBool("run", false);

        }
    }

    private void FixedUpdate()
    {
        // Draw ray for ground detection
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, RayDistance);
        // debug ray
        Debug.DrawRay(this.transform.position, Vector3.down * RayDistance, Color.red);

        float XDir = Input.GetAxis("Horizontal");

        //! Raw for detect idle state
        if(Input.GetAxisRaw("Horizontal") == 0)
        {
            rb2d.velocity = Vector2.zero;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            rb2d.velocity = new Vector2(XDir * speed,rb2d.velocity.y);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            rb2d.velocity = new Vector2((XDir * speed), rb2d.velocity.y);
        }

        //? Is hit colliding?
        if (hit.collider != null)
        {

            //? Is ground?
            if (hit.collider.tag.Equals("GROUND"))
            {
                IsJump = false;
            }
        }


        if (Input.GetKey(KeyCode.W) && !IsJump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, JumpForce);
            IsJump = true;
            rb2d.gravityScale = 0;

        }

        if (IsJump)
        {
            rb2d.gravityScale = 1;
        }


    }
}
