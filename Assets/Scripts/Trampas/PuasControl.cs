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
                Rb2D.gravityScale = 10;
                Rb2D.bodyType = 0;
                Debug.Log("hola");
            }
        }

        RaycastHit2D Izq = Physics2D.Raycast(transform.position - ControlRayCastDer, Vector2.down, DistanciaIzq);
        Debug.DrawRay(transform.position - ControlRayCastIzq, Vector2.down * DistanciaIzq, Color.yellow);
        if (Izq.collider != null)
        {
            if (Izq.collider.CompareTag("Player"))
            {
                Rb2D.gravityScale = 10;
                Rb2D.bodyType = 0;
                Debug.Log("hola");
            }
        }
    }
}
