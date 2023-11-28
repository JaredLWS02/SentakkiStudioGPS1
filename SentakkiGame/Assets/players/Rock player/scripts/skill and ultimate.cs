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
    [SerializeField] private Transform RockUltiattackpoint;
    [SerializeField] private Transform EdmUltiattackpoint;
    [SerializeField] private AudioSource skillAndUltisfx;
    [SerializeField] private AudioSource edmUltSfx;
    [SerializeField] private AudioSource ultiReadySfx;
    [SerializeField] private combomanagerUI combomanagerUI;
    [SerializeField] private float edmUltDuration;
    private ParticleSystem hitvfx;
    private bool failHit;
    public float sizeX;
    public float sizeY;
    public float damagePerSecond;

    [SerializeField] private GameObject edmobject;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject UICanvas;
    [SerializeField] private GameObject edmUltVfx;

    public bool enabledSkill;
    private float campos;

    // Start is called before the first frame update
    void Start()
    {
        damagePerSecond = stats.ultdmg;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enabledSkill)
        {
            return;
        }
        if (PauseMenu.instance.isPaused || animationskill.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            return;
        }

        if (Time.time - lastskillclickedtime >= stats.skillcooldown)
        {
            if (Input.GetKeyDown(KeyCode.K) && gaugePoint.gaugePointAmount > 32)
            {
                if (movement.instance.IsGrounded())
                {
                    gaugePoint.ReduceGauge(33);
                    lastskillclickedtime = Time.time;
                    animationskill.Play("skill", 0, 0);
                    //if (!swapmechanic.instance.player1Active)
                    //{
                    //    skillP2();
                    //}
                    //else
                    //{
                    //    skillP1();
                    //}
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.U) && gaugePoint.gaugePointAmount == 100)
        {
            if (movement.instance.IsGrounded())
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

        if (GaugePoint.Instance.gaugeBar.fillAmount >= 1 && !GaugePoint.Instance.ultiReady)
        {
            GaugePoint.Instance.ultiReady = true;
            ultiReadySfx.clip = stats.ultReadysfx;
            ultiReadySfx.Play();
        }
    }

    void skillP1()
    {
        if (!failHit)
        {
            skillAndUltisfx.clip = stats.skillsfx;
            skillAndUltisfx.Play();
            hitenemiesSkill = Physics2D.OverlapCircleAll(skillattackpoint.position, stats.skillrange, stats.enemylayer);
            //animationskill.runtimeAnimatorController = skillanim.animatorOV;
            //lastskillclickedtime = Time.time;

            if (hitenemiesSkill.Length <= 0)
            {
                failHit = true;
            }
            else
            {
                failHit = false;
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
        //if (Time.time - lastskillclickedtime >= stats.skillcooldown)
        //{
        //}
    }

    void skillP2()
    {
        //if (Time.time - lastskillclickedtime >= stats.skillcooldown)
        //{
        Instantiate(edmobject, transform.position, Quaternion.identity);
        skillAndUltisfx.clip = stats.skillsfx;
        skillAndUltisfx.Play();
        //hitenemiesSkill = Physics2D.OverlapCircleAll(skillattackpoint.position, stats.skillrange, stats.enemylayer);
        //gaugePoint.ReduceGauge(33);
        //animationskill.runtimeAnimatorController = skillanim.animatorOV;
        //animationskill.Play("skill", 0, 0);
        //lastskillclickedtime = Time.time;
        //}
    }

    void ultimateP1()
    {
        GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
        Time.timeScale = 0.5f;
        gaugePoint.ReduceGauge(100);
        panel.SetActive(true);
        UICanvas.SetActive(false);
        animationskill.Play("ulti", 0, 0);
    }


    void ultimateP2()
    {
        GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
        Time.timeScale = 0.5f;
        gaugePoint.ReduceGauge(100);
        panel.SetActive(true);
        UICanvas.SetActive(false);
        animationskill.Play("ult start", 0, 0);
        skillAndUltisfx.clip = stats.ultsfx;
        skillAndUltisfx.Play();
    }




    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(skillattackpoint.position, stats.skillrange);
        Gizmos.DrawWireSphere(RockUltiattackpoint.position, stats.Rockultrange);
        Gizmos.DrawWireCube(new Vector2(EdmUltiattackpoint.position.x, EdmUltiattackpoint.position.y), stats.edmUltRange);
    }
    private void ExitSkill()
    {
        failHit = false;
        animationskill.Play("idle", 0, 0);
    }

    private void Startfreezeframe()
    {
        if (!failHit)
        {
            Time.timeScale = 0.0f;
            StartCoroutine(Endfreezeframe());
            //atkanim.SetFloat("slow",0.6f);
            //atkanim.speed = freezeframeduration;
        }
    }
    private IEnumerator Endfreezeframe()
    {
        yield return new WaitForSecondsRealtime(0.13f);
        Time.timeScale = 1f;
    }

    private void edmUlt()
    {
        GetComponent<Animator>().updateMode = AnimatorUpdateMode.Normal;
        UICanvas.SetActive(true);
        Time.timeScale = 1f;
        edmUltSfx.Play();
        hitenemiesUlti = Physics2D.OverlapBoxAll(EdmUltiattackpoint.position, stats.edmUltRange, 1f, stats.enemylayer);
        if (hitenemiesUlti.Length <= 0)
        {
            failHit = true;
        }
        else
        {
            failHit = false;
        }


        foreach (Collider2D enemy in hitenemiesUlti)
        {
            if (enemy.CompareTag("enemy"))
            {
                StartCoroutine(DamageOverTime(enemy, stats.ultdmg, edmUltDuration));
                combomanagerUI.innercomboUI++;
                combomanagerUI.checkcombostatus();

            }

            if (enemy.CompareTag("enemyMelee"))
            {
                StartCoroutine(DamageOverTime(enemy, stats.ultdmg, edmUltDuration));
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
                    hitvfx = enemy.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();
                    hitvfx.Play();
                    enemy.GetComponent<EnemyAi>().takeDamage(damaging);
                }
                else if (enemy.CompareTag("enemyMelee"))
                {
                    hitvfx = enemy.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();
                    hitvfx.Play();
                    enemy.GetComponent<EnemyAiMelee>().takeDamage(damaging);
                }

                elapsedTime += Time.deltaTime;
                yield return null; // Wait until the next frame
            }
        }
        Invoke("returnOri", edmUltDuration);
    }
    private void rockUlt()
    {
        GetComponent<Animator>().updateMode = AnimatorUpdateMode.Normal;
        UICanvas.SetActive(true);
        Time.timeScale = 1f;
        hitenemiesUlti = Physics2D.OverlapCircleAll(RockUltiattackpoint.position, stats.Rockultrange, stats.enemylayer);
        skillAndUltisfx.clip = stats.ultsfx;
        skillAndUltisfx.Play();

        if (hitenemiesUlti.Length <= 0)
        {
            failHit = true;
        }
        else
        {
            failHit = false;
        }

        foreach (Collider2D enemy in hitenemiesUlti)
        {
            if (enemy.CompareTag("enemy"))
            {
                {
                    hitvfx = enemy.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();
                    hitvfx.Play();
                    enemy.GetComponent<EnemyAi>().takeDamage(stats.ultdmg);
                    enemy.GetComponent<StunEnemy>().Stun();
                }

            }

            if (enemy.CompareTag("enemyMelee"))
            {
                {
                    hitvfx = enemy.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();
                    hitvfx.Play();
                    enemy.GetComponent<EnemyAiMelee>().takeDamage(stats.ultdmg);
                    enemy.GetComponent<StunEnemy>().Stun();
                }
            }
            combomanagerUI.innercomboUI++;
            combomanagerUI.checkcombostatus();
        }
    }

    private void enableIframe()
    {
        gameObject.layer = LayerMask.NameToLayer("ghostplayer");
        GetComponent<swapmechanic>().enabled = false;
    }

    private void disableIframe()
    {
        gameObject.layer = LayerMask.NameToLayer("player");
        GetComponent<swapmechanic>().enabled = true;
    }

    private void shakecamera()
    {
        panel.SetActive(false);
        if(transform.localScale.x > 0)
        {
            LeanTween.moveLocalX(Camera.main.gameObject, 0.3f, 0.02f).setIgnoreTimeScale(true).setOnComplete(shakeback);
        }
        else
        {
            LeanTween.moveLocalX(Camera.main.gameObject, -0.3f, 0.02f).setIgnoreTimeScale(true).setOnComplete(shakeback);
        }
    }

    private void shakeback()
    {
        if(transform.localScale.x > 0)
        {
            LeanTween.moveLocalX(Camera.main.gameObject, -0.3f, 0.02f).setIgnoreTimeScale(true);
        }
        else
        {
            LeanTween.moveLocalX(Camera.main.gameObject, 0.3f, 0.02f).setIgnoreTimeScale(true);
        }
        LeanTween.moveLocalX(Camera.main.gameObject, 0, 0.02f).setIgnoreTimeScale(true).setDelay(0.02f);
    }


    private void enableEdmVfx()
    {
        edmUltVfx.SetActive(true);
    }

    private void returnOri()
    {
        edmUltVfx.SetActive(false);
        animationskill.Play("ult end", 0, 0);
    }
}
