using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class StunEnemy : MonoBehaviour
{
    public GameObject enemy;
    public bool stunned;


    private void Start()
    {

    }

    private void Update()
    {
        if (enemy.CompareTag("enemy"))
        {
            if(enemy.GetComponent<EnemyAi>().ded)
            {
                CancelInvoke("ReleaseStun");
            }
        }
        else if (enemy.CompareTag("enemyMelee"))
        {
            if (enemy.GetComponent<EnemyAiMelee>().ded)
            {
                CancelInvoke("ReleaseStun");
            }
        }
    }
    public void Stun()
    {
         Invoke("ReleaseStun", 4f);

         if (enemy.CompareTag("enemy"))
         {
            stunned = true;
            enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            enemy.GetComponent<EnemyPath>().enabled = false;
         }
         
          if (enemy.CompareTag("enemyMelee"))
         {
            stunned = true;
            enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            enemy.GetComponent<EnemyPath>().enabled = false;
            Debug.Log ("Stun");
         }
    }

    public void ReleaseStun()
    {
         enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
         if (enemy.CompareTag("enemy"))
         {
            stunned = false;

            enemy.GetComponent<Animator>().Play("EnemyWalk", 0, 0);
             enemy.GetComponent<EnemyPath>().enabled = true;
             enemy.GetComponent<EnemyAi>().startatk();
         }

          if (enemy.CompareTag("enemyMelee"))
         {
            stunned = false;
            enemy.GetComponent<Animator>().Play("EnemyWalk", 0, 0);
            enemy.GetComponent<EnemyPath>().enabled = true;
            enemy.GetComponent<EnemyAiMelee>().startatk();

            Debug.Log ("Unstunned");
         }
    }
}