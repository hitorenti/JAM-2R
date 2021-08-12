using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fauna : MonoBehaviour
{
    public Rigidbody2D Rb2D;
    public Animator Animator;
    public SpriteRenderer Sprite;
    public GameObject[] Posiciones;
    int i = 0;
    public float TiempoDeEspera;
    float Tiempo;
    public float Speed;
    public float RangoDevision;
    public GameObject Player;
    public bool VerAlPlayer = false;
    public enum Animales
    {
        Conejo,Ave
    }
    public Animales Cambio;
    private void Start()
    {
        Tiempo = TiempoDeEspera;
        Physics2D.IgnoreLayerCollision(0, 7);
    }
    private void Update()
    {
        switch (Cambio)
        {
            case Animales.Conejo:
                if (VerAlPlayer)
                {
                    Animator.SetBool("ConejoM", false);
                    if (Posiciones[i].transform.position.x < transform.position.x) { Sprite.flipX = true; } else { Sprite.flipX = false; }
                    transform.position = Vector2.MoveTowards(transform.position, Posiciones[i].transform.position, Speed*Time.deltaTime);
                    if (Vector2.Distance(Player.transform.position, transform.position) < RangoDevision)
                    {
                        if (Vector2.Distance(transform.position, Posiciones[i].transform.position) < 0.1f)
                        {
                            if (Tiempo < 0)
                            {
                                Animator.SetBool("ConejoM", true);
                                i++;
                                Tiempo = TiempoDeEspera;
                                if (i > 1) { i = 0; }
                            }
                            else { Tiempo -= Time.deltaTime; Animator.SetBool("ConejoM", false); }
                        }
                    }
                }
                else
                {
                    Animator.SetBool("ConejoM", true);
                    if (Posiciones[i].transform.position.x < transform.position.x) { Sprite.flipX = true; } else { Sprite.flipX = false; }
                    transform.position = Vector2.MoveTowards(transform.position, Posiciones[i].transform.position, Speed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, Posiciones[i].transform.position) < 0.1f)
                    {
                        if (Tiempo < 0)
                        {
                            Animator.SetBool("ConejoM", true);
                            i++;
                            Tiempo = TiempoDeEspera;
                            if (i > 1) { i = 0; }
                        }
                        else { Tiempo -= Time.deltaTime; Animator.SetBool("ConejoM",false); }
                    }
                }
                break;
            case Animales.Ave:
                if (VerAlPlayer)
                {
                    Animator.SetBool("Ave", true);
                    if (Posiciones[i].transform.position.x < transform.position.x) { Sprite.flipX = true; } else { Sprite.flipX = false; }
                    transform.position = Vector2.MoveTowards(transform.position, Posiciones[i].transform.position, Speed * Time.deltaTime);
                    if (Vector2.Distance(Player.transform.position, transform.position) < RangoDevision)
                    {
                        if (Vector2.Distance(transform.position, Posiciones[i].transform.position) < 0.1f)
                        {
                            if (Tiempo < 0)
                            {
                                Animator.SetBool("AveM", true);
                                i++;
                                Tiempo = TiempoDeEspera;
                                if (i > 1) { i = 0; }
                            }
                            else { Tiempo -= Time.deltaTime; Animator.SetBool("AveM", false); }
                        }
                    }
                    
                }
                else
                {
                    Animator.SetBool("ConejoM", true);
                    if (Posiciones[i].transform.position.x < transform.position.x) { Sprite.flipX = true; } else { Sprite.flipX = false; }
                    transform.position = Vector2.MoveTowards(transform.position, Posiciones[i].transform.position, Speed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, Posiciones[i].transform.position) < 0.1f)
                    {
                        if (Tiempo < 0)
                        {
                            Animator.SetBool("ConejoM", true);
                            i++;
                            Tiempo = TiempoDeEspera;
                            if (i > 1) { i = 0; }
                        }
                        else { Tiempo -= Time.deltaTime; Animator.SetBool("ConejoM", false); }
                    }
                }
                break;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, RangoDevision);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Posiciones[0].transform.position, Posiciones[1].transform.position);
    }
}
