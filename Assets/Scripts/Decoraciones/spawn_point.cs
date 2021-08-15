using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_point : MonoBehaviour
{
    [Tooltip("Activar o desactivar pared invisible coomo limite del jugador")]
    public bool Wall;
    public BoxCollider2D wall;

    void Start()
    {
        wall.enabled = Wall;
    }

}
