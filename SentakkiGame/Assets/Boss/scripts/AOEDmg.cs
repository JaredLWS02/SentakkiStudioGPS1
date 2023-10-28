using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEDmg : MonoBehaviour
{
    [SerializeField] private float sizex;
    [SerializeField] private float sizey;
    [SerializeField] private Transform attackPoint;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void touch()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(sizex, sizey), angle);
        if (hitEnemies.Length > 0)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                healthPoint.Instance.TakeDamage(5);
                Debug.Log("Player hit!!!" + enemy.name);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            touch();
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireCube(attackPoint.position, new Vector2(sizex, sizey));
    }
}
