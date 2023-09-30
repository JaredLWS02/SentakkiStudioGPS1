/*using System.Collections;
using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public class attack : MonoBehaviour
{
    public static attack instance;

    private bool canAttack;
    private bool attackcooldown = false;
    private int combocounter;
    private int innercombocounter;

    *//*    private bool plungeAttack;*/
    /*    private bool plungeCooldown = false;*/
    /*    public bool isPlunging;*//*

    private float lastclickedtime = 0f;
    private float maxcombodelay = 3f;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerattackhitbox;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TextMeshProUGUI combo;
    [SerializeField] private TextMeshProUGUI hit;
    [SerializeField] private Animator attackanim;

    private void Start()
    {
        innercombocounter = 0;
        combo.text = "x 0";
        instance = this;
    }

    void Update()
    {

        Debug.DrawRay(player.transform.position, Vector3.down * 2.5f, Color.blue);

        if (attackanim.GetBool("idle2"))
        {
            attackanim.SetBool("idle2", false);
        }


        if (Time.time - lastclickedtime > maxcombodelay)
        {
            innercombocounter = 0;
        }

        if (!movement.instance.isDashing)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
*//*                if (Physics2D.Raycast(player.transform.position, Vector2.down, 2.5f, groundLayer))
                {*//*
                    if(movement.instance.IsGrounded() && !attackcooldown)
                    {
                        Debug.Log("pressed");
                        Attack();
                    }
*//*                }*/
/*                else
                {
                    if(!plungeCooldown && !isPlunging)
                    {
                        StartCoroutine(PlungeAttack());
                    }
                }*//*                
            }
        }

        if (attackanim.GetCurrentAnimatorStateInfo(0).IsName("placedholder") || attackanim.GetCurrentAnimatorStateInfo(0).IsName("move") || attackanim.GetCurrentAnimatorStateInfo(0).IsName("dash"))
        {
            return;
        }
        else
        {
            if (attackanim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
            {
                endcooldown();
                Debug.Log(attackanim.GetCurrentAnimatorStateInfo(0).normalizedTime);
                attackanim.SetBool("idle2", true);
            }
        }
    }

    private void startcooldown()
    {
        attackcooldown = true;
    }

    private void endcooldown()
    {
        Debug.Log("cooldown reset");
        attackcooldown = false;
    }

    private void Attack()
    {
        attackanim.SetBool("move", false);
        lastclickedtime = Time.time;
        innercombocounter++;
        if(innercombocounter > 5)
        {
            innercombocounter = 2;
        }

        if(attackanim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            return;
        }

        if (innercombocounter >= 1 && (attackanim.GetCurrentAnimatorStateInfo(0).IsName("placedholder") || attackanim.GetCurrentAnimatorStateInfo(0).IsName("move")))
        {
            Debug.Log("Hit1");
            attackanim.SetTrigger("hit 1");
        }

        if (innercombocounter >= 2 && attackanim.GetCurrentAnimatorStateInfo(0).IsName("placeholder2"))
        {
            Debug.Log("hit2");
            attackanim.SetTrigger("hit 2");
        }

        if (innercombocounter >= 3 && attackanim.GetCurrentAnimatorStateInfo(0).IsName("placeholder3"))
        {
            Debug.Log("hit3");
            attackanim.SetTrigger("hit 3");
        }

        if (innercombocounter >= 4 && attackanim.GetCurrentAnimatorStateInfo(0).IsName("placeholder4"))
        {
            Debug.Log("hit4");
            attackanim.SetTrigger("hit 4");
        }

        if (innercombocounter >= 5 && attackanim.GetCurrentAnimatorStateInfo(0).IsName("placeholder5"))
        {
            Debug.Log("hit4");
            attackanim.SetTrigger("hit 1");
        }
    }

    private IEnumerator freezeframe()
    {
        if (canAttack)
        {
            attackanim.enabled = false;
            yield return new WaitForSeconds(0.2f);
            attackanim.enabled = true;
        }
    }

    private void checkidle()
    {
        attackanim.SetBool("idle2", true);
        innercombocounter = 0;
    }

    *//*    private IEnumerator PlungeAttack()
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
            yield return new WaitForSeconds(0.1f);
            isPlunging = false;
            yield return new WaitForSeconds(0.4f);
            plungeCooldown = false;
        }*//*

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
*/