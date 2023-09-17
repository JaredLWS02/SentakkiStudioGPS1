using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class movement : MonoBehaviour
{
    public static movement instance;

    private float horizontal;
    public float speed;
    public float jumpingPower;
    private bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing;
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;

    private float oriGravity;
    private float fallingGravity = 8f;

    public bool moveKeyPress;
    private bool iframe = false;
/*    private bool m_Started;*/

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
/*        m_Started = true;*/
        oriGravity = rb.gravityScale;
        instance = this;
    }

    
    private void Update()
    {
        if(isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
/*            Debug.Log("Press");*/
            moveKeyPress = true;
        }
        else 
        {
/*            Debug.Log("NotPress");*/
            moveKeyPress = false;
        }

        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse);
            
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash && IsGrounded() && moveKeyPress)
        {
            Debug.Log("Dashing");
            StartCoroutine(Dash());
        }

        Flip();

        if(rb.velocity.y >= 0f)
        {
            rb.gravityScale = oriGravity;
        }
        else
        {
            rb.gravityScale = fallingGravity;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, new Vector2(0.8f, 0.1f), 0f, groundLayer);
    }

   private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
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
        Debug.Log("Dash");
        canDash = false;
        isDashing = true;
        iframe = true;
/*        float oriGravity = rb.gravityScale;
        rb.gravityScale = 0f;*/
        Vector2 velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        rb.AddForce(velocity, ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashingTime);
/*        rb.gravityScale = oriGravity;*/
        isDashing = false;
        iframe = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

/*    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (m_Started)
            Gizmos.DrawWireCube(groundCheck.position, new Vector2(1, 0.1f));
        *//*            Gizmos.DrawWireSphere(groundCheck.position,0.4f);*//*
    }*/
}
