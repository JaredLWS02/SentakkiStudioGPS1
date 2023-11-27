using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class StunEnemy : MonoBehaviour
{
    public GameObject enemy;


    private void Start()
    {

    }

    public void Stun()
    {
        {
            if (enemy.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
            {
                enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                Invoke("ReleaseStun", 3f);
            }

            if (enemy.CompareTag("enemy"))
            {
                enemy.GetComponent<EnemyPath>().enabled = false;
                enemy.GetComponent<EnemyAi>().enabled = false;  
            }
            
             if (enemy.CompareTag("enemyMelee"))
            {
                enemy.GetComponent<EnemyPath>().enabled = false;
                enemy.GetComponent<EnemyAiMelee>().enabled = false;
                Debug.Log ("Stun");
            }

        }
    }

    private void ReleaseStun()
    {
        {
            enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

            if (enemy.CompareTag("enemy"))
            {
                enemy.GetComponent<EnemyPath>().enabled = true;
                enemy.GetComponent<EnemyPath>().enabled = true;
            }

             if (enemy.CompareTag("enemyMelee"))
            {
                enemy.GetComponent<EnemyPath>().enabled = true;
                enemy.GetComponent<EnemyAiMelee>().enabled = true;
                Debug.Log ("Unstunned");
            }
    }
}
