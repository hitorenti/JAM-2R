using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYERGROUND : MonoBehaviour
{
    [HideInInspector]
    public bool IsJump = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("GROUND"))
        {
            IsJump = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("GROUND"))
        {
            IsJump = true;
        }
    }
}
