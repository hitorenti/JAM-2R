using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puas : MonoBehaviour
{
    public Player_Manager DañoAlPlayer;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DañoAlPlayer.Damage(PuasControl.DañoAlPersonaje) ; // colocar Dalo al player
        }
    }
}
