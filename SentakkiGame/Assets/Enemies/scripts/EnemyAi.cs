using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private enemyStats stats;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject AttackSensor;
    [SerializeField] private Animator enemyanim; 
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Transform attackPoint;
    //public float attackRange = 0.2f;
    [SerializeField] private float sizex;
    [SerializeField] private float sizey;
    private float angle;
    //private bool isFaceRight = false;
    [SerializeField] private float currentHealth;
    [SerializeField] private EnemyPath movement;
    private Vector2 lastPos;
    private Vector2 curPos;

    private bool hit;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = stats.maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        //lastPos.x = curPos.x;
        //curPos.x = transform.position.x;


        if (enemyanim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && enemyanim.GetCurrentAnimatorStateInfo(0).IsTag("death"))
        {
            Destroy(gameObject);
        }

        if ((rb.velocity.x == 10 || rb.velocity.x == -10) && !hit)
        {
            enemyAttack();
        }
    }

    void FixedUpdate()
    {
        if (lastPos.x < curPos.x)
        {
            angle = 180.0f;
            //isFaceRight = true;

        }
        else if (lastPos.x > curPos.x)
        {
            angle = 0f;
            //isFaceRight = false;
        }
    }

    void enemyAttack()
    {
        // play attack animation
        // Detect enemy(player) in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.position, new Vector2(sizex, sizey), angle, stats.playerLayers);
        //Debug.Log(hitEnemies[0]);
        //Damage the enemy(player)
        if(hitEnemies.Length > 0)
        {
            hit = true;
            foreach (Collider2D enemy in hitEnemies)
            {
                healthPoint.Instance.TakeDamage(stats.atk);
                Debug.Log("Player hit!!!" + enemy.name);
            }
        }
    }

    public void takeDamage(float damage)
    {
        // Detect attack from enemy(player)
        // play knock back animation
        // knockback
        // take damage
        enemyanim.Play("EnemyKnockBack", 0, 0);

        if (transform.localScale.x >= 1)
        {
            Debug.Log("R");
            rb.AddForce(new Vector2(-8, -4), ForceMode2D.Impulse);
        }
        else
        {
            Debug.Log("L");
            rb.AddForce(new Vector2(8, 4), ForceMode2D.Impulse);
        }
        StopAllCoroutines();
        Invoke("hitreset", 1.1f);

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            death();
        }
    }

    void death()
    {
        gameObject.layer = LayerMask.NameToLayer("ghostenemy");
        enemyanim.Play("EnemyDeath", 0, 0);
        movement.enabled = false;
        AttackSensor.SetActive(false);
        stopmoving();
        Spawn.instance.enemycounter -= 1;
    }

    public void hitreset()
    {
        movement.enabled = true;
        AttackSensor.SetActive(true);
        resetmove();
    }

    public void stopmoving()
    {
        rb.velocity = Vector2.zero;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            if (transform.localScale.x >= 1)
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
        hit = false;
        enemyanim.Play("EnemyAttack", 0, 0);
        stopmoving();
        movement.enabled = false;
        AttackSensor.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        rb.AddForce(new Vector2(-2 * stats.chargeSpd, 0), ForceMode2D.Impulse);
        //enemyAttack();
        yield return new WaitForSeconds(1.1f);
        stopmoving();
        movement.enabled = true;
        AttackSensor.SetActive(true);
    }

    IEnumerator chargeAtkRight()
    {
        hit = false;
        enemyanim.Play("EnemyAttack", 0, 0);
        stopmoving();
        movement.enabled = false;
        AttackSensor.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        rb.AddForce(new Vector2(2 * stats.chargeSpd, 0), ForceMode2D.Impulse);
        //enemyAttack();
        yield return new WaitForSeconds(1.1f);
        stopmoving();
        movement.enabled = true;
        AttackSensor.SetActive(true);
    }

    public void finishattack()
    {
        enemyanim.Play("EnemyAtkRec", 0, 0);
    }

    public void resetmove()
    {
        enemyanim.Play("EnemyWalk", 0, 0);
    }
}
