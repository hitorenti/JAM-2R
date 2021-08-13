using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuasControl : MonoBehaviour
{
    public Rigidbody2D Rb2D;
    public static int DamageAlPlayer;

    public float DistanciaDer;
    public Vector3 ControlRayCastDer;
    public float DistanciaIzq;
    public Vector3 ControlRayCastIzq;


    public void Update()
    {
        RaycastHit2D Der = Physics2D.Raycast(transform.position- ControlRayCastDer, Vector2.down, DistanciaDer);
        Debug.DrawRay(transform.position - ControlRayCastDer, Vector2.down*DistanciaDer, Color.yellow);
        if(Der.collider != null)
        {
            if (Der.collider.gameObject.CompareTag("Player"))
            {
                Rb2D.gravityScale = 2;
                Rb2D.bodyType = 0;
            }
        }

        RaycastHit2D Izq = Physics2D.Raycast(transform.position - ControlRayCastIzq, Vector2.down, DistanciaIzq);
        Debug.DrawRay(transform.position - ControlRayCastIzq, Vector2.down * DistanciaIzq, Color.yellow);
        if (Izq.collider != null)
        {
            if (Izq.collider.CompareTag("Player"))
            {
                Rb2D.gravityScale = 2;
                Rb2D.bodyType = 0;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GROUND"))
        {
            Rb2D.gravityScale = 1000;
            Rb2D.velocity = Vector2.zero;
            Debug.Log("hola");
        }
    }
}
