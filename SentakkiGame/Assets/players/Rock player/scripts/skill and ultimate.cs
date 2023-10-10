using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class skillandultimate : MonoBehaviour
{
    public GaugePoint gaugePoint;
    private bool reseted;
    public Animator animationskill;
    public float skillcooldown;
    private float lastskillclickedtime;
    public float skilldmg;
    [SerializeField] private attackscirptableobject skillanim;
    [SerializeField] private Transform skillattackpoint;
    [SerializeField] private float skillattackrange;
    [SerializeField] private Collider2D[] hitenemiesSkill;
    [SerializeField] private LayerMask enemylayer;

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
    }

    void skill()
    {
        if (Time.time - lastskillclickedtime >= skillcooldown)
        {
            hitenemiesSkill = Physics2D.OverlapCircleAll(skillattackpoint.position, skillattackrange, enemylayer);
            gaugePoint.TakeDamage(33);
            animationskill.runtimeAnimatorController = skillanim.animatorOV;
            animationskill.Play("skill", 0, 0);
            lastskillclickedtime = Time.time;

            foreach (Collider2D enemy in hitenemiesSkill)
            {
                enemy.GetComponent<EnemyAi>().takeDamage(skilldmg);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(skillattackpoint.position, skillattackrange);
    }
    void ExitSkill()
    {
       animationskill.SetTrigger("skill");
    }
}
