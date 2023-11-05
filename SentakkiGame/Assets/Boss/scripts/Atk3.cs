using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class Atk3 : MonoBehaviour
{
    [SerializeField] private BoxCollider2D col2d;
    [SerializeField] private GameObject atk3;
    [SerializeField] private GameObject atk4;
    [SerializeField] private float speed;
    [SerializeField] private float sizex3;
    [SerializeField] private float sizey3;
    [SerializeField] private Transform attackPoint3;
    private Vector2 spawnLocation3;
    private Vector2 force;
    private Vector2 force1;
    private float angle;
    private Rigidbody2D rb;
    private Rigidbody2D rb1;
    // Start is called before the first frame update
    void Start()
    {
        rb = atk3.GetComponent<Rigidbody2D>();
        rb1 = atk4.GetComponent<Rigidbody2D>();
        spawnLocation3 = attackPoint3.position;
        force = new Vector2(-3, 0);
        force1 = new Vector2(3, 0);
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
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint3.position, new Vector2(sizex3, sizey3), angle);
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
        if (attackPoint3 == null)
            return;
        Gizmos.DrawWireCube(attackPoint3.position, new Vector2(sizex3, sizey3));
    }

    private IEnumerator ShockAtk()
    {
        yield return new WaitForSecondsRealtime(3);
        bash();
        yield return new WaitForSecondsRealtime(0.2f);
        Instantiate(atk3, new Vector3(spawnLocation3.x - 0.2f, spawnLocation3.y, 0.0f), Quaternion.identity);
        Instantiate(atk4, new Vector3(spawnLocation3.x + 0.2f, spawnLocation3.y, 0.0f), Quaternion.identity);
        atk3.SetActive(true);
        atk4.SetActive(true);
        rb.AddForce(force * speed, ForceMode2D.Impulse);
        rb1.AddForce(force1 * speed, ForceMode2D.Impulse);
        yield return new WaitForSecondsRealtime(3);

    }
}
