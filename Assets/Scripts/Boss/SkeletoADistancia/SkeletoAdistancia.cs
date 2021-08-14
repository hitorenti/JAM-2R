using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletoAdistancia : MonoBehaviour
{
    public Rigidbody2D Rb2D;
    public Animator Animator;
    public SpriteRenderer Sprite;
    public GameObject Player;
    public GameObject Bullet;
    public GameObject[] Pos;
    public float Speed;
    public float RangoDeVision;
    public float RangoDeAtaqueCercano;
    float Tiempo;
    public float TiempoDeEspera;
    float TiempoDeAtaqueCercano;
    float TiempoDeAtaqueADistancia;
    float PosX;
    float PosY;
    bool Ataque=true;
    bool Ataque2 = true;
    public bool Perseguir = true;
    float PosicionDelEnemigo;
    // Direccion de creacion
    Vector3 PosicionDeCreacion;
    public bool AtaqueEnUNaDireccion=false;
    private void Start()
    {
        Tiempo = TiempoDeEspera;
        Physics2D.IgnoreLayerCollision(10, 3);
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) < RangoDeVision)// rango de vision
        {
            Animator.SetBool("Movimiento", true);
            if (!AtaqueEnUNaDireccion)// para lansar en una sola direccion sin perseguir al player
            {
                if (Player.transform.position.x > transform.position.x) { Sprite.flipX = false; PosicionDeCreacion = Pos[0].transform.position; } else { Sprite.flipX = true; PosicionDeCreacion = Pos[1].transform.position; }
            }
            else { Rb2D.velocity = Vector2.zero; Animator.SetBool("Movimiento", false); Animator.SetBool("MovimientoAtras", false); }
            if (!Ataque2 && !AtaqueEnUNaDireccion) // Ataque corto
            {
                if (Perseguir)
                {// Perseguir al player
                    Animator.SetBool("MovimientoAtras", false);
                    PosX = Player.transform.position.x;
                    PosY = transform.position.y;
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(PosX, PosY), (Speed/2) * Time.deltaTime);
                }
                else
                {// tretroceder de Player
                    Animator.SetBool("MovimientoAtras", true);
                    if (Player.transform.position.x > transform.position.x)
                    { Rb2D.velocity = new Vector2(-Speed / 4, Rb2D.velocity.y); }
                    else { Rb2D.velocity = new Vector2(Speed / 4, Rb2D.velocity.y); }
                }
            }
            if(Vector2.Distance(transform.position, Player.transform.position) < RangoDeAtaqueCercano)//Ataque cercano
            {
                Ataque = false;
                Ataque2 = true;
                Rb2D.velocity = Vector2.zero; Animator.SetBool("Movimiento", false); Animator.SetBool("MovimientoAtras", false);
                if (TiempoDeAtaqueCercano < 0)
                {
                    AtaqueEnUNaDireccion = true;
                    Animator.Play("Ataque2");
                    TiempoDeAtaqueCercano = 3;
                    PosicionDelEnemigo = Random.Range(0, 2);
                    Tiempo = TiempoDeEspera;
                }
                else { TiempoDeAtaqueCercano -= Time.deltaTime; }
            }
            else { Ataque = true;Ataque2 = false; }
            if (Ataque) // ataque a distancia
            {
                if (Tiempo < 0)
                {
                    Animator.Play("Ataque");
                    Tiempo = TiempoDeEspera;
                    PosicionDelEnemigo = Random.Range(0, 2);
                    if (!Sprite.flipX) { Wind.Speed = 4; Wind.Direccion = 0; } else { Wind.Speed = -4; Wind.Direccion = 1; }
                }
                else
                {
                    Tiempo -= Time.deltaTime;
                }
            }
            if(PosicionDelEnemigo == 0) { Perseguir = true; } else { Perseguir = false; }
            TiempoDeAtaqueCercano -= Time.deltaTime;// tiempo de ataque cercano si sale de la zona
            if (TiempoDeAtaqueCercano < 0) { AtaqueEnUNaDireccion = false; }
        }
        else { Rb2D.velocity = Vector2.zero; Animator.SetBool("Movimiento", false); Animator.SetBool("MovimientoAtras", false); }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, RangoDeVision);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RangoDeAtaqueCercano);
    }
    public void Creacion()
    {
        GameObject obj = Instantiate(Bullet) as GameObject;
        obj.transform.position = PosicionDeCreacion;
        obj.transform.rotation = transform.rotation;
    }
    public void AtaqueRealizado()
    {
        Creacion();
    }
}
