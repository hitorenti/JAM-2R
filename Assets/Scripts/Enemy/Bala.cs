using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public Animator Animator;
    public int DamageToPlayer;
    public SpriteRenderer Sprite;
    public Rigidbody2D Rb2D;
    public static float Speed;
    bool Activa = false;
    float Velocidad;
    public static int Direccion;
    private void OnEnable()
    {
        Velocidad = Speed;
        Destroy(gameObject, 10f);
        Physics2D.IgnoreLayerCollision(10,6);
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
}
