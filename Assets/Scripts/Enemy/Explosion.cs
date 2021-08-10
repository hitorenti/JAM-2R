using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Animator Animator;
    public SpriteRenderer Sprite;
    public Rigidbody2D Rb2D;
    public static float Speed;
    bool Activa=false;
    float Velocidad;
    Vector3 Rotacion=new Vector3(0,0,0);
    bool Suelo = false;
    private void OnEnable()
    {
        Velocidad = Speed;
        Animator.SetBool("Explosion", false);
        Animator.SetBool("Explosion_Aire", false);
        Activa = false;
        Rb2D.AddForce(new Vector2(200, 200));
    }
    private void Update()
    {
        Rb2D.velocity = new Vector2(Velocidad, Rb2D.velocity.y);
        if (Activa) { if (Suelo) { Animator.SetBool("Explosion", true); Rb2D.velocity = new Vector2(0, 0); }
                      else { Animator.SetBool("Explosion_Aire",true); Rb2D.velocity = new Vector2(0, 0); } }
    }
    public void Eliminar()
    {
        GameObject.Destroy(gameObject);
    }
    public void BombaActiva()
    {
        Rb2D.freezeRotation = true;
        gameObject.transform.localEulerAngles = Rotacion;
        Activa = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rb2D.freezeRotation = true;
            gameObject.transform.localEulerAngles = Rotacion;
            Activa = true;
        }
        if (collision.gameObject.CompareTag("GROUND"))
        {
            Suelo = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GROUND"))
        {
            Suelo = false ;
        }
    }
}
