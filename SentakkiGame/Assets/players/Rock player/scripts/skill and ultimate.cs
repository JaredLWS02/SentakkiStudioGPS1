using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class skillandultimate : MonoBehaviour
{
    private float lastskillclickedtime;
    private Collider2D[] hitenemiesSkill;

    [SerializeField] private attackscirptableobject skillanim;
    public playerstats stats;

    [SerializeField] private GaugePoint gaugePoint;
    [SerializeField] private Animator animationskill;
    [SerializeField] private Transform skillattackpoint;
    [SerializeField] private AudioSource skillAndUltisfx;
    [SerializeField] private AudioSource ultiReadySfx;
    [SerializeField] private combomanagerUI combomanagerUI;
    private bool failskill;

    [SerializeField] private GameObject edmobject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.instance.isPaused || animationskill.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.U) && gaugePoint.gaugePointAmount > 32)
        {
            if (movement.instance.IsGrounded() && !movement.instance.isDashing)
            {
                if(!swapmechanic.instance.player1Active)
                {
                    skillP2();
                }
                else
                {
                    skillP1();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Y) && gaugePoint.gaugePointAmount == 100)
        {
            if (movement.instance.IsGrounded() && !movement.instance.isDashing)
            {
                if (!swapmechanic.instance.player1Active)
                {
                    ultimateP2();
                }
                else
                {
                    ultimateP1();
                }
            }
        }

        if(GaugePoint.Instance.gaugeBar.fillAmount >= 1 && !GaugePoint.Instance.ultiReady)
        {
            GaugePoint.Instance.ultiReady = true;
            ultiReadySfx.clip = stats.ultReadysfx;
            ultiReadySfx.Play();
        }
    }

    void skillP1()
    {
        if (Time.time - lastskillclickedtime >= stats.skillcooldown)
        {
            skillAndUltisfx.clip = stats.skillsfx;
            skillAndUltisfx.Play();
            hitenemiesSkill = Physics2D.OverlapCircleAll(skillattackpoint.position, stats.skillrange, stats.enemylayer);
            gaugePoint.ReduceGauge(33);
            //animationskill.runtimeAnimatorController = skillanim.animatorOV;
            animationskill.Play("skill", 0, 0);
            lastskillclickedtime = Time.time;

            if(hitenemiesSkill.Length <= 0)
            {
                failskill = true;
            }
            else
            {
                failskill = false;
            }
            foreach (Collider2D enemy in hitenemiesSkill)
            {
                if(enemy.CompareTag("enemy"))
                {
                    enemy.GetComponent<EnemyAi>().takeDamage(stats.skilldmg);
                }

                if (enemy.CompareTag("enemyMelee"))
                {
                    enemy.GetComponent<EnemyAiMelee>().takeDamage(stats.skilldmg);
                }
                combomanagerUI.innercomboUI++;
                combomanagerUI.checkcombostatus();
            }
        }
    }

    void skillP2()
    {
        if (Time.time - lastskillclickedtime >= stats.skillcooldown)
        {
            Instantiate(edmobject,transform.position,Quaternion.identity);
            skillAndUltisfx.clip = stats.skillsfx;
            skillAndUltisfx.Play();
            //hitenemiesSkill = Physics2D.OverlapCircleAll(skillattackpoint.position, stats.skillrange, stats.enemylayer);
            gaugePoint.ReduceGauge(33);
            //animationskill.runtimeAnimatorController = skillanim.animatorOV;
            animationskill.Play("skill", 0, 0);
            lastskillclickedtime = Time.time;
        }
    }

    void ultimateP1()
    {
       
    }

    void ultimateP2()
    {

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(skillattackpoint.position, stats.skillrange);
    }
    private void ExitSkill()
    {
        animationskill.Play("idle", 0, 0);
    }

    private void Startfreezeframe()
    {
        if (!failskill)
        {
            Time.timeScale = 0.47f;
            //atkanim.SetFloat("slow",0.6f);
            //atkanim.speed = freezeframeduration;
        }
    }

}
