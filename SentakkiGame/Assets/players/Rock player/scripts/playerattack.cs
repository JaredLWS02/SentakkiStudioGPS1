using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class playerattack : MonoBehaviour
{
    // swap script
    [SerializeField] private SwapScript swap;
    [SerializeField] private AudioSource p1;
    [SerializeField] private AudioSource p2;
    [SerializeField] private Animator animplayer2;
    public bool player1Active = true;
    public bool canSwap = true;

    // attack script
    public float atkDmg = 20;
    private bool failattack;
    private bool reseted;

    private float lastclickedTime;
    private float lastcomboEnd;
    private int combocounter;

    private Animator anim;

    public float attackcooldown;
    public float freezeframeduration;

    [SerializeField] private List<attackscirptableobject> combo;
    [SerializeField] private combomanagerUI combomanagerUI;
    [SerializeField] private AudioSource comboAudioSource;

    [SerializeField] private Transform attackpoint;
    [SerializeField] private float attackrange;
    [SerializeField] private LayerMask enemylayer;
    [SerializeField] private Collider2D [] hitenemies;

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
            if(canSwap)
            {
                SwitchPlayer();
            }
        }

        // Attack input
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (movement.instance.IsGrounded() && !movement.instance.isDashing)
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
            hitenemies = Physics2D.OverlapCircleAll(attackpoint.position, attackrange, enemylayer);

            if (hitenemies.Length >= 1)
            {
               failattack = false;
               reseted = true;
               CancelInvoke("EndCombo");
            }
            else
            {
                combocounter = 0;
                failattack = true;
            }


            if (Time.time - lastclickedTime >= attackcooldown)
            {
                Debug.Log("attack combo");
                anim.runtimeAnimatorController = combo[combocounter].animatorOV;
                anim.Play("attack", 0, 0);
                combocounter++;
                lastclickedTime = Time.time;
                comboAudioSource.Play();

                if (combocounter >= combo.Count)
                {
                    combocounter = 1;
                }

                foreach (Collider2D enemy in hitenemies)
                {
                    enemy.GetComponent<EnemyAi>().takeDamage(atkDmg);
                    combomanagerUI.innercomboUI++;
                    combomanagerUI.checkcombostatus();
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
            Invoke("EndCombo", 1);
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


    private void disablemovement()
    {
        GetComponent<movement>().enabled = false;
    }
    private void startfreezeframe()
    {
        if (!failattack)
        {
            anim.speed = freezeframeduration;
        }
    }
    private void endfreezeframe()
    {
        if (!failattack)
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
            StartCoroutine(swapCooldown());
        }
        else
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = swap.character2;
            Attack();
            p1.volume = 0.2f;
            p2.volume = 0.0f;
            player1Active = true;
            StartCoroutine(swapCooldown());
        }
    }

    IEnumerator swapCooldown()
    {
        canSwap = false;
        yield return new WaitForSeconds(5);
        canSwap = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackpoint.position, attackrange);
    }
}
