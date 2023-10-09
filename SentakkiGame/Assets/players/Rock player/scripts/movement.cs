using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public static movement instance;

    private float horizontal;
    [SerializeField] private float speed;
    [SerializeField] private float jumpingPower;
    private bool isFacingRight = true;

    private bool canDash = true;
    public bool isDashing;
    [SerializeField] private float dashingPower;
    [SerializeField] private float dashingTime;
    [SerializeField] private float dashingCooldown;

    private float oriGravity;
    private float fallingGravity = 8f;

    private bool moveKeyPress;
    private bool m_Started;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator moveanim;


    private void OnDisable()
    {
        rb.velocity = Vector2.zero;
    }
    private void Start()
    {
        m_Started = true;
        oriGravity = rb.gravityScale;
        instance = this;
    }


    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            moveKeyPress = true;
            moveanim.SetBool("move", true);
        }
        else 
        {
            moveKeyPress = false;
            moveanim.SetBool("move", false);
        }

        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse);
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash && moveKeyPress)
        {
            Debug.Log("Dashing");
            StartCoroutine(Dash());
        }

        if(IsGrounded())
        {
            rb.gravityScale = oriGravity;
        }

        Flip();
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, new Vector2(1.5f, 0.1f), 0f, groundLayer);
    }


    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

/*        if (playerattack.isAttacking)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }*/

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

    }

    private void Flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private IEnumerator Dash()
    {
        moveanim.SetTrigger("dash");
        Debug.Log("Dash");
        canDash = false;
        isDashing = true;
        //gameObject.layer = LayerMask.NameToLayer("ghostplayer");
        float oriGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = oriGravity;
        isDashing = false;
        //gameObject.layer = LayerMask.NameToLayer("player");
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void gravitypull()
    {
        Debug.Log("switch");
        rb.gravityScale = fallingGravity;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (m_Started)
            Gizmos.DrawWireCube(groundCheck.position, new Vector2(1.5f, 0.1f));
    }
}
