using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puas : MonoBehaviour
{
    public Player_Manager Da�oAlPlayer;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Da�oAlPlayer.Damage(PuasControl.Da�oAlPersonaje) ; // colocar Dalo al player
        }
    }
}
