using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class attack : MonoBehaviour
{
    public static attack instance;

    private bool canAttack;
    private bool plungeAttack;
    private bool attackCooldown = false;
    private bool plungeCooldown = false;
    public bool isPlunging;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerattackhitbox;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        instance = this;
    }

    void Update()
    {
        Debug.DrawRay(player.transform.position, Vector3.down * 2.5f, Color.blue);

        if (!movement.instance.isDashing)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (Physics2D.Raycast(player.transform.position, Vector2.down, 2.5f, groundLayer))
                {
                    if(movement.instance.IsGrounded() && !attackCooldown)
                    {
                        StartCoroutine(Attack());
                    }
                }
                else
                {
                    if(!plungeCooldown && !isPlunging)
                    {
                        StartCoroutine(PlungeAttack());
                    }
                }
                
            }
        }

    }

/*    private void FixedUpdate()
    {
        Debug.Log(rb.velocity.y);
    }*/
    private IEnumerator Attack()
    {
        attackCooldown = true;
        if(canAttack)
        {
            Debug.Log("Hit");
        }
        else
        {
            Debug.Log("miss");
        }
        yield return new WaitForSeconds(0.3f);
        attackCooldown = false;
    }

    private IEnumerator PlungeAttack()
    {
        isPlunging = true;
        plungeCooldown = true;
        rb.AddForce(Vector2.down * 40f, ForceMode2D.Impulse);

        if (Physics2D.Raycast(playerattackhitbox.transform.position, Vector2.down, 7f, enemyLayer))
        {
            plungeAttack = true;
            Debug.Log(plungeAttack);
        }
        else 
        {
            plungeAttack = false;
            Debug.Log(plungeAttack);
        }
        yield return new WaitForSeconds(0.22f);
        isPlunging = false;
        yield return new WaitForSeconds(0.4f);
        plungeCooldown = false;
    }

    private void OnTriggerEnter2D(Collider2D attackenemy)
    {
        if (attackenemy.CompareTag("enemyHitbox") && playerattackhitbox.CompareTag( "playerAttackHitbox"))
        {
            canAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D attackenemy)
    {
        canAttack = false;
    }
}
