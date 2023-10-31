using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Timeline;
using UnityEngine;

public class emdDebuffer : MonoBehaviour
{
    public float size;
    public GameObject player;
    public Collider2D [] hitenemy;
    [SerializeField] private playerstats stats;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player.transform.localScale.x > 1)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(6, 3), ForceMode2D.Impulse);
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-6, 3), ForceMode2D.Impulse);
        }
        Invoke("kill", 10f);
    }


    //private void slowdown()
    //{
    //    hitenemy = Physics2D.OverlapCircleAll(transform.position, size,stats.enemylayer);

    //    foreach (Collider2D coll in hitenemy)
    //    {
    //        coll.GetComponent<EnemyPath>().speed = 1;
    //    }

    //}

    private void kill()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("enemy"))
        {
            coll.GetComponent<EnemyPath>().speed = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.CompareTag("enemy"))
        {
            coll.GetComponent<EnemyPath>().speed = 2;   
        }
    }
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawWireSphere(transform.position, size);

    //}
}
