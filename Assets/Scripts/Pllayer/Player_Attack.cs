using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    private Animator anim;
    private bool attack;
    public int DamageToEnemy;
    public int EnemyLayer;

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

        Vector3 origin = this.transform.position;
        origin.x -= 0.1f;

        if (this.GetComponent<SpriteRenderer>().flipX)
        {

            hit = Physics2D.Raycast(origin, Vector2.left, RayAttackDistance, EnemyLayer);

        }
        else
        {
            hit = Physics2D.Raycast(origin, Vector2.right, RayAttackDistance, EnemyLayer);
        }
        
        
        if (Input.GetKeyDown(AttackKey))
        {
            attack = true;
            Attack(hit.collider, DamageToEnemy);
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
                if(hit.name == "Empuje")
                {
                    hit.transform.parent.GetComponent<Enemy_Damage>().Damage(damage);

                }
                else
                {
                    if (hit.name.Equals("Bala"))
                    {
                        hit.GetComponent<Enemy_Damage>().Damage(damage);

                    }
                }
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

        Vector3 origin = this.transform.position;
        origin.x -= 0.1f;

        Gizmos.DrawRay(origin, direction);
    }
}
