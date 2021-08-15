using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mazo : MonoBehaviour
{  public Animator Animator;
    public enum Mazos
    {
        VueltaCompleta,MediaSuperior
    }
    public Mazos Cambio;
    private void Start()
    {
        switch (Cambio)
        {
            case Mazos.VueltaCompleta:
                Animator.SetBool("Media", false);
                break;
            case Mazos.MediaSuperior:
                Animator.SetBool("", true);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
  

    
}
