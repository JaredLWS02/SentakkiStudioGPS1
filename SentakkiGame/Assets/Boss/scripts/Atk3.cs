using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Atk3 : MonoBehaviour
{
    [SerializeField] private BoxCollider2D col2d;
    [SerializeField] private GameObject atk3;
    [SerializeField] private GameObject atk4;
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private Rigidbody2D rb1;
    // Start is called before the first frame update
    void Start()
    {
        rb = atk3.GetComponent<Rigidbody2D>();
        rb1 = atk4.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col2d.enabled = false;
            if (col2d.enabled == false)
            {
                Debug.Log("Atk3");
                StartCoroutine(ShockAtk());
            }
            else
            {
                col2d.enabled = false;
            }
        }
    }

    void bash()
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

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireCube(attackPoint.position, new Vector2(sizex, sizey));
    }

    private IEnumerator ShockAtk()
    {
        atk3.SetActive(true);
        atk4.SetActive(true);
        yield return null;
    }
}
