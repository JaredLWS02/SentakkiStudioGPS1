using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class playerattack : MonoBehaviour
{
    // swap script
    public SwapScript swap;
    public AudioSource p1;
    public AudioSource p2;
    public movement playerControl;
    public Animator animplayer2;
    public bool player1Active = true;
    // attack script
    private bool canAttack;
    private bool failattack;
    private bool reseted;
    [SerializeField] private GameObject playerattackhitbox;

    [SerializeField] private List<attackscirptableobject> combo;
    private float lastclickedTime;
    private float lastcomboEnd;
    private int combocounter;
    [SerializeField] private combomanagerUI combomanagerUI;

    private Animator anim;
    
    void Start()
    {
        p1.volume = 0.2f;
        p2.volume = 0.0f;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // switch input
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchPlayer();
        }

        // Attack input
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (movement.instance.IsGrounded())
            {
                Attack();
            }
        }

        // combo interrupted
        if(reseted)
        {
            ExitAttack();
        }
    }

    // attack mechanic
    void Attack()
    {
        if (Time.time - lastcomboEnd > 0.5f && combocounter <= combo.Count)
        {
            if (canAttack)
            {
                failattack = false;
            }
            else
            {
                combocounter = 0;
                failattack = true;
            }

            if (!failattack)
            {
                reseted = true;
                CancelInvoke("EndCombo");
            }

            if (Time.time - lastclickedTime >= 0.8f)
            {
                Debug.Log("attack combo");
                anim.runtimeAnimatorController = combo[combocounter].animatorOV;
                anim.Play("attack", 0, 0);
                combocounter++;
                lastclickedTime = Time.time;

                if (combocounter >= combo.Count)
                {
                    combocounter = 1;
                }

                if (!failattack)
                {
                    combomanagerUI.innercomboUI++;
                    combomanagerUI.checkcombostatus();
                    //combomanagerUI.restoreAplha();
                }

            }
        }

    }

    // reset script
    void ExitAttack()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            Debug.Log("A");
            reseted = false;
            Invoke("EndCombo", 2);
        }
    }

    void EndCombo()
    {
        //combomanagerUI.removeAplhaCombo();
        Debug.Log("endcombo");
        combocounter = 0;
        lastcomboEnd = Time.time;
        combomanagerUI.innercomboUI = 0;
        combomanagerUI.x = 0;
    }

    // Attack sensor
    private void OnTriggerEnter2D(Collider2D attackenemy)
    {
        if (attackenemy.CompareTag("enemyHitbox") && playerattackhitbox.CompareTag("playerAttackHitbox"))
        {
            canAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D attackenemy)
    {
        canAttack = false;
    }

    private void disablemovement()
    {
        GetComponent<movement>().enabled = false;
    }
    private void startfreezeframe()
    {
        if (canAttack)
        {
            anim.speed = 0.3f;
        }
    }
    private void endfreezeframe()
    {
        if (canAttack)
        {
            anim.speed = 1;
        }
    }
    private void enablemovement()
    {
        GetComponent<movement>().enabled = true;
    }

    // switch mechanic
    public void SwitchPlayer()
    {
        if (player1Active)
        {
            GetComponent<Animator>().enabled = true;
            GetComponent<SpriteRenderer>().sprite = swap.character1;
            Attack();
            p1.volume = 0.0f;
            p2.volume = 0.2f;
            player1Active = false;
        }
        else
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = swap.character2;
            Attack();
            p1.volume = 0.2f;
            p2.volume = 0.0f;
            player1Active = true;
        }
    }
}
