using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Animator Animator;
    public Rigidbody2D Rb2D;
    public SpriteRenderer Sprite;
    public GameObject Player;
    public GameObject Bullet;
    // Ataque
    public float Speed;
    public float RangoDeVision;
    public float RangoDeAtaqueCercano;
    public float Tiempo;
    float TiempoDeAtaque;
    float Recuperacion;
    float PosX;
    float PosY;
    bool Ataque = true;
    public Vector3 Punto;
    public bool Direccion = true;
    int AtaqueEntre1y2;
    // ataque con telepor
    float TiempoDeTelepor;
    // posicion de creacion de ataque
    public Vector3 PosicionDeBulletD;
    public Vector3 PosicionDeBulletI;
    // Ataque Randon
    int AtkAleatorio;
    // acercarce 
    bool Perseguir=true;
    int PerseguirRandon;
    float TiempoEnSeguir;
    private void Start()
    {
        Tiempo = 2;
        TiempoDeTelepor = 1.05f;
        TiempoDeAtaque = 3f;
        Recuperacion = 5;
        Physics2D.IgnoreLayerCollision(10, 3);
        AtkAleatorio = Random.Range(0,3);
        TiempoEnSeguir = 1f;

    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) < RangoDeVision)
        {
            RangoDeVision = 40;
            if (Ataque && AtkAleatorio == 0)
            {
                if (Tiempo < 0)
                {
                    Animator.SetBool("Movimiento", true);
                    if (Direccion)
                    {
                        if (transform.position.x < Player.transform.position.x) { Sprite.flipX = false; Rb2D.velocity = new Vector2(Speed, Rb2D.velocity.y); } else { Sprite.flipX = true; Rb2D.velocity = new Vector2(-Speed, Rb2D.velocity.y); }
                        Direccion = false;
                    }
                    else
                    {
                        if(TiempoDeAtaque<0)
                        {
                            Rb2D.velocity = Vector2.zero;
                            Animator.SetBool("Movimiento", false);
                            Ataque = false;
                        }
                        else { TiempoDeAtaque -= Time.deltaTime; }
                    }
                    if(Vector2.Distance(transform.position-Punto, Player.transform.position) < RangoDeAtaqueCercano)
                    {
                        Rb2D.velocity = new Vector2(0,0);
                        TiempoDeAtaque = 3f;
                        Animator.SetBool("Movimiento", false);
                        AtaqueEntre1y2 = Random.Range(0, 2);
                        if (AtaqueEntre1y2 == 0) { Animator.Play("Ataque"); } else { Animator.Play("AtaqueEnArea"); }
                        Perseguir = true;
                        PerseguirRandon= Random.Range(0, 2);
                        TiempoEnSeguir = 1.2f;
                        Ataque = false;
                    }
                }
                else { Tiempo -= Time.deltaTime; if (transform.position.x < Player.transform.position.x) { Sprite.flipX = false; } else { Sprite.flipX = true; } }
            }
            if (Ataque && AtkAleatorio == 1)
            {
                if (TiempoDeTelepor < 0)
                {
                    if (Direccion)
                    {
                        if (transform.position.x < Player.transform.position.x) { Sprite.flipX = false; Rb2D.velocity = new Vector2(Speed * 1.6f, Rb2D.velocity.y); } else { Sprite.flipX = true; Rb2D.velocity = new Vector2(-Speed * 1.5f, Rb2D.velocity.y); }
                        Direccion = false;
                    }
                    else
                    {
                        if (TiempoDeAtaque < 0)
                        {
                            Rb2D.velocity = Vector2.zero;
                            Animator.SetBool("PreparandoTelepor", false);
                            Ataque = false;
                        }
                        else { TiempoDeAtaque -= Time.deltaTime; }
                    }
                    if (Vector2.Distance(transform.position - Punto, Player.transform.position) < RangoDeAtaqueCercano)
                    {
                        Rb2D.velocity = new Vector2(0, 0);
                        TiempoDeAtaque = 3f;
                        Animator.SetBool("PreparandoTelepor", false);
                        Animator.Play("AtaqueDeTelepor");
                        Perseguir = true;
                        PerseguirRandon = Random.Range(0, 2);
                        TiempoEnSeguir = 1.2f;
                        Ataque = false;
                    }
                }
                else { TiempoDeTelepor -= Time.deltaTime; if (transform.position.x < Player.transform.position.x) { Sprite.flipX = false; } else { Sprite.flipX = true; } Animator.SetBool("PreparandoTelepor", true); }
            }
            if (Ataque && AtkAleatorio == 2)
            {
                if (TiempoDeTelepor < 0)
                {
                    if (Direccion)
                    {
                        if (transform.position.x < Player.transform.position.x) { Sprite.flipX = false; } else { Sprite.flipX = true;  }
                        Direccion = false;
                    }
                        Rb2D.velocity = new Vector2(0, 0);
                        Animator.Play("AtaqueADistancia");
                        Ataque = false;
                }
                else { TiempoDeTelepor -= Time.deltaTime; if (transform.position.x < Player.transform.position.x) { Sprite.flipX = false; } else { Sprite.flipX = true; } }
            }
        }
        if (!Ataque)
        {
            if (Recuperacion<0)
            {
                Animator.SetBool("Perseguir", false);
                Tiempo = 2;
                TiempoDeTelepor = 1.05f;
                TiempoDeAtaque = 3f;
                Direccion = true;
                Recuperacion = 5;
                AtkAleatorio = Random.Range(0, 3);
                Perseguir = false;
                Ataque = true;
            }
            else { Recuperacion -= Time.deltaTime;
                if (TiempoEnSeguir < 0)
                {
                    if (Perseguir && PerseguirRandon == 1)
                    {// Perseguir al player
                        Animator.SetBool("Perseguir", true);
                        PosX = Player.transform.position.x;
                        PosY = transform.position.y;
                        if (transform.position.x < Player.transform.position.x) { Sprite.flipX = false; } else { Sprite.flipX = true; }
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(PosX, PosY), (Speed / 5) * Time.deltaTime);
                    }
                }
                else { TiempoEnSeguir -= Time.deltaTime; }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, RangoDeVision);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position-Punto, RangoDeAtaqueCercano);
    }
    public void Creacion()
    {
        GameObject obj = Instantiate(Bullet) as GameObject;
        if (Sprite.flipX) { obj.transform.position = transform.position - PosicionDeBulletI; } else { obj.transform.position = transform.position - PosicionDeBulletD; }
        obj.transform.rotation = transform.rotation;
    }
    public void ataqueAdistancia()
    {
        if (Sprite.flipX) { BulletBoss.Speed = -6; } else { BulletBoss.Speed = 6; }
        Creacion();
        PerseguirRandon = Random.Range(0, 2);
        TiempoEnSeguir = 1.2f;
        Perseguir = true;
    }
}
