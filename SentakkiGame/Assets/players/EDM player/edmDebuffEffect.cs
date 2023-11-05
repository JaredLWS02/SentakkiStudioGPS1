using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class edmDebuffEffect : MonoBehaviour
{
    [SerializeField] private enemyStats enemyStats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("enemyMelee") || coll.CompareTag("enemy"))
        {
            coll.GetComponent<EnemyPath>().speed = 1;
        }

        if (coll.CompareTag("enemy"))
        {
            coll.GetComponent<EnemyAi>().chargepower = enemyStats.chargeSpd /2;
        }

    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("enemy") || coll.CompareTag("enemyMelee"))
        {
            coll.GetComponent<EnemyPath>().speed = 2;
        }

        if (coll.CompareTag("enemy"))
        {
            coll.GetComponent<EnemyAi>().chargepower = enemyStats.chargeSpd;
        }
    }
}
