using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotauro : MonoBehaviour
{
    public Animator Animator;
    public Rigidbody2D Rb2D;
    public SpriteRenderer Sprite;
    public GameObject Player;
    public float Speed;
    // ataque
    public float RangoDeVision;
    public float RangoDeTaqueCercano;
    public float Tiempo;
    float PosX;
    float PosY;
    bool verificacion = true;
    bool Ataque= true;
    bool AtaqueEnProceso = false;
    float Recuperacion;
    // ataqueGiro
    bool AtaqueGiro = true;
    public float TiempoAntesDeGirar;
    float TiempoDeGiro;
    float RecuperacionDelGiro;
    // elegir ataque
    int EligiendoAtaque;
    private void Start()
    {
        Tiempo = 3f;
        TiempoAntesDeGirar = 3f;
        TiempoDeGiro = 4;
        Physics2D.IgnoreLayerCollision(10, 3);
        Recuperacion = 3;
        RecuperacionDelGiro = 3f;
        EligiendoAtaque = Random.Range(0, 2);
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) < RangoDeVision)
        {
            RangoDeVision = 40;

            if (Ataque &&  EligiendoAtaque==0)
            {
                if (Tiempo < 0)
                {
                    Animator.SetBool("Carga", false);
                    Animator.SetBool("Movimiento", true);
                    // indentificando la posicion mas cercana del player a la pared
                    if (verificacion)
                    {
                        PosX = Player.transform.position.x;
                        PosY = transform.position.y;
                        if (transform.position.x < Player.transform.position.x) { Sprite.flipX = false; } else { Sprite.flipX = true; }
                        verificacion = false;
                    }
                    if (!AtaqueEnProceso) { transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, Speed * Time.deltaTime); }
                    if (Vector2.Distance(transform.position, Player.transform.position) < RangoDeTaqueCercano) // jugador cerca
                    {
                        AtaqueEnProceso = true;
                        Rb2D.velocity = Vector2.zero;
                        Animator.Play("Ataque");
                        StartCoroutine("NegandoAtaque");
                    }
                }
                else { Tiempo -= Time.deltaTime; Animator.SetBool("Carga", true); if (transform.position.x < Player.transform.position.x) { Sprite.flipX = false; } else { Sprite.flipX = true; } }
            }
            if(AtaqueGiro && EligiendoAtaque == 1)
            {
                if(TiempoAntesDeGirar < 0)
                {
                    Animator.SetBool("AntesDeGirar", false);
                    Animator.SetBool("Girar", true);
                    if (verificacion)
                    {
                        if (transform.position.x < Player.transform.position.x) { Rb2D.velocity = new Vector2(Speed, Rb2D.velocity.y); } else { Rb2D.velocity = new Vector2(-Speed, Rb2D.velocity.y); }
                        verificacion = false;
                    }
                    if(TiempoDeGiro < 0)
                    {
                        Rb2D.velocity = Vector2.zero;
                        Animator.SetBool("Girar", false) ;
                        AtaqueGiro = false;

                    }
                    else { TiempoDeGiro -= Time.deltaTime; }
                }
                else { TiempoAntesDeGirar -= Time.deltaTime;Animator.SetBool("AntesDeGirar", true); if (transform.position.x < Player.transform.position.x) { Sprite.flipX = false; } else { Sprite.flipX = true; } }
            }

        }
        else { }
        if (!Ataque)
        {
            if(Recuperacion < 0)
            {
                Tiempo = 3;
                Animator.SetBool("Carga", false);
                Animator.SetBool("Movimiento", false);
                Ataque = true;
                verificacion = true;
                AtaqueEnProceso = false;
                EligiendoAtaque = Random.Range(0, 2);
                Recuperacion = 3;
                
            }
            else { Recuperacion -= Time.deltaTime;  }
        }
        if (!AtaqueGiro)
        {
            if (RecuperacionDelGiro < 0)
            {
                TiempoDeGiro = 3;
                TiempoAntesDeGirar = 3f;
                Animator.SetBool("AntesDeGirar", false);
                Animator.SetBool("Girar", false);
                verificacion = true;
                AtaqueGiro = true;
                EligiendoAtaque = Random.Range(0, 2);
                RecuperacionDelGiro = 5f;
            }
            else { RecuperacionDelGiro -= Time.deltaTime; }
        }
    }
    IEnumerator NegandoAtaque()
    {
        yield return new WaitForSeconds(1.15f);
        Animator.SetBool("Movimiento", false);
        Ataque = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, RangoDeVision);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RangoDeTaqueCercano);
    }
}
