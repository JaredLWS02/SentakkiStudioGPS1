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
    // attack script
    private bool failattack;
    private bool reseted;

    private float lastclickedTime;
    private float lastcomboEnd;
    private int combocounter;

    private Collider2D[] hitenemies;

    public float freezeframeduration;

    [SerializeField] private playerstats stats;

    [SerializeField] private Animator atkanim;
    [SerializeField] private combomanagerUI combomanagerUI;
    [SerializeField] private AudioSource comboAudioSource;
    [SerializeField] private Transform attackpoint;

    void Start()
    {

    }

    void Update()
    {
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
    public void Attack()
    {
        if (Time.time - lastcomboEnd > 0.5f && combocounter <= stats.combo.Count)
        {
            hitenemies = Physics2D.OverlapCircleAll(attackpoint.position, stats.atkrange, stats.enemylayer);

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

            if (Time.time - lastclickedTime >= stats.atkcooldown)
            {
                Debug.Log("attack combo");
                atkanim.runtimeAnimatorController = stats.combo[combocounter].animatorOV;
                atkanim.Play("attack", 0, 0);
                combocounter++;
                lastclickedTime = Time.time;
                comboAudioSource.Play();

                if (combocounter >= stats.combo.Count)
                {
                    combocounter = 1;
                }

                foreach (Collider2D enemy in hitenemies)
                {
                    enemy.GetComponent<EnemyAi>().takeDamage(stats.atkdmg);
                    combomanagerUI.innercomboUI++;
                    combomanagerUI.checkcombostatus();
                }

            }
        }

    }

    // reset script
    void ExitAttack()
    {
        if(atkanim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && atkanim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
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

    private void disablemovement()
    {
        GetComponent<movement>().enabled = false;
    }
    private void startfreezeframe()
    {
        if (!failattack)
        {
            atkanim.speed = freezeframeduration;
        }
    }
    private void endfreezeframe()
    {
        if (!failattack)
        {
            atkanim.speed = 1;
        }
    }
    private void enablemovement()
    {
        GetComponent<movement>().enabled = true;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackpoint.position, stats.atkrange);
    }
}
