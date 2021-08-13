using UnityEngine;

public class Enemy_Damage : MonoBehaviour
{
    public int Life;
    public HealthBar hb;

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
        if (Life == 0)
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
        Debug.Log("C muere");
    }
}