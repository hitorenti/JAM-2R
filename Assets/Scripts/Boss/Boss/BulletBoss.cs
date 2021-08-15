using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoss : MonoBehaviour
{
    public int DamageToPlayer;
    public Rigidbody2D Rb2D;
    public static float Speed;
    bool Activa = false;
    float Velocidad;
    public static int Direccion;
    private void OnEnable()
    {
        Velocidad = Speed;
        Destroy(gameObject, 3f);
        Physics2D.IgnoreLayerCollision(10, 6);
    }
    private void Update()
    {
        Rb2D.velocity = new Vector2(Velocidad, Rb2D.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player_Manager>().Damage(DamageToPlayer, false, 2);
            GameObject.Destroy(gameObject);
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Muro"))
        {
            GameObject.Destroy(gameObject);
        }
    }
}
