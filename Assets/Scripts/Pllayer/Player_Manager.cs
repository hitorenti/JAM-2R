using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    public int PlayerLife;
    private int _playerLife; // Buffer

    private Animator anim;
    private Player_Jump pj;
    public static int LLaves = 0;
    public HealthBar hb;
    public Transform RespawnPosition;
    public Player_Attack pa;

    private void Awake()
    {
        anim = this.gameObject.GetComponent<Animator>();
        pj = this.gameObject.GetComponent<Player_Jump>();
        _playerLife = PlayerLife;
    }

    public void EndDeathAnimation()
    {
        // offset
        Vector2 nwPos = RespawnPosition.position;
        nwPos.x += 5;
        this.transform.position = nwPos;

        anim.SetBool("death", false);

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("ENEMY"))
        {
            if (!pj.IsOverEnemy)
            {
                Damage(2,false,2);
            }

        }
    }

    public void EndDamageAnimation()
    {
        anim.SetBool("damage", false);
        hb.ChangePercentage((100 / PlayerLife) * _playerLife);


    }

    /// <summary>
    /// Activate damage anim and take life
    /// </summary>
    /// <param name="damage">To substract</param>
    /// <param name="UpDamage">Damage from Up?</param>
    public void Damage(int damage,bool UpDamage,float pushForce)
    {
        _playerLife -= damage;

        if(_playerLife > 0)
        {
            if (!UpDamage)
            {
                if (this.GetComponent<SpriteRenderer>().flipX)
                {
                    this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x, this.transform.position.x + pushForce, 0.215f), this.transform.position.y);

                }
                else
                {
                    this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x, this.transform.position.x - pushForce, 0.215f), this.transform.position.y);

                }
            }
            anim.SetBool("damage", true);
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

    public void EndAttack()
    {
        pa.attack = false;

    }

}
