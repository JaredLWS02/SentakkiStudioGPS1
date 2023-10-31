using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class skillandultimate : MonoBehaviour
{
    private float lastskillclickedtime;
    private Collider2D[] hitenemiesSkill;

    [SerializeField] private attackscirptableobject skillanim;
    [SerializeField] private playerstats stats;

    [SerializeField] private GaugePoint gaugePoint;
    [SerializeField] private Animator animationskill;
    [SerializeField] private Transform skillattackpoint;
    [SerializeField] private AudioSource skillAndUltisfx;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && gaugePoint.gaugePointAmount > 32)
        {
            if (movement.instance.IsGrounded() && !movement.instance.isDashing)
            {
                skill();
            }
        }

        if (Input.GetKeyDown(KeyCode.K) && gaugePoint.gaugePointAmount == 100)
        {
            if (movement.instance.IsGrounded() && !movement.instance.isDashing)
            {
                ultimate();
            }
        }
    }

    void skill()
    {
        if (Time.time - lastskillclickedtime >= stats.skillcooldown)
        {
            skillAndUltisfx.clip = stats.skillsfx;
            skillAndUltisfx.Play();
            hitenemiesSkill = Physics2D.OverlapCircleAll(skillattackpoint.position, stats.skillrange, stats.enemylayer);
            gaugePoint.TakeDamage(33);
            animationskill.runtimeAnimatorController = skillanim.animatorOV;
            animationskill.Play("skill", 0, 0);
            lastskillclickedtime = Time.time;

            foreach (Collider2D enemy in hitenemiesSkill)
            {
                enemy.GetComponent<EnemyAi>().takeDamage(stats.skilldmg);
            }
        }
    }

    void ultimate()
    {
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(skillattackpoint.position, stats.skillrange);
    }
    void ExitSkill()
    {
        animationskill.Play("idle", 0, 0);
    }
}
