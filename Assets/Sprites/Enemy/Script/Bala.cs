using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public Animator Animator;
    public SpriteRenderer Sprite;
    public Rigidbody2D Rb2D;
    public static float Speed;
    bool Activa = false;
    float Velocidad;

    private void OnEnable()
    {
        Velocidad = Speed;
    }
    private void Update()
    {
        Rb2D.velocity = new Vector2(Velocidad, Rb2D.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.Destroy(gameObject);
        }
        GameObject.Destroy(gameObject);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
}
