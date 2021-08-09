using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Movement : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb2d;
    private Animator anim;

    private void Awake()
    {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        anim = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        //! Animations
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
        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

        //! Raw for detect idle state
        if(Input.GetAxisRaw("Horizontal") == 0)
        {
            rb2d.velocity = Vector2.zero;
        }
        else if(dir.x > 0)
        {
            rb2d.velocity = new Vector2(dir.x * speed,rb2d.velocity.y);
        }
        else if(dir.x < 0)
        {
            rb2d.velocity = new Vector2((dir.x * speed), rb2d.velocity.y);
        }


    }
}
