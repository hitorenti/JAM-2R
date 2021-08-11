using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_WallJump : MonoBehaviour
{
    public Vector2 RIGHTOFFSET;
    public Vector2 RIGHTSIZE;

    private void FixedUpdate()
    {
        
        Collider2D bxRight = Physics2D.OverlapBox(new Vector2(this.transform.position.x+ RIGHTOFFSET.x, this.transform.position.y+ RIGHTOFFSET.y),
                                                    new Vector2(RIGHTSIZE.x, RIGHTSIZE.y),0);

        if(bxRight != null)
        {
            Debug.Log(bxRight.transform.tag);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector2(this.transform.position.x+ RIGHTOFFSET.x, this.transform.position.y + RIGHTOFFSET.y), new Vector2(RIGHTSIZE.x, RIGHTSIZE.y));
    }
}
