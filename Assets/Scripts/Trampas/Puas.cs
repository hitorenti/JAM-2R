using UnityEngine;

public class Puas : MonoBehaviour
{
    public Player_Manager DamageAlPlayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DamageAlPlayer.Damage(PuasControl.DamageAlPlayer, true); // colocar Dalo al player, indicando que el dalo viene de arriba
        }
    }
}