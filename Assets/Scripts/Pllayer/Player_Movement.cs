using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Movement : MonoBehaviour
{
    // EVENT
    public delegate void DELDashingLeftTime(float left);
    public event DELDashingLeftTime DashingLeftTime;

    public delegate void DELDashingTime(float left);
    public event DELDashingTime OnDashing;

    public float speed;
    public float DashSpeed;
    public float NextDashingSeconds;
    public float DashDurationSeconds;
    public float SecondsForDestroyEnemy;
    public Player_Attack pa;
    public BoxCollider2D AtackColl;

    private Rigidbody2D rb2d;
    private Animator anim;
    private bool dashing;
    private bool NextDashingAvailable = true;

    // dash
    // Dash Simple
    public bool DashBool;
    float DashTiempo;
    float DashRecuperacion;

    public static bool DashActivo=false;
    public SpriteRenderer Sprite;
    public float DashT;
    public float DTiempo;
    public static float SpeedDash = 10;
    private void Awake()
    {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        anim = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (!DashActivo)
        {
            //! Animations
            if (dashing)
            {
                if (rb2d.velocity.x > 0)
                {
                    anim.SetBool("dashing", true);
                    this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    AtackColl.offset = new Vector2(Mathf.Abs(AtackColl.offset.x), AtackColl.offset.x);


                }
                else if (rb2d.velocity.x < 0)
                {
                    anim.SetBool("dashing", true);
                    this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    AtackColl.offset = new Vector2(-AtackColl.offset.x, AtackColl.offset.x);
                }
            }
            else
            {
                anim.SetBool("dashing", false);

                if (Input.GetAxisRaw("Horizontal") == 0)
                {
                    anim.SetBool("run", false);

                }
                else if (rb2d.velocity.x > 0)
                {
                    anim.SetBool("run", true);
                    this.gameObject.GetComponent<SpriteRenderer>().flipX = false;

                }
                else if (rb2d.velocity.x < 0)
                {
                    anim.SetBool("run", true);
                    this.gameObject.GetComponent<SpriteRenderer>().flipX = true;

                }
            }
        }



        if (Input.GetKeyDown(KeyCode.K))
        {
            if (DashRecuperacion < 0)
            {
                DashBool = true;
            }

        }
        if (DashBool)
        {
            DashActivo = true;
            anim.Play("DobleSalto");
            Physics2D.IgnoreLayerCollision(3, 6, true);
            rb2d.velocity = Vector2.zero;
            if (!Sprite.flipX) { transform.Translate((Vector3.right) * SpeedDash * Time.deltaTime); }
            else { transform.Translate((Vector3.left) * SpeedDash * Time.deltaTime); }
            rb2d.gravityScale = 0;
            Invoke("Dash_false", 0f);
            if (DashTiempo < 0)
            {
                DashTiempo = 0.3f;
                DashBool = false;
                DashRecuperacion = 1f;
            }
            else { DashTiempo -= Time.deltaTime; }
        }
        else
        {
            DashActivo = false;
            rb2d.gravityScale = 1;
            Physics2D.IgnoreLayerCollision(3, 6, false);
        }
        DashRecuperacion -= Time.deltaTime;
    }

    private void FixedUpdate()
    {

        // For 2 different keys usage and x direciton
        float XDir = Input.GetAxis("Horizontal");

        //! Raw for detect idle state
        if (!DashActivo)
        {
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
                if (dashing)
                {
                    StopCoroutine(Dash());
                    dashing = false;
                }


            }
            else
            {
                if (dashing)
                {
                    rb2d.velocity = new Vector2(XDir * DashSpeed, rb2d.velocity.y);

                }
                else
                {
                    rb2d.velocity = new Vector2(XDir * speed, rb2d.velocity.y);

                }

            }
        }


        // Dash
       /* if (Input.GetKeyDown(KeyCode.F))
        {

            if (NextDashingAvailable && XDir != 0 && !dashing)
            {
                // Dash
                dashing = true;
                StartCoroutine(Dash());

            }

        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            if(XDir != 0)
            {
                StopCoroutine(Dash());
                dashing = false;
            }
        }*/
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ignore on collision enter while player is dashing
        if (collision.transform.tag.Equals("ENEMY"))
        {
            if (dashing)
            {
                // pass over enemy
                Physics2D.IgnoreCollision(collision.collider, this.GetComponent<Collider2D>());
                // OVER ENEMY
                int initialLayer = this.GetComponent<SpriteRenderer>().sortingOrder;
                this.GetComponent<SpriteRenderer>().sortingOrder = 90;
                StartCoroutine(WaitForDestroyEnemy(collision.collider, initialLayer));
            }
        }
    }

    private float dashTimeBuffer;
    private float leftTime = 0;
    IEnumerator NextDashing()
    {
        NextDashingAvailable = false;
        for (dashTimeBuffer = NextDashingSeconds;dashTimeBuffer > 0; dashTimeBuffer -= Time.deltaTime)
        {
            //Calculate time
            leftTime = float.Parse((1 - (dashTimeBuffer / NextDashingSeconds)).ToString("F2"));
            // Report
            DashingLeftTime(leftTime);

            
            //Debug.Log($"Time for next dashing: {leftTime}");

            yield return null;
            
        }

        if(leftTime > 0.9)
        {
            NextDashingAvailable = true;

        }

    }

    private float dshBuff;
    private float crtdsh = 1;
    IEnumerator Dash()
    {
        for(dshBuff = DashDurationSeconds; dshBuff > 0; dshBuff -= Time.deltaTime)
        {
            // CALCULATE
            crtdsh = float.Parse((dshBuff/DashDurationSeconds).ToString("F2"));
            // REPORT
            OnDashing(crtdsh);

            yield return null;
        }

        //! PREV
        //yield return new WaitForSeconds(DashDurationSeconds);
        //dashing = false;
        //StartCoroutine(NextDashing());
        //! PREV

        if (crtdsh < 0.1)
        {
            dashing = false;
            StartCoroutine(NextDashing());
        }


    }

    IEnumerator WaitForDestroyEnemy(Collider2D enemy,int layer)
    {
        yield return new WaitForSeconds(SecondsForDestroyEnemy);

        if(enemy != null)
        {
            // Destroy enemy
            if (enemy.name == "Empuje")
            {
                enemy.transform.parent.GetComponent<Enemy_Damage>().Damage(pa.DamageToEnemy + 2);

            }
            else if (enemy.tag.Equals("ENEMY") && enemy.name != "bomb" && enemy.name != "bala")
            {
                enemy.GetComponent<Enemy_Damage>().Damage(pa.DamageToEnemy + 2);

            }

            // Set star layer
            this.GetComponent<SpriteRenderer>().sortingOrder = layer;

            // Turn off ignore collision
            Physics2D.IgnoreCollision(enemy, this.GetComponent<Collider2D>(), false);
        }


    }
}
