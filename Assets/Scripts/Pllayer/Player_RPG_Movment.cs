using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_RPG_Movment : MonoBehaviour
{
    public float speed;

    private int AnimationDir;
    private Animator anim;

    private void Awake()
    {
        anim = this.GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetInteger("DIRECTION", AnimationDir);
    }

    void FixedUpdate()
    {
        Vector3 Dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if(Input.GetKey(KeyCode.D))
        {
            AnimationDir = 2;
            this.transform.position += Dir * speed;
            this.GetComponent<SpriteRenderer>().flipX = false;


        }
        else if(Input.GetKey(KeyCode.A))
        {
            AnimationDir = 2;
            this.transform.position += Dir * speed;
            this.GetComponent<SpriteRenderer>().flipX = true;

        }
        else if(Input.GetKey(KeyCode.W))
        {
            AnimationDir = 3;
            this.transform.position += Dir * speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            AnimationDir = 1;

            this.transform.position += Dir * speed;
        }
        else
        {
            AnimationDir = 0;
        }

    }
}
