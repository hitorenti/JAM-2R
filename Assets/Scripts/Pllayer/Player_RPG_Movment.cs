using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_RPG_Movment : MonoBehaviour
{
    public float speed;

    void FixedUpdate()
    {
        Vector3 Dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        this.transform.position += Dir * speed;
    }
}
