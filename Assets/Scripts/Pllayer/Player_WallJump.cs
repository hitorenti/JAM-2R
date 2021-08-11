using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_WallJump : MonoBehaviour
{
    public Vector2 RIGHTOFFSET;
    public Vector2 RIGHTSIZE;

    public Vector2 LEFTOFFSET;
    public Vector2 LEFTSIZE;

    public PLAYERGROUND pg;
    public float WallJumpForce;

    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        
        Collider2D bxRight = Physics2D.OverlapBox(new Vector2(this.transform.position.x+ RIGHTOFFSET.x, this.transform.position.y+ RIGHTOFFSET.y),
                                                    new Vector2(RIGHTSIZE.x, RIGHTSIZE.y),0);
        Collider2D bxLeft = Physics2D.OverlapBox(new Vector2(this.transform.position.x + LEFTOFFSET.x, this.transform.position.y + LEFTOFFSET.y),
                                            new Vector2(LEFTSIZE.x, LEFTSIZE.y), 0);

        if (bxRight != null)
        {
            if (pg.IsJump && Input.GetKey(KeyCode.A))
            {
                // JUMP TO LEFT
                rb2d.AddForce(Vector2.right * WallJumpForce);
            }
        }

        if (bxLeft != null)
        {
            if (pg.IsJump && Input.GetKey(KeyCode.D))
            {
                // JUMP TO RIGHT
                rb2d.AddForce(Vector2.right * WallJumpForce);

            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector2(this.transform.position.x+ RIGHTOFFSET.x, this.transform.position.y + RIGHTOFFSET.y), new Vector2(RIGHTSIZE.x, RIGHTSIZE.y));
        Gizmos.DrawCube(new Vector2(this.transform.position.x+ LEFTOFFSET.x, this.transform.position.y + LEFTOFFSET.y), new Vector2(LEFTSIZE.x, LEFTSIZE.y));
    }
}
