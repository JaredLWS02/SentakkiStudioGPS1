using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class playerattack : MonoBehaviour
{
    private bool canAttack;
    private bool failattack;
    private bool reseted;
    [SerializeField] private GameObject playerattackhitbox;

    public List<attackscirptableobject> combo;
    private float lastclickedTime;
    private float lastcomboEnd;
    private int combocounter;
    private bool ishit = true;

    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (movement.instance.IsGrounded())
            {
                Attack();
            }
        }

        if(reseted)
        {
            ExitAttack();
        }
    }

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
                    combocounter = 0;
                }

                if (!failattack)
                {
                    combomanagerUI.instance.innercomboUI++;
                }

            }
        }

    }

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
        Debug.Log("endcombo");
        combocounter = 0;
        lastcomboEnd = Time.time;
        combomanagerUI.instance.innercomboUI = 0;
    }

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
}
