using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class CharController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float playerSpeed;
    public float jumpPower;
    public int directionInput;
    public bool facingRight = true;
    public bool loose;
    public Animator am;
    public bool groundCheck;

    public bool isGround;
    Transform grounded;
    public LayerMask layerMask;

    public bool waisted;
    Transform hitbox;
    public LayerMask layerMask2;

    public bool winning;
    Transform winHitbox;
    public LayerMask layerMask3;



    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        am = GetComponent<Animator>();
        grounded = GameObject.Find(this.name + "/grounded").transform;
        hitbox = GameObject.Find(this.name + "/hitbox").transform;
        winHitbox = GameObject.Find(this.name + "/winHitbox").transform;
    }


    void Update()
    {
        if ((directionInput < 0) && (facingRight))
        {
            Flip();
        }
        if ((directionInput > 0) && (!facingRight))
        {
            Flip();
        }
        groundCheck = true;

        isGround = Physics2D.Linecast(transform.position, grounded.position, layerMask);
        am.SetBool("jump", !isGround);

        //waisted = Physics2D.Linecast(transform.position, hitbox.position, layerMask2);
        //am.SetBool("loose", waisted);

        winning = Physics2D.Linecast(transform.position, winHitbox.position, layerMask3);
        am.SetBool("winning", winning);

        winning = loose;


        int ABSdirectionInput = Math.Abs(directionInput);
        am.SetFloat("speed", ABSdirectionInput);

        if (loose)
        {
            Invoke("restart", 3f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        am.SetBool("loose", true);
        loose = true;
        directionInput = 0;
    }
    void restart()
    {
        SceneManager.LoadScene("PushkinskayaStreet", LoadSceneMode.Single);
    }



    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(playerSpeed * directionInput, rb2d.velocity.y);
    }

    public void Move(int InputAxis)
    {

        if (!loose)
        {
            directionInput = InputAxis;
        }

    }

    public void Jump(bool isJump)
    {
        if (!loose)
        {
            isJump = groundCheck;
            var position = transform.position;
            var x = position.x;
            var y = position.y;


            if (isGround)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpPower);
            }
            if ((x >= 6050) && (x <= 6200) && (y >= 150))
            {
                transform.position = new Vector3(6120, -710, 1);
            }
            if (y < 150)
            {
                transform.position = new Vector3(6120, 240, 1);
            }
        }
    }

    void Flip()
    {
        if (!loose)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }



    /*void OnCollisionEneter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            am.SetBool("loose", true);
            loose = true;
        }
    }
    */


}

