using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    public float smooth;
    Rigidbody2D rb2d;
    bool facingRight = true;
    public Transform GroundCheck;
    public LayerMask GroundLayer;
    private bool isGrounded;
    public float checkRadius;
    public int maxJumps;
    private int extraJumps;
    public int maxspeed;
    public int normalspeed;

    private Animator anim;

    private float xInput;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PowerUp")
        {
            Destroy(collision.gameObject);
            gameObject.tag = "PowerUp";
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            StartCoroutine("reset");
        }
    }
    IEnumerator reset()
    {
        yield return new WaitForSeconds(5f);
        gameObject.tag = "Player";
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        maxspeed = normalspeed * 2;
        extraJumps = maxJumps;
        rb2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(xInput * normalspeed, rb2d.velocity.y);

        rb2d.velocity = Vector2.SmoothDamp(rb2d.velocity, targetVelocity, ref targetVelocity, Time.deltaTime * smooth);


        // the input is moving the player right and the player is facing left...
        if (xInput > 0 && !facingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (xInput < 0 && facingRight)
        {
            // ... flip the player.
            Flip();
        }

    }
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, GroundLayer);

        xInput = Input.GetAxisRaw("Horizontal");


        anim.SetBool("Run", xInput != 0);

        if (rb2d.velocity.y != 0)
        {
            anim.SetBool("Jump", true);
            anim.SetBool("Run", false);

        }
        else
        {
            anim.SetBool("Jump", false);
        }


        if (isGrounded == true)
        {
            extraJumps = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb2d.velocity = Vector2.up * jumpForce;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb2d.velocity = Vector2.up * jumpForce;
            extraJumps--;

        }
        if (Input.GetKeyDown("s"))
        {
            StartCoroutine(DoSomething());

        }

    }
    IEnumerator DoSomething()
    {
        normalspeed = maxspeed;
        yield return new WaitForSeconds(5f);
        normalspeed = 10;
    }

    void Flip()
    {
        //Invert the facingRight variable
        facingRight = !facingRight;

        //Flip the Character
        transform.Rotate(0f, 180f, 0f);

    }


}




