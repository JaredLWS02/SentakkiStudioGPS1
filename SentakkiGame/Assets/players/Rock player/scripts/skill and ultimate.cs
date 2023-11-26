using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class skillandultimate : MonoBehaviour
{
    private float lastskillclickedtime;
    private Collider2D[] hitenemiesSkill;
    private Collider2D[] hitenemiesUlti;

    [SerializeField] private attackscirptableobject skillanim;
    public playerstats stats;

    [SerializeField] private GaugePoint gaugePoint;
    [SerializeField] private Animator animationskill;
    [SerializeField] private Transform skillattackpoint;
    [SerializeField] private Transform ultiattackpoint;
    [SerializeField] private Transform ultiattackEDMpoint;
    [SerializeField] private AudioSource skillAndUltisfx;
    [SerializeField] private AudioSource ultiReadySfx;
    [SerializeField] private combomanagerUI combomanagerUI;
    private float duration = 3f;
    private bool failskill;
    private bool failUlti;
    public float sizeX;
    public float sizeY;
    public float damagePerSecond;

    [SerializeField] private GameObject edmobject;

    // Start is called before the first frame update
    void Start()
    {
        damagePerSecond = stats.ultdmg;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.instance.isPaused || animationskill.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.H) && gaugePoint.gaugePointAmount > 32)
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

        if (Input.GetKeyDown(KeyCode.U) && gaugePoint.gaugePointAmount == 100)
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
            if (enemy.CompareTag("enemy"))
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
        if (Time.time - lastskillclickedtime >= stats.skillcooldown)
        {
            skillAndUltisfx.clip = stats.ultsfx;
            skillAndUltisfx.Play();
            hitenemiesUlti = Physics2D.OverlapCircleAll(ultiattackpoint.position, stats.ultrange, stats.enemylayer);
            gaugePoint.ReduceGauge(100);
            animationskill.Play("ulti", 0, 0);
            lastskillclickedtime = Time.time;

            bool anyEnemyHit = false;

            if (hitenemiesUlti.Length <= 0)
            {
                failUlti = true;
            }
            else
            {
                failUlti = false;
            }


            foreach (Collider2D enemy in hitenemiesUlti)
        {
            if (enemy.CompareTag("enemy"))  
            {
                {
                enemy.GetComponent<EnemyAi>().takeDamage(stats.ultdmg);
                enemy.GetComponent<StunEnemy>().Stun();
                }
                
            }

            if (enemy.CompareTag("enemyMelee"))
            {

                {

                enemy.GetComponent<EnemyAiMelee>().takeDamage(stats.ultdmg);
                enemy.GetComponent<StunEnemy>().Stun();
                }
            }


            combomanagerUI.innercomboUI++;
            combomanagerUI.checkcombostatus();
        }
        }
    }
    

    void ultimateP2()
    {
        if (Time.time - lastskillclickedtime >= stats.skillcooldown)
        {
            skillAndUltisfx.clip = stats.ultsfx;
            skillAndUltisfx.Play();
            hitenemiesUlti = Physics2D.OverlapBoxAll(ultiattackEDMpoint.position, new Vector2(sizeX, sizeY), 1f , stats.enemylayer);
            gaugePoint.ReduceGauge(100);
            animationskill.Play("ulti", 0, 0);
            lastskillclickedtime = Time.time;

            bool anyEnemyHit = false;


            if (hitenemiesUlti.Length <= 0)
            {
                failUlti = true;
            }
            else
            {
                failUlti = false;
            }


            foreach (Collider2D enemy in hitenemiesUlti)
        {
            if (enemy.CompareTag("enemy"))
            {
                StartCoroutine(DamageOverTime(enemy, stats.ultdmg, duration));
                
            }

            if (enemy.CompareTag("enemyMelee"))
            {
                StartCoroutine(DamageOverTime(enemy, stats.ultdmg, duration));
            }


            combomanagerUI.innercomboUI++;
            combomanagerUI.checkcombostatus();
        }
        }

IEnumerator DamageOverTime(Collider2D enemy, float totalDamage, float duration)
{
    float elapsedTime = 0f;
    while (elapsedTime < duration)
    {
        float damaging = totalDamage / duration * Time.deltaTime;

        if (enemy.CompareTag("enemy"))
        {
            enemy.GetComponent<EnemyAi>().takeDamage(damaging);
            enemy.GetComponent<StunEnemy>().Stun();
        }
        else if (enemy.CompareTag("enemyMelee"))
        {
            enemy.GetComponent<EnemyAiMelee>().takeDamage(damaging);
            enemy.GetComponent<StunEnemy>().Stun();
        }

        elapsedTime += Time.deltaTime;
        yield return null; // Wait until the next frame
    }
}

    }




    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(skillattackpoint.position, stats.skillrange);
        Gizmos.DrawWireSphere(ultiattackpoint.position, stats.ultrange);
        Gizmos.DrawWireCube(new Vector2(ultiattackEDMpoint.position.x,ultiattackEDMpoint.position.y), new Vector2(sizeX,sizeY));
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

    IEnumerator DamageOverTime(Collider2D enemy, float totalDamage, float duration)
{
    float elapsedTime = 0f;
    while (elapsedTime < duration)
    {
        elapsedTime += Time.deltaTime;
        
        float damaging = totalDamage / duration * Time.deltaTime;

        if (enemy.CompareTag("enemy"))
        {
            enemy.GetComponent<EnemyAi>().takeDamage(damaging);
            enemy.GetComponent<StunEnemy>().Stun();
        }
        else if (enemy.CompareTag("enemyMelee"))
        {
            enemy.GetComponent<EnemyAiMelee>().takeDamage(damaging);
            enemy.GetComponent<StunEnemy>().Stun();
        }
        
        yield return null; 
    }
}



}
