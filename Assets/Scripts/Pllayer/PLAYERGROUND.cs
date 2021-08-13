using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYERGROUND : MonoBehaviour
{
    [HideInInspector]
    public bool IsJump = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        IsJump = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        IsJump = true;

    }
}
