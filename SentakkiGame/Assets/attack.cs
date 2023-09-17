using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    private bool canAttack;
    private bool plungeAttack;

    public GameObject enemy;
    public Rigidbody2D rb;
    [SerializeField] private Transform enemyCheck;
    [SerializeField] private LayerMask enemyLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.J)) 
        {
            if(rb.velocity.y < 0f || rb.velocity.y > 8f)
            {
                PlungeAttack();
            }
            else
            {
                canAttack = Physics2D.OverlapBox(enemyCheck.position, new Vector2(1f, 0f), 0f, enemyLayer);
                Debug.Log("Press");
            }
        }

        if(canAttack)
        {
            Attack();
            canAttack = false;
        }

    }

    private void FixedUpdate()
    {
        Debug.Log(rb.velocity.y);
    }
    private void Attack()
    {
        Debug.Log("Hit");
    }

    private void PlungeAttack()
    {
        Debug.Log("Plunged");
        rb.AddForce(Vector2.down * 30f,ForceMode2D.Impulse);
    }
}
