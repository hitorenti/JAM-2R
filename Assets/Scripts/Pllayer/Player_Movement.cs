using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Movement : MonoBehaviour
{
    public float speed;
    public float DashSpeed;
    public float NextDashingSeconds;
    public float DashDurationSeconds;
    public float SecondsForDestroyEnemy;

    private Rigidbody2D rb2d;
    private Animator anim;
    private bool dashing;
    private bool NextDashingAvailable = true;

    private void Awake()
    {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        anim = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        //! Animations
        if (dashing)
        {
            if (rb2d.velocity.x > 0)
            {
                anim.SetBool("dashing", true);
                this.gameObject.GetComponent<SpriteRenderer>().flipX = false;

            }else if(rb2d.velocity.x < 0)
            {
                anim.SetBool("dashing", true);
                this.gameObject.GetComponent<SpriteRenderer>().flipX = true;

            }
        }
        else
        {
            anim.SetBool("dashing", false);


            if (rb2d.velocity.x > 0)
            {
                anim.SetBool("run", true);
                this.gameObject.GetComponent<SpriteRenderer>().flipX = false;

            }
            else if (rb2d.velocity.x < 0)
            {
                anim.SetBool("run", true);
                this.gameObject.GetComponent<SpriteRenderer>().flipX = true;

            }
            else
            {
                anim.SetBool("run", false);

            }
        }

    }

    private void FixedUpdate()
    {

        // For 2 different keys usage and x direciton
        float XDir = Input.GetAxis("Horizontal");

        //! Raw for detect idle state
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


        // Dash
        if (Input.GetKeyDown(KeyCode.F))
        {

            if (NextDashingAvailable && XDir != 0)
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
        }

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
                StartCoroutine(WaitForDestroyEnemy(collision.gameObject, initialLayer));
            }
        }
    }

    private float dashTimeBuffer;
    IEnumerator NextDashing()
    {
        NextDashingAvailable = false;

        for (dashTimeBuffer = NextDashingSeconds;dashTimeBuffer > 0; dashTimeBuffer -= Time.deltaTime)
        {
            Debug.Log(dashTimeBuffer);
            yield return null;
            
        }

        if(dashTimeBuffer < 0.5)
        {
            NextDashingAvailable = true;

        }

    }

    IEnumerator Dash()
    {

        yield return new WaitForSeconds(DashDurationSeconds);
        dashing = false;
        StartCoroutine(NextDashing());

    }

    IEnumerator WaitForDestroyEnemy(GameObject enemy,int layer)
    {
        yield return new WaitForSeconds(SecondsForDestroyEnemy);
        // Destroy enemy
        Destroy(enemy.gameObject);
        // Set star layer
        this.GetComponent<SpriteRenderer>().sortingOrder = layer;

    }
}
