using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAiMelee : MonoBehaviour
{
    [SerializeField] private enemyStats stats;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject AttackSensor;
    [SerializeField] private GameObject AttackHtibox;
    [SerializeField] private Animator enemyanim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform attackHitbox;
    //public float attackRange = 0.2f;
    [SerializeField] private float sizex;
    [SerializeField] private float sizey;
    private float angle;
    //private bool isFaceRight = false;
    [SerializeField] private float currentHealth;
    [SerializeField] private EnemyPath movement;
    private Vector2 lastPos;
    private Vector2 curPos;
    [SerializeField] private AudioSource hitsfx;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = stats.maxhp;
        hitsfx = GameObject.FindGameObjectWithTag("hitsfx").GetComponent<AudioSource>();
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
        
    }

/*    void FixedUpdate()
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

    }*/

    void enemyAttack()
    {
        // play attack animation
        // Detect enemy(player) in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackHitbox.position, new Vector2(sizex, sizey), angle, stats.playerLayers);
        //Debug.Log(hitEnemies[0]);
        //Damage the enemy(player)
        if (hitEnemies.Length > 0)
        {
            hitsfx.Play();
            foreach (Collider2D enemy in hitEnemies)
            {
                healthPoint.Instance.TakeDamage(stats.dmg);
                //Debug.Log("Player hit!!!" + enemy.name);
            }
        }
    }

    public void takeDamage(float damage)
    {
        // Detect attack from enemy(player)
        // play knock back animation
        // knockback
        // take damage
        StopAllCoroutines();
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            stopmoving();
            gameObject.layer = LayerMask.NameToLayer("ghostenemy");
            enemyanim.Play("EnemyDeath", 0, 0);
            movement.enabled = false;
            AttackSensor.SetActive(false);
            Spawn.instance.enemycounter -= 1;
            ProgressBar.instance.UpdateProgressBar();
        }
        else
        {
            enemyanim.Play("EnemyKnockBack", 0, 0);
            StartCoroutine(hitknockback());
            //Invoke("hitreset", 1.2f);
        }

    }

    private IEnumerator hitknockback()
    {
        if (transform.localScale.x > 1)
        {
            rb.AddForce(new Vector2(-stats.XknockbackForce, stats.YknockbackForce) ,ForceMode2D.Impulse);
        }
        else if (transform.localScale.x < 1)
        {
            rb.AddForce(new Vector2(stats.XknockbackForce, stats.YknockbackForce), ForceMode2D.Impulse);
        }

        gameObject.layer = LayerMask.NameToLayer("ghostenemy");
        yield return new WaitForSecondsRealtime(0.4f);
        gameObject.layer = LayerMask.NameToLayer("enemy");
        //stopmoving();
        movement.enabled = true;
        resetmove();
        AttackSensor.SetActive(true);
    }
    public void stopmoving()
    {
        rb.velocity = Vector2.zero;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (transform.localScale.x >= 1)
            {
                StartCoroutine(swingAtkRight());
                //Debug.Log("Moved right");
            }
            else
            {
                StartCoroutine(swingAtkLeft());
                //Debug.Log("Moved left");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireCube(attackHitbox.position, new Vector2(sizex, sizey));
    }

    IEnumerator swingAtkLeft()
    {
        enemyanim.Play("EnemyAttack", 0, 0);
        stopmoving();
        movement.enabled = false;
        AttackSensor.SetActive(false);
        yield return new WaitForSeconds(stats.chargetime);
        enemyAttack();
        yield return new WaitForSeconds(1.1f);
        stopmoving();
        movement.enabled = true;
        yield return new WaitForSeconds(stats.atkcooldown);
        AttackSensor.SetActive(true);
    }

    IEnumerator swingAtkRight()
    {
        enemyanim.Play("EnemyAttack", 0, 0);
        stopmoving();
        movement.enabled = false;
        AttackSensor.SetActive(false);
        yield return new WaitForSeconds(stats.chargetime);
        enemyAttack();
        yield return new WaitForSeconds(1.1f);
        stopmoving();
        movement.enabled = true;
        yield return new WaitForSeconds(stats.atkcooldown);
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
