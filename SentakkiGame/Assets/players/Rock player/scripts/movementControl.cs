using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementControl : MonoBehaviour
{
    public static movementControl instance;
    private float horizontal;
    private bool isFacingRight = true;

    private bool canDash = true;
    public bool isDashing;

    private float oriGravity;
    public float fallingGravity;

    public bool moveKeyPress;

    public playerstats stats;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Animator moveanim;
    [SerializeField] private AudioSource jumpSource;
    [SerializeField] private float distancebetweenImages;
    [SerializeField] private float lastImagePosX;
    [SerializeField] private Vector2 startpos;
    private bool isjumping;

    private void Start()
    {
        instance = this;
        oriGravity = rb.gravityScale;
    }

    private void Update()
    {
        //if(isjumping && IsGrounded())
        //{
        //    moveanim.Play("jump end", 0, 0);
        //    rb.gravityScale = oriGravity;

        //}
        if (isDashing)
        {
            afterimage();
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                jumpSource.Play();
                moveanim.Play("jump start", 0, 0);
                rb.AddForce(Vector2.up * stats.jumpingPower, ForceMode2D.Impulse);
            }
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            moveKeyPress = true;
            moveanim.SetBool("move", true);
            if (moveanim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f && moveanim.GetCurrentAnimatorStateInfo(0).IsTag("move"))
            {
                moveanim.Play("walk loop", 0, 0);
            }

        }
        else
        {
            moveKeyPress = false;
            moveanim.SetBool("move", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            jumpSource.Play();
            moveanim.Play("jump start", 0, 0);
            rb.AddForce(Vector2.up * stats.jumpingPower, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.I) && canDash && moveKeyPress)
        {
            StartCoroutine(Dash());
        }

        if (IsGrounded())
        {
            rb.gravityScale = oriGravity;
        }

        Flip();
        rb.velocity = new Vector2(horizontal * stats.speed, rb.velocity.y);
    }

    //private void FixedUpdate()
    //{
    //    if (isDashing)
    //    {
    //        return;
    //    }


    //}
    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, new Vector2(1.5f, 0.05f), 0f, stats.groundlayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private IEnumerator Dash()
    {
        moveanim.Play("dash", 0, 0);

        canDash = false;
        isDashing = true;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * stats.dashingPower, 0f);
        //rb.AddForce(Vector2.right * transform.localScale.x * stats.dashingPower, ForceMode2D.Impulse);
        yield return new WaitForSeconds(stats.dashingTime);
        rb.gravityScale = oriGravity;
        isDashing = false;
        gameObject.layer = LayerMask.NameToLayer("player");
        yield return new WaitForSecondsRealtime(stats.dashingCooldown);
        canDash = true;
    }

    private void idle()
    {
        moveanim.Play("idle", 0, 0);
        this.enabled = true;
    }

    //private void gravitypull()
    //{
    //    Debug.Log("switch");
    //    rb.gravityScale = fallingGravity;
    //}

    private void disablemovescript()
    {
        this.enabled = false;
    }

    private void afterimage()
    {
        AfterImagePooling.instance.GetFromPool();
        lastImagePosX = transform.position.x;
        if (Mathf.Abs(transform.position.x - lastImagePosX) > distancebetweenImages)
        {
            AfterImagePooling.instance.GetFromPool();
            lastImagePosX = transform.position.x;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, new Vector2(1.5f, 0.05f));
    }

    private void endjump()
    {
        if (IsGrounded())
        {
            moveanim.Play("jump end", 0, 0);
        }
    }
}
