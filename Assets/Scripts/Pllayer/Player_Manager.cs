using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    public float PushForce;
    public int PlayerLife;
    private int _playerLife; // Buffer

    private Animator anim;
    private Player_Jump pj;
    public static int LLaves = 0;
    public HealthBar hb;

    private void Awake()
    {
        anim = this.gameObject.GetComponent<Animator>();
        pj = this.gameObject.GetComponent<Player_Jump>();
        _playerLife = PlayerLife;
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
                Damage(0,false);
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
    /// <param name="UpDamage">Damage from Up?</param>
    public void Damage(int damage,bool UpDamage)
    {
        _playerLife -= damage;

        if(_playerLife > 0)
        {
            if (!UpDamage)
            {
                if (this.GetComponent<SpriteRenderer>().flipX)
                {
                    this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x, this.transform.position.x + PushForce, 0.215f), this.transform.position.y);

                }
                else
                {
                    this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x, this.transform.position.x - PushForce, 0.215f), this.transform.position.y);

                }
            }
            anim.SetBool("damage", true);
            hb.ChangePercentage((100/PlayerLife)*_playerLife);
        }
        else
        {
            Death();
        }


    }

    /// <summary>
    /// Activate Death animation
    /// </summary>
    private void Death()
    {
        anim.SetBool("death", true);
    }
}
