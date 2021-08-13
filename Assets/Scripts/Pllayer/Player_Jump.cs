using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Jump : MonoBehaviour
{
    //public float RayDistance;
    public float JumpForce;
    [Tooltip("El tiempo entre cada salto se debe modificar antes de inciar el juego")]
    public float SecondsForNextJump;
    public PLAYERGROUND pg;


    private Rigidbody2D rb2d;
    private Animator anim;
    //[HideInInspector]
    //public bool IsJump;
    [HideInInspector]
    public bool IsOverEnemy;

    private bool AvailableJump = true; // Its true for start

    // para el salto y doble con presion de boton
    // Salto
    float Fall = 0.5f;
    float Low = 1f;
    bool CamDoubleJump;
    public bool DobleSalto;
    private void Awake()
    {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        anim = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        //! Animation
        anim.SetBool("jump", pg.IsJump);

        if (Input.GetKey("space"))
        {
            if (!pg.IsJump) { rb2d.velocity = new Vector2(rb2d.velocity.x, JumpForce); CamDoubleJump = true; }
            else { if (DobleSalto) { if (Input.GetKeyDown(KeyCode.Space)) { if (CamDoubleJump) {  rb2d.velocity = new Vector2(rb2d.velocity.x, JumpForce); CamDoubleJump = false; } } } }
            
        }
        if (rb2d.velocity.y < 0) { rb2d.velocity += Vector2.up * Physics2D.gravity.y * (Fall) * Time.deltaTime; }
        if (rb2d.velocity.y > 0 && !Input.GetKey("space")) { rb2d.velocity += Vector2.up * Physics2D.gravity.y * (Low) * Time.deltaTime; }
    }

    private void FixedUpdate()
    {
        // ! Draw ray for ENEMY DETECTION
        //RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, RayDistance);

        //IsJump = true;

        ////? Is hit colliding?
        //if (hit.collider != null)
        //{
        //    if (hit.collider.tag.Equals("ENEMY"))
        //    {
        //        //IsJump = false;
        //        IsOverEnemy = true;
        //    }
        //    else
        //    {
        //        IsOverEnemy = false;

        //    }
        //}
       
        // For 2 different keys usage
        if (Input.GetAxis("Vertical")>0 && !pg.IsJump)
        {
            if (AvailableJump)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, JumpForce);
                AvailableJump = false;
                StartCoroutine(NextJump());
            }
        }
    }

    private IEnumerator NextJump()
    {
        yield return new WaitForSeconds(SecondsForNextJump);
        AvailableJump = true;
    }
}