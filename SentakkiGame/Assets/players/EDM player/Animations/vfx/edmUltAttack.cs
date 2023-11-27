using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class edmUltAttack : MonoBehaviour
{
    [SerializeField] private AudioSource edmUltSfx;
    [SerializeField] private Animator playeranim;
    [SerializeField] private Collider2D[] hitenemiesUlti;
    [SerializeField] private Transform EdmUltiattackpoint;
    [SerializeField] private combomanagerUI combomanagerUI;
    [SerializeField] private GameObject edmUltVfx;
    public playerstats stats;
    private float duration;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void edmUlt()
    {
        Invoke("returnOriginal", 3);
        if(!edmUltSfx.isPlaying)
        {
            edmUltSfx.Play();
        }
        hitenemiesUlti = Physics2D.OverlapBoxAll(EdmUltiattackpoint.position, stats.edmUltRange, 1f, stats.enemylayer);

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

        IEnumerator DamageOverTime(Collider2D enemy, float totalDamage, float duration)
        {
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                float damaging = totalDamage / duration * Time.deltaTime;

                if (enemy.CompareTag("enemy"))
                {
                    enemy.GetComponent<EnemyAi>().takeDamage(damaging);
                    //enemy.GetComponent<StunEnemy>().Stun();
                }
                else if (enemy.CompareTag("enemyMelee"))
                {
                    enemy.GetComponent<EnemyAiMelee>().takeDamage(damaging);
                    //enemy.GetComponent<StunEnemy>().Stun();
                }

                elapsedTime += Time.deltaTime;
                yield return null; // Wait until the next frame
            }
        }

        
    }
    private void returnOriginal()
    {
        edmUltVfx.SetActive(false);
        playeranim.Play("idle", 0, 0);
    }
}
