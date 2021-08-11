using UnityEngine;

public class Puerta : MonoBehaviour
{
    public Animator Animator;
    bool Abrir = false;
    public enum Puertas
    {
        Derecha, Izquierda
    }
    public Puertas Cambio;
    private void Update()
    {
        switch (Cambio)
        {
            case Puertas.Derecha:
                Animator.SetBool("Izquierda", false);
                if (Abrir)
                {
                    if (Player_Manager.LLaves > 0)
                    {
                        Animator.SetBool("DerechaAbierta", true);
                    }
                }
                break;
            case Puertas.Izquierda:
                Animator.SetBool("Izquierda", true);
                if (Abrir)
                {
                    if (Player_Manager.LLaves > 0)
                    {
                        Animator.SetBool("IzquierdaAbierta", true);
                    }
                }
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Abrir = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Abrir = false;
    }
}
