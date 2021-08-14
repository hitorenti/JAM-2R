using UnityEngine;

public class Enemy_Damage : MonoBehaviour
{
    public int Life;
    public HealthBar hb;
    public Animator anim;

    private int BufferLife; // Total life stored

    private void Start()
    {
        BufferLife = Life;
    }

    /// <summary>
    /// Substract life to this enemy
    /// </summary>
    /// <param name="d">To substract</param>
    public void Damage(int d)
    {
        // Display life
        hb.gameObject.SetActive(true);

        // substract specified value
        Life -= d;

        //? has life?
        if (Life <= 0)
        {
            hb.ChangePercentage(0);
            Death();
        }
        else
        {
            //! Substract life
            float perc = (100 / BufferLife) * Life;
            hb.ChangePercentage(perc);
        }
    }

    private void Death()
    {
        if(this.GetComponent<Skeleton>() != null)
        {
            anim.SetBool("MuertePermanente",true);
        }
        else if(this.GetComponent<SkeletoAdistancia>() != null)
        {
            anim.SetBool("MuertePermanente", true);

        }
        else
        {
            switch (this.GetComponent<Enemy>().Cambio)
            {
                case Enemy.Enemigos.bomberGoblin:
                    anim.SetBool("DEATH_BGOBLIN", true);
                    break;
                case Enemy.Enemigos.Fly:
                    anim.SetBool("DEATH_FLY", true);
                    break;
                case Enemy.Enemigos.Globin:
                    anim.SetBool("DEATH_GOBLIN", true);
                    break;
                case Enemy.Enemigos.Mushroon:
                    anim.SetBool("DEATH_MUSH", true);
                    break;
                case Enemy.Enemigos.Slime:
                    anim.SetBool("DEATH_SLIME", true);
                    break;
                case Enemy.Enemigos.Worm:
                    anim.SetBool("DEATH_WORM", true);
                    break;
            }

        }

    }

    public void EndDeath(int IsSkeleton)
    {
        //? 1 = true, 0= false
        if (IsSkeleton == 0)
        {
            Destroy(this.transform.parent.gameObject);

        }
        else
        {
            anim.StopPlayback();
        }
    }
}
