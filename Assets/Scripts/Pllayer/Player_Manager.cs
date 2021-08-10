using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    public float PushForce;

    private Animator anim;
    private Player_Jump pj;

    private void Awake()
    {
        anim = this.gameObject.GetComponent<Animator>();
        pj = this.gameObject.GetComponent<Player_Jump>();
    }

    public void EndDeathAnimation()
    {
        Vector2 nwPos = this.transform.position;

        if (this.GetComponent<SpriteRenderer>().flipX)
        {
            nwPos.x += 10; //! Spawn in -10 of current position
            this.transform.position = nwPos;
        }
        else
        {
            nwPos.x -= 10; //! Spawn in -10 of current position
            this.transform.position = nwPos;
        }

        anim.SetBool("death", false);

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("ENEMY"))
        {
            if (!pj.IsOverEnemy)
            {
                Damage(0);
                //Death();
            }

        }
    }

    public void EndDamageAnimation()
    {
        anim.SetBool("damage", false);

    }

    /// <summary>
    /// Activate damage anim and take life
    /// </summary>
    /// <param name="damage">To substract</param>
    private void Damage(int damage)
    {
        if (this.GetComponent<SpriteRenderer>().flipX)
        {
            this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x, this.transform.position.x + PushForce, 0.215f), this.transform.position.y);

        }
        else
        {
            this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x, this.transform.position.x - PushForce, 0.215f), this.transform.position.y);

        }
        anim.SetBool("damage", true);

    }

    /// <summary>
    /// Activate Death animation
    /// </summary>
    private void Death()
    {
        anim.SetBool("death", true);
    }
}
