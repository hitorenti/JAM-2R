using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator Animator;
    public SpriteRenderer Sprite;
    public Rigidbody2D Rb2D;
    public GameObject Player;
    public BoxCollider2D Box;
    private Color ColorRango;
    // clases de enemigos
    public enum Enemigos
    {
        bomberGoblin,
        Fly,
        Globin,
        Mushroon,
        Slime,
        Worm
    }
    public Enemigos Cambio;
    // Variables
    public float RangoDeVision;
    float TiempoDeAtaque;
    public float TiempoDeEspera;
    public GameObject Bomba;
    // Fly
    public float SpeedFly;
    public float TiempoDeEsperaFly;
    float TiempoFly;
    public GameObject[] Punto;
    int i = 0;
    // GoblinA
    public GameObject Balas;
    // Mushroom
    public GameObject Empujes;
    public float FuerzaDeSalto;
    float TiempoDeEsperaMush;
    Vector2  PuntoDelPlayer;
    Vector2 PuntoDeInicio;
    float TiempoDeSaltoSlime;
    bool suelo;
    // mejorar posiciones
    float PosX;
    float PosY;
    float PosXPlayer;
    // sufrir Daño
    public static bool SufrirDaño = false;
    float TiempoDeDañoSufrido;
    private void Start()
    {
        ColorRango = Color.green;
        Physics2D.IgnoreLayerCollision(14,13);
        TiempoFly = TiempoDeEsperaFly;
        TiempoDeEsperaMush = 3f;
        PuntoDeInicio = transform.position;

        TiempoDeDañoSufrido = 0.3f;
    }
    private void Update()
    {
        if (SufrirDaño) { Debug.Log("Daño"); }
        if (SufrirDaño)
        {
            if(TiempoDeDañoSufrido < 0)
            {
                TiempoDeDañoSufrido = 0.3f;
                SufrirDaño = false;
            }
            else { TiempoDeDañoSufrido -= Time.deltaTime; }
            if(TiempoDeDañoSufrido < 0.1f) { Physics2D.IgnoreLayerCollision(7, 6,true); }
        }
        else { Physics2D.IgnoreLayerCollision(7, 6, false); }
        switch (Cambio)
        {
            case Enemigos.bomberGoblin:
                Empujes.SetActive(false);
                Rb2D.gravityScale = 1;
                Box.offset = new Vector2(0.07384136f, -0.07636565f);
                Box.size= new Vector2(0.6687278f, 0.9311044f);
                if (!SufrirDaño)
                {
                    if (Vector2.Distance(transform.position, Player.transform.position) < RangoDeVision)
                    {
                        if (Player.transform.position.x < transform.position.x) { Sprite.flipX = true; } else { Sprite.flipX = false; }
                        if (TiempoDeAtaque < 0)
                        {
                            Animator.Play("Goblin_Ataque");
                            if (Sprite.flipX) { Explosion.Speed = -7; } else { Explosion.Speed = 7; }
                            TiempoDeAtaque = TiempoDeEspera;
                        }
                        else { TiempoDeAtaque -= Time.deltaTime; }

                    }
                    else
                    {
                        TiempoDeAtaque -= Time.deltaTime;
                    }
                }
                else { Rb2D.velocity = Vector2.zero; }
                break;
            case Enemigos.Fly:
                if (!SufrirDaño)
                {
                    Empujes.SetActive(false);
                    Box.offset = new Vector2(-0.01668851f, 0.0595618f);
                    Box.size = new Vector2(0.4470131f, 0.3742403f);
                    Rb2D.gravityScale = 0;
                    Animator.SetBool("Fly", true);
                    Animator.SetBool("Fly_movimiento", true);
                    transform.position = Vector2.MoveTowards(transform.position, Punto[i].transform.position, SpeedFly * Time.deltaTime);
                    if (Punto[i].transform.position.x < transform.position.x) { Sprite.flipX = false; } else { Sprite.flipX = true; }
                    if (Vector2.Distance(transform.position, Punto[i].transform.position) < 0.1f)
                    {

                        if (TiempoFly < 0)
                        {
                            i++;
                            TiempoFly = TiempoDeEsperaFly;
                            if (i > 1) { i = 0; }
                        }
                        else
                        {
                            TiempoFly -= Time.deltaTime;
                            Animator.SetBool("Fly_movimiento", false);
                        }
                    }
                }
                else { Rb2D.velocity = Vector2.zero; }
                break;
            case Enemigos.Globin:
                if (!SufrirDaño)
                {
                    Empujes.SetActive(false);
                    Box.offset = new Vector2(0.1491423f, -0.07385114f);
                    Box.size = new Vector2(0.5402526f, 0.928192f);
                    Rb2D.gravityScale = 1;
                    Animator.SetBool("GloblinA_Idle", true);
                    if (Vector2.Distance(transform.position, Player.transform.position) < RangoDeVision)
                    {
                        if (Player.transform.position.x < transform.position.x) { Sprite.flipX = true; } else { Sprite.flipX = false; }
                        if (TiempoDeAtaque < 0)
                        {
                            Animator.SetBool("GloblinA_Movimiento", false);
                            Animator.Play("GloblinA_Ataque");
                            if (Sprite.flipX) { Bala.Speed = -7; } else { Bala.Speed = 7; }
                            TiempoDeAtaque = TiempoDeEspera;
                        }
                        else { TiempoDeAtaque -= Time.deltaTime; }

                    }
                    else
                    {
                        TiempoDeAtaque -= Time.deltaTime;
                        Animator.SetBool("GloblinA_Movimiento", true);
                        PosX = Punto[i].transform.position.x;
                        PosY = transform.position.y;
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(PosX, PosY), SpeedFly * Time.deltaTime);
                        if (Punto[i].transform.position.x < transform.position.x) { Sprite.flipX = true; } else { Sprite.flipX = false; }
                        if (Vector2.Distance(transform.position, new Vector2(PosX, PosY)) < 0.1f)
                        {

                            if (TiempoFly < 0)
                            {
                                i++;
                                TiempoFly = TiempoDeEsperaFly;
                                if (i > 1) { i = 0; }
                            }
                            else
                            {
                                TiempoFly -= Time.deltaTime;
                                Animator.SetBool("GloblinA_Movimiento", false);
                            }
                        }
                    }
                }
                else { Rb2D.velocity = Vector2.zero; }
                break;
            case Enemigos.Mushroon:
                if (!SufrirDaño)
                {
                    Box.offset = new Vector2(-0.02761306f, -0.2303469f);
                    Box.size = new Vector2(0.7849402f, 0.4621089f);
                    Rb2D.gravityScale = 1;
                    Animator.SetBool("MushroonM", true);
                    if (!Empuje.SaltoActivo)
                    {
                        Empujes.SetActive(true);
                        PosX = Punto[i].transform.position.x;
                        PosY = transform.position.y;
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(PosX, PosY), SpeedFly * Time.deltaTime);
                        if (Punto[i].transform.position.x < transform.position.x) { Sprite.flipX = true; } else { Sprite.flipX = false; }
                        if (Vector2.Distance(transform.position, new Vector2(PosX, PosY)) < 0.1f)
                        {

                            if (TiempoFly < 0)
                            {
                                i++;
                                TiempoFly = TiempoDeEsperaFly;
                                if (i > 1) { i = 0; }
                            }
                            else
                            {
                                TiempoFly -= Time.deltaTime;
                                Animator.SetBool("MushroonM", false);
                            }
                        }
                    }
                    else
                    {
                        Empujes.SetActive(false);
                        Animator.Play("Mushroon_SaltoAlPj");
                        if (TiempoDeEsperaMush < 0)
                        {
                            Animator.Play("Mushroon_SaltoAlPj2");
                            TiempoDeEsperaMush = 3f;
                        }
                        else
                        {
                            TiempoDeEsperaMush -= Time.deltaTime;
                        }
                    }
                }
                else { Rb2D.velocity = Vector2.zero; }
                break;
            case Enemigos.Slime:
                if (!SufrirDaño)
                {
                    Empujes.SetActive(false);
                    Box.offset = new Vector2(-0.004821658f, -0.2024379f);
                    Box.size = new Vector2(0.8088621f, 0.6614586f);
                    Rb2D.gravityScale = 1;
                    if (Vector2.Distance(transform.position, Player.transform.position) < RangoDeVision)
                    {
                        if (Player.transform.position.x < transform.position.x) { Sprite.flipX = false; } else { Sprite.flipX = true; }
                        Animator.SetBool("Slime_Movimiento", true);
                        PosXPlayer = Player.transform.position.x;
                        PosY = transform.position.y;
                        transform.position = Vector2.MoveTowards(transform.position, new Vector2(PosXPlayer, PosY), SpeedFly * Time.deltaTime);
                        if (TiempoDeSaltoSlime < 0 && suelo) { Rb2D.velocity = new Vector2(0, 0.3f); TiempoDeSaltoSlime = 0.17f; } else { TiempoDeSaltoSlime -= Time.deltaTime; }
                    }
                    else
                    {
                        if (PuntoDeInicio.x < transform.position.x) { Sprite.flipX = false; } else { Sprite.flipX = true; }
                        transform.position = Vector2.MoveTowards(transform.position, PuntoDeInicio, SpeedFly * Time.deltaTime);
                        if (transform.position.x == PuntoDeInicio.x)
                        {
                            Rb2D.velocity = Vector2.zero;
                            Animator.SetBool("Slime", true);
                            Animator.SetBool("Slime_Movimiento", false);
                        }
                    }
                }
                break;
            case Enemigos.Worm:
                if (!SufrirDaño)
                {
                    Empujes.SetActive(false);
                    Box.offset = new Vector2(-0.004821658f, -0.06888688f);
                    Box.size = new Vector2(0.8088621f, 0.3943565f);
                    Rb2D.gravityScale = 1;
                    Animator.SetBool("Worm", true);
                    PosX = Punto[i].transform.position.x;
                    PosY = transform.position.y;
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(PosX, PosY), SpeedFly * Time.deltaTime);
                    if (Punto[i].transform.position.x < transform.position.x) { Sprite.flipX = false; } else { Sprite.flipX = true; }
                    if (Vector2.Distance(transform.position, new Vector2(PosX, PosY)) < 0.1f)
                    {

                        if (TiempoFly < 0)
                        {
                            i++;
                            TiempoFly = TiempoDeEsperaFly;
                            if (i > 1) { i = 0; }
                        }
                        else
                        {
                            TiempoFly -= Time.deltaTime;
                        }
                    }
                }
                else { Rb2D.velocity = Vector2.zero; }
                break;

        }

    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, RangoDeVision);
        // fly
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Punto[0].transform.position, Punto[1].transform.position);
    }
    // Bomber Goblin
    public void Ataque()
    {
        GameObject obj = Instantiate(Bomba) as GameObject;
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
    }
    // reposo
    public void Descanso()
    {

    }
    // GoblinA
    public void Ataque2()
    {
        GameObject obj = Instantiate(Balas) as GameObject;
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
    }
    // Mushroon
    public void Actividad()
    {
        Empuje.SaltoActivo = false;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("GROUND"))
            {
            suelo = true;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GROUND"))
        {
            suelo = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ArmaDEPlayer"))
        {
            SufrirDaño = true;
        }
    }
}
