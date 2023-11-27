using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
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
    public float extra;
    public int combocounter;

    private Collider2D[] hitenemies;

    public float freezeframeduration;

    public playerstats stats;

    [SerializeField] private Animator atkanim;
    [SerializeField] private combomanagerUI combomanagerUI;
    [SerializeField] private Transform attackpoint;
    [SerializeField] private Transform plungeattackpoint;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioSource combosource;
    [SerializeField] private AudioSource atkmissSource;
    [SerializeField] private AudioSource atksfx;
    private bool hit;
    public bool isPlunging;
    public float sizeX;
    public float sizeY;

    public bool enabledAttack;
    public bool moving;

    [SerializeField] private Collider2D[] hitenemiesSwap;
    [SerializeField] private Transform swapattackpoint;
    [SerializeField] private Vector2 swapatkSize;
    [SerializeField] private ParticleSystem hitvfx;
    void Start()
    {

    }

    void Update()
    {

        if (!enabledAttack)
        {
            return;
        }
        if(PauseMenu.instance.isPaused || atkanim.GetCurrentAnimatorStateInfo(0).IsTag("skill") || atkanim.GetCurrentAnimatorStateInfo(0).IsTag("ultimate"))
        {
            return;
        }
        //Debug.DrawRay(plungeattackpoint.position, Vector2.down * 4f, Color.blue);
        // Attack input
        if (Input.GetKeyDown(KeyCode.J) && !isPlunging)
        {
                if (movement.instance.IsGrounded())
                {
                    Attack();
                }
                else
                {
                    if(!isPlunging)
                    {
                        isPlunging = true;
                        hit = false;
                        atkanim.Play("plunge", 0, 0);
                    }
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
            attackpoint.transform.localPosition = stats.atkpointpos;
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
                //Debug.Log("attack combo");
                atkanim.runtimeAnimatorController = stats.combo[combocounter].animatorOV;
                combosource.clip = stats.combosfx[combocounter];
                atkmissSource.clip = stats.atksfx;
                if (hitenemies.Length >= 1)
                {
                    atksfx.Play();
                    combosource.Play();
                }
                else
                {
                    atkmissSource.Play();
                }
                atkanim.Play("attack", 0, 0);
                combocounter++;
                lastclickedTime = Time.time;

                if (combocounter >= stats.combo.Count)
                {
                    combocounter = 0;
                }

                foreach (Collider2D enemy in hitenemies)
                {
                    if (enemy.CompareTag("enemy"))
                    {
                        hitvfx = enemy.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>(); 
                        hitvfx.Play();
                        enemy.GetComponent<EnemyAi>().takeDamage(stats.atkdmg);
                    }
                    else if (enemy.CompareTag("enemyMelee"))
                    {
                        hitvfx = enemy.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();
                        hitvfx.Play();
                        enemy.GetComponent<EnemyAiMelee>().takeDamage(stats.atkdmg);
                    }

                    GaugePoint.Instance.RestoreGaugePoints(stats.gaugerestoreHit + extra);
                    combomanagerUI.innercomboUI++;
                    combomanagerUI.checkcombostatus();
                }
            }
        }

    }

    void PlungeAttack()
    {
        if(!hit)
        {
            Collider2D[] enemieshit = Physics2D.OverlapBoxAll(plungeattackpoint.position, new Vector2(sizeX, sizeY), 1f, stats.enemylayer);
            if (enemieshit.Length >= 1)
            {
                hit = true;
                failattack = false;
                reseted = true;
                CancelInvoke("EndCombo");
            }
            else
            {
                combocounter = 0;
                failattack = true;
            }

            if (combocounter >= stats.combo.Count)
            {
                combocounter = 1;
            }


            foreach (Collider2D enemy in enemieshit)
            {
                if (enemy.CompareTag("enemy"))
                {
                    hitvfx = enemy.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();
                    hitvfx.Play();
                    enemy.GetComponent<EnemyAi>().takeDamage(stats.atkdmg);
                }
                else if (enemy.CompareTag("enemyMelee"))
                {
                    hitvfx = enemy.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();
                    hitvfx.Play();
                    enemy.GetComponent<EnemyAiMelee>().takeDamage(stats.atkdmg);
                }
                GaugePoint.Instance.RestoreGaugePoints(stats.gaugerestoreHit + extra);
                combomanagerUI.innercomboUI++;
                combomanagerUI.checkcombostatus();
            }
        }

        if(movement.instance.IsGrounded())
        {
            isPlunging = false;
            atkanim.Play("plunge", 0, 0.6f);
        }

    }
    public void SwapAtk()
    {
        hitenemiesSwap = Physics2D.OverlapBoxAll(swapattackpoint.position, swapatkSize, 0, stats.enemylayer);
        if (hitenemiesSwap.Length >= 1)
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

        foreach (Collider2D enemy in hitenemiesSwap)
        {
            if (enemy.CompareTag("enemy"))
            {
                hitvfx = enemy.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();
                hitvfx.Play();
                enemy.GetComponent<EnemyAi>().takeDamage(stats.atkdmg);
            }

            if (enemy.CompareTag("enemyMelee"))
            {
                hitvfx = enemy.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();
                hitvfx.Play();
                enemy.GetComponent<EnemyAiMelee>().takeDamage(stats.atkdmg);
            }
            combomanagerUI.innercomboUI++;
            combomanagerUI.checkcombostatus();
        }
    }

    // reset script
    void ExitAttack()
    {
        if(atkanim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && (atkanim.GetCurrentAnimatorStateInfo(0).IsTag("attack") || atkanim.GetCurrentAnimatorStateInfo(0).IsTag("swap")))
        {
            reseted = false;

            if (combomanagerUI.innercomboUI >= 0 && combomanagerUI.innercomboUI <= 18)
            {
                Invoke("EndCombo", 2);
            }
            else if (combomanagerUI.innercomboUI >=18 && combomanagerUI.innercomboUI <= 24)
            {
                Invoke("EndCombo", 1f);
            }
            else if (combomanagerUI.innercomboUI > 24)
            {
                Invoke("EndCombo", 0.5f);
            }

        }
    }

    void EndCombo()
    {
        //combomanagerUI.removeAplhaCombo();
        //Debug.Log("endcombo");
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
            Time.timeScale = 0.0f;
            StartCoroutine(endfreezeframe());
            //atkanim.SetFloat("slow",0.6f);
            //atkanim.speed = freezeframeduration;
        }
    }
    private IEnumerator endfreezeframe()
    {
        //if (!failattack)
        //{
        yield return new WaitForSecondsRealtime(0.13f);
        Time.timeScale = 1f;
            //atkanim.SetFloat("slow", 1f);
        //}
    }
    public void enablemovement()
    {
        GetComponent<movement>().enabled = true;
    }
    private void moveforward()
    {
        if (transform.localScale.x > 0)
        {
            LeanTween.moveLocalX(this.gameObject, transform.position.x + 0.2f, 0.1f).setEaseOutExpo();
        }
        else
        {
            LeanTween.moveLocalX(this.gameObject, transform.position.x - 0.2f, 0.1f).setEaseOutExpo();
 
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackpoint.position, stats.atkrange);;
        Gizmos.DrawWireCube(plungeattackpoint.position, new Vector2(sizeX, sizeY));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(swapattackpoint.position, swapatkSize);
    }


    private void disableSwap()
    {
        GetComponent<swapmechanic>().enabled = false;
    }

    private void enableSwap()
    {
        GetComponent<swapmechanic>().enabled = true;
    }

    private void addDownForce()
    {
        rb.AddForce(new Vector2(0, -stats.jumpingPower + 2), ForceMode2D.Impulse);
    }
}
