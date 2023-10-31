using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class bossAtk : MonoBehaviour
{
    //[SerializeField] private enemyStats stats;
    [SerializeField] private GameObject target;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float sizex;
    [SerializeField] private float sizey;
    private float angle;
    private Rigidbody2D rb;
    [SerializeField] private Animator bossAnim;
    [SerializeField] private float curHp;
    [SerializeField] private GameObject enemy;

    // Start is called before the first frame update
    void Awake()
    {
       // curHp = stats.hp;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //void bossAtk2()
    //{
    //    Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(sizex, sizey), angle, stats.playerLayers);
    //    if (hitEnemies.Length > 0)
    //    {
    //        hit = true;
    //        foreach (Collider2D enemy in hitEnemies)
    //        {
    //            healthPoint.Instance.TakeDamage(stats.atk);
    //            Debug.Log("Player hit!!!" + enemy.name);
    //        }
    //    }
    //}

    //void bossAtk3()
    //{
    //    Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(sizex, sizey), angle, stats.playerLayers);
    //    if (hitEnemies.Length > 0)
    //    {
    //        hit = true;
    //        foreach (Collider2D enemy in hitEnemies)
    //        {
    //            healthPoint.Instance.TakeDamage(stats.atk);
    //            Debug.Log("Player hit!!!" + enemy.name);
    //        }
    //    }
    //}

    //void bossAtk4()
    //{
    //    Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(sizex, sizey), angle, stats.playerLayers);
    //    if (hitEnemies.Length > 0)
    //    {
    //        hit = true;
    //        foreach (Collider2D enemy in hitEnemies)
    //        {
    //            healthPoint.Instance.TakeDamage(stats.atk);
    //            Debug.Log("Player hit!!!" + enemy.name);
    //        }
    //    }
    //}

    //void bossAtk5()
    //{
    //    Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(sizex, sizey), angle, stats.playerLayers);
    //    if (hitEnemies.Length > 0)
    //    {
    //        hit = true;
    //        foreach (Collider2D enemy in hitEnemies)
    //        {
    //            healthPoint.Instance.TakeDamage(stats.atk);
    //            Debug.Log("Player hit!!!" + enemy.name);
    //        }
    //    }
    //}

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireCube(attackPoint.position, new Vector2(sizex, sizey));
    }
}
