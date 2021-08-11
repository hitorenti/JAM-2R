using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    public Rigidbody2D Rb2D;
    public Animator Animator;
    public BoxCollider2D Collider;
    public float TiempoDeEspera;
    float Tiempo;
    Vector2 PuntoDeInicio;
    bool ContactoPlayer=false;
    bool Habilitar = false;
    float TimeActivar;
    public bool Activar;
    public enum Cambio
    {
        Superior,Inferior
    }
    public Cambio Plataformas;
    private void Start()
    {
        Tiempo = TiempoDeEspera;
        TimeActivar = 1f;
        PuntoDeInicio = transform.position;
    }
    private void Update()
    {
        switch (Plataformas)
        {
            case Cambio.Superior:
                Animator.SetBool("Inferior", false);
                if (Activar) 
                {
                    if (TimeActivar < 0)
                    {
                        ContactoPlayer = true;
                    }
                    else { TimeActivar -=Time.deltaTime; }
                }
                else { TimeActivar = 1f; }
                if (ContactoPlayer)
                {
                    Animator.SetBool("DesapareceS", true);
                }
                if (Habilitar)
                {
                    if (Tiempo < 0)
                    {
                        Rb2D.velocity = Vector2.zero;
                        Rb2D.bodyType = RigidbodyType2D.Kinematic;
                        transform.position = PuntoDeInicio;
                        Rb2D.gravityScale = 0f;
                        Collider.isTrigger = false;
                        Animator.SetBool("DesapareceS", false);
                        Tiempo = TiempoDeEspera;
                        TimeActivar = 1f;
                        ContactoPlayer = false;
                        Activar = false;
                        Habilitar = false;
                    }
                    else { Tiempo -= Time.deltaTime; }
                }
                break;
            case Cambio.Inferior:
                Animator.SetBool("Inferior", true);
                if (Activar)
                {
                    if (TimeActivar < 0)
                    {
                        ContactoPlayer = true;
                    }
                    else { TimeActivar -= Time.deltaTime; }
                }
                else { TimeActivar = 1f; }
                if (ContactoPlayer)
                {
                    Animator.SetBool("DesapareceI", true);
                }
                if (Habilitar)
                {
                    if (Tiempo < 0)
                    {
                        Rb2D.velocity = Vector2.zero;
                        Rb2D.bodyType = RigidbodyType2D.Kinematic;
                        transform.position = PuntoDeInicio;
                        Rb2D.gravityScale = 0f;
                        Collider.isTrigger = false;
                        Animator.SetBool("DesapareceI", false);
                        Tiempo = TiempoDeEspera;
                        TimeActivar = 1f;
                        ContactoPlayer = false;
                        Activar = false;
                        Habilitar = false;
                    }
                    else { Tiempo -= Time.deltaTime; }
                }
                break;
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Activar = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Activar = false;
        }
    }
    public void DesavilitarCollider()
    {
        Collider.isTrigger = true;
        Habilitar = true;
        Rb2D.bodyType=0;
        Rb2D.gravityScale = 0.5f;
    }

}
