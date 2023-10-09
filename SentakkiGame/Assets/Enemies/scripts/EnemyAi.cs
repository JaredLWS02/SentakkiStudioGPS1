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
    public float attackRange = 0.2f;
    public bool isFaceRight = false;
    public float maxHealth;
    float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void enemyAttack()
    {
        // play attack animation
        //animator.SetTrigger("Attack");
        // Detect enemy(player) in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        //Damage the enemy(player)
        foreach(Collider2D enemy in hitEnemies)
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
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            death();
        }
    }

    void death()
    {
        Debug.Log("Died");
        Destroy(gameObject);
        // play death animation
        // +point to progress bar
        // delete game object
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            StartCoroutine(chargeAtk());
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    IEnumerator chargeAtk()
    {
        yield return new WaitForSeconds(2);
        enemyAttack();
    }
}
