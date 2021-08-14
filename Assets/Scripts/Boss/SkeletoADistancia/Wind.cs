using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public Animator Animator;
    public SpriteRenderer Sprite;
    public Rigidbody2D Rb2D;
    public static float Speed;
    public int WindDamage;
    bool Activa = false;
    float Velocidad;
    float SpriteDireccion;
    public static int Direccion;
    private void OnEnable()
    {
        Velocidad = Speed;
        SpriteDireccion = Direccion;
        Destroy(gameObject, 10f);
        Physics2D.IgnoreLayerCollision(10, 6);
    }
    private void Update()
    {
        Rb2D.velocity = new Vector2(Velocidad, Rb2D.velocity.y);
        if (SpriteDireccion == 0) { Sprite.flipX = false; } else { Sprite.flipX = true; }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("Colision");
            collision.gameObject.GetComponent<Player_Manager>().Damage(WindDamage, false, 0);
        }
        StartCoroutine("Colision");
    }
    private void OnCollisionExit2D(Collision2D collision)
    {

    }
    IEnumerator Colision()
    {
        Animator.Play("BulletCollision");
        yield return new WaitForSeconds(0.25f);
        GameObject.Destroy(gameObject);
    }
}
