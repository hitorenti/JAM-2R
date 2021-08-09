using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Jump : MonoBehaviour
{
    public float RayDistance;
    public float JumpForce;

    private Rigidbody2D rb2d;
    private Animator anim;
    [HideInInspector]
    public bool IsJump;
    [HideInInspector]
    public bool IsOverEnemy;

    private void Awake()
    {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        anim = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        //! Animation
        anim.SetBool("jump", IsJump);
    }

    private void FixedUpdate()
    {
        // Draw ray for ground detection
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, RayDistance);

        IsJump = true;

        //? Is hit colliding?
        if (hit.collider != null)
        {
            //? Is ground?
            if (hit.collider.tag.Equals("GROUND"))
            {
                IsJump = false;

            }
            if (hit.collider.tag.Equals("ENEMY"))
            {
                IsJump = false;
                IsOverEnemy = true;
            }
            else
            {
                IsOverEnemy = false;

            }
        }

        // For 2 different keys usage
        if (Input.GetAxis("Vertical")>0 && !IsJump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, JumpForce);
        }
    }
}
