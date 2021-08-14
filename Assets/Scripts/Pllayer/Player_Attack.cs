using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public Animator anim;
    [HideInInspector]
    public bool attack;
    public int DamageToEnemy;

    public KeyCode AttackKey;
    private Collider2D enemyCollider;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("ENEMY"))
        {
            enemyCollider = collision;
        }
    }

    private void Update()
    {

        if (Input.GetKeyDown(AttackKey))
        {
            attack = true;
            Attack(enemyCollider, DamageToEnemy);
        }
        else
        {
            attack = false;

        }

        anim.SetBool("attack", attack);
    }


    /// <summary>
    /// hurts the enemy
    /// </summary>
    /// <param name="damage">life to take</param>
    private void Attack(Collider2D hit, int damage)
    {
        if (hit != null)
        {
            if (hit.tag.Equals("ENEMY"))
            {
                if (hit.name == "Empuje")
                {
                    hit.transform.parent.GetComponent<Enemy_Damage>().Damage(damage);

                }
                else
                {
                    if (!hit.name.Equals("Bala") && !hit.name.Contains("bomb"))
                    {
                        hit.GetComponent<Enemy_Damage>().Damage(damage);
                    }
                }
            }
        }
    }

}
