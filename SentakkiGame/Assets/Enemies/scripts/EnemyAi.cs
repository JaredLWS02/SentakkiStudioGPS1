using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public enemyStats stats;
    public GameObject target;
    public GameObject AttackSensor;
    public Animator animator; 
    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    public LayerMask playerLayers;
    public Transform attackPoint;
    //public float attackRange = 0.2f;
    public float sizex = 4.0f;
    public float sizey = 2.0f;
    public float angle;
    public bool isFaceRight = false;
    public float maxHealth;
    float currentHealth;
    public EnemyPath movement;
    public float chargeSpd = 5;
    private Vector2 lastPos;
    private Vector2 curPos;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        lastPos.x = curPos.x;
        curPos.x = transform.position.x;
    }

    void FixedUpdate()
    {
        if (lastPos.x < curPos.x)
        {
            angle = 180.0f;
            isFaceRight = true;

        }
        else if (lastPos.x > curPos.x)
        {
            angle = 0f;
            isFaceRight = false;

        }
    }

    void enemyAttack()
    {
        // play attack animation
        //animator.SetTrigger("Attack");
        // Detect enemy(player) in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(sizex,sizey), angle, playerLayers);
        //Damage the enemy(player)
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Player hit!!!" + enemy.name);
        }
    }

    public void takeDamage(float damage)
    {
        // Detect attack from enemy(player)
        // play knock back animation
        // knockback
        // take damage
        animator.SetTrigger("takedmg");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            death();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            animator.SetBool("Attacking", true);
            if (isFaceRight)
            {
                StartCoroutine(chargeAtkRight());
                Debug.Log("Moved right");
            }
            else
            {
                StartCoroutine(chargeAtkLeft());
                Debug.Log("Moved left");
            }

        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireCube(attackPoint.position, new Vector2(sizex,sizey));
    }

    IEnumerator chargeAtkLeft()
    {
        movement.enabled = false;
        AttackSensor.SetActive(false);
        yield return new WaitForSeconds(2);
        rb.AddForce(new Vector2(-2 * chargeSpd, 0), ForceMode2D.Impulse);
        enemyAttack();
        yield return new WaitForSeconds(2);
        movement.enabled = true;
        AttackSensor.SetActive(true);
        animator.SetBool("Attacking", false);
        animator.SetBool("FinishedAttack", true);
        yield return new WaitForSeconds(1);
        animator.SetBool("FinishedAttack", false);
    }

    void death()
    {
        Debug.Log("Died");
        animator.SetBool("Attacking", false);
        animator.SetBool("FinishedAttack", false);
        animator.SetBool("isMoving", false);
        animator.SetTrigger("died");
        movement.enabled = false;
        AttackSensor.SetActive(false);
        Destroy(gameObject);
        // play death animation
        // +point to progress bar
        // delete game object
    }

    IEnumerator chargeAtkRight()
    {
        movement.enabled = false;
        AttackSensor.SetActive(false);
        yield return new WaitForSeconds(2);
        rb.AddForce(new Vector2(2 * chargeSpd, 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(2);
        enemyAttack();
        movement.enabled = true;
        AttackSensor.SetActive(true);
        animator.SetBool("Attacking", false);
        animator.SetBool("FinishedAttack", true);
        yield return new WaitForSeconds(1);
        animator.SetBool("FinishedAttack", false);
    }
}
