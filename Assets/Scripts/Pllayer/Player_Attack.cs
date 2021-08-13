using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    private Animator anim;
    private bool attack;

    public float RayAttackDistance;
    public KeyCode AttackKey;

    private void Awake()
    {
        anim = this.GetComponent<Animator>();
        // For ray attack
        Physics2D.queriesStartInColliders = false;
    }

    private void Update()
    {
        anim.SetBool("attack", attack);
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit;

        if (this.GetComponent<SpriteRenderer>().flipX)
        {
            hit = Physics2D.Raycast(this.transform.position, Vector2.left, RayAttackDistance);
        }
        else
        {
            hit = Physics2D.Raycast(this.transform.position, Vector2.right, RayAttackDistance);
        }
        
        
        if (Input.GetKeyDown(AttackKey))
        {
            attack = true;
            Attack(hit.collider,1);
        }
        else
        {
            attack = false;

        }
    }

    public void EndAttack()
    {
        attack = false;

    }

    /// <summary>
    /// hurts the enemy
    /// </summary>
    /// <param name="damage">life to take</param>
    private void Attack(Collider2D hit, int damage)
    {
        if(hit != null)
        {
            if (hit.tag.Equals("ENEMY"))
            {
                hit.GetComponent<Enemy_Damage>().Damage(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 direction;
        Gizmos.color = Color.blue;
        if (this.GetComponent<SpriteRenderer>().flipX)
        {
             direction = this.transform.TransformDirection(Vector3.left) * RayAttackDistance;

        }
        else
        {
             direction = this.transform.TransformDirection(Vector3.right) * RayAttackDistance;

        }

        Gizmos.DrawRay(this.transform.position, direction);
    }
}
