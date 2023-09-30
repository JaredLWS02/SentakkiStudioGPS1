using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerattack : MonoBehaviour
{
    private bool canAttack;
    public static bool isAttacking;
    [SerializeField] private GameObject playerattackhitbox;
    [SerializeField] private TextMeshProUGUI combotext;

    public List<attackscirptableobject> combo;
    private float lastclickedTime;
    private float lastcomboEnd;
    private int combocounter;
    private int innercomboUI;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        combotext.text = "x 0";
    }

    // Update is called once per frame
    void Update()
    {
        combotext.text = ("x " + innercomboUI);
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("is Attacking");
            if (movement.instance.IsGrounded())
            {
                Attack();
            }
        }
        ExitAttack();
    }

    void Attack()
    {
        if (Time.time - lastcomboEnd > 0.5f && combocounter <= combo.Count)
        {
            CancelInvoke("EndCombo");

            if(Time.time - lastclickedTime >= 0.6f)
            {
                isAttacking = true;
                Debug.Log("attack combo");
                anim.runtimeAnimatorController = combo[combocounter].animatorOV;
                anim.Play("attack", 0, 0);
                combocounter++;
                innercomboUI++;
                lastclickedTime = Time.time;

                if(combocounter >= combo.Count)
                {
                    combocounter = 0;
                }
            }
        }
    }

    void ExitAttack()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            Invoke("EndCombo", 2);
        }
    }

    void EndCombo()
    {
        Debug.Log("endcombo");
        combocounter = 0;
        lastcomboEnd = Time.time;
        innercomboUI = 0;

    }

    private IEnumerator freezeframe()
    {
        isAttacking = false;
        if (canAttack)
        {
            anim.enabled = false;
            yield return new WaitForSeconds(0.6f);
            anim.enabled = true;
        }
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
}
