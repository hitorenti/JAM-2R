using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public Rigidbody2D Rb2D;
    public Animator Animator;
    public SpriteRenderer Sprite;
    public GameObject Player;
    public float Speed;
    public float DistanciaDeVision;
    public float DistanciaDeAtaque;
    public float TiempoDeAtaque; // Tiempo para realizar un ataque
    float Tiempo;
    float TiempoDeRetroceso;// Tiempo para retroceder despus de un ataque
    float PosInicial;
    Vector3 PosIInicial;
    bool Reposo = false;
    float PosX;
    float PosY;
    bool Ataque = true;
    bool NegandoSpeed=false;
    bool VerAlPlayer;

    public void Start()
    {
        TiempoDeRetroceso = 2f;
        PosInicial = transform.position.x;
        PosIInicial = transform.position;
        Physics2D.IgnoreLayerCollision(10, 3);
    }
    public void Update()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) < DistanciaDeVision)// vision del Player
        {
            Reposo = true;
            if(Player.transform.position.x > transform.position.x) { Sprite.flipX = false; } else { Sprite.flipX = true; }
            Animator.SetBool("Movimiento", true);
            PosX = Player.transform.position.x;
            PosY = transform.position.y;
            if (Ataque) { if (!NegandoSpeed) { transform.position = Vector2.MoveTowards(transform.position, new Vector2(PosX, PosY), Speed * Time.deltaTime); Animator.SetBool("MovimientoAtras", false); } }
            else
            {
                Animator.SetBool("MovimientoAtras", true);
                if (Player.transform.position.x > transform.position.x) { Rb2D.velocity = new Vector2(-Speed / 3, Rb2D.velocity.y); }
                else { Rb2D.velocity = new Vector2(Speed / 3, Rb2D.velocity.y); }
            }
            if(Vector2.Distance(transform.position, Player.transform.position) < DistanciaDeAtaque && Ataque)// vision de ataque
            {
                if (Tiempo < 0)
                {
                    NegandoSpeed = true;
                    Rb2D.velocity = new Vector2(0, 0);
                    Animator.Play("Ataque");
                    Tiempo = TiempoDeAtaque;  
                }
                else { Tiempo -= Time.deltaTime; }

            }
            else { Tiempo -= Time.deltaTime; }

        }
        else
        {
            NegandoSpeed = false;
            Ataque = true;
            TiempoDeRetroceso = 2f;
            if (Reposo)
            {
                if (PosInicial > transform.position.x) { Sprite.flipX = false; } else { Sprite.flipX = true; }
                PosY = transform.position.y;
                Tiempo -= Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(PosInicial, PosY), (Speed / 3) * Time.deltaTime);
                if (Vector2.Distance(transform.position, PosIInicial) < 1) { Animator.SetBool("Movimiento", false); Reposo = false; }
            }
            else
            {
                Rb2D.velocity =Vector2.zero;
                Animator.SetBool("Movimiento", false);
                Animator.SetBool("MovimientoAtras", false);
                Sprite.flipX = false;
            }
        }
        if (!Ataque)
        {
            if(TiempoDeRetroceso < 0)
            {
                NegandoSpeed = false;
                Ataque = true;
                TiempoDeRetroceso = 2f;
            }
            else { TiempoDeRetroceso -= Time.deltaTime; }
            Animator.SetBool("MovimientoAtras", true);
            if (Player.transform.position.x > transform.position.x) { Rb2D.velocity = new Vector2(-Speed / 3, Rb2D.velocity.y); }
            else { Rb2D.velocity = new Vector2(Speed / 3, Rb2D.velocity.y); }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, DistanciaDeVision);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DistanciaDeAtaque);
    }
    public void NegandoAtaque()
    {
        Ataque = false;
    }
}
