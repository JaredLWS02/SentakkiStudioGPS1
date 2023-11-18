using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] private enemyStats stats;
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

    [SerializeField] private AudioSource atksfx;

    private float counter;


    private bool hit;
    public float chargepower;
    // Start is called before the first frame update
    void Start()
    {
        atksfx = GameObject.FindGameObjectWithTag("sfxPlayerAndEnemy").GetComponent<AudioSource>();
        chargepower = stats.chargeSpd;
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

        //if ((rb.velocity.x >= chargepower || rb.velocity.x <= -chargepower) && !hit)
        //{
        //    enemyAttack();
        //}
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
        if(!hit)
        {
            // play attack animation
            // Detect enemy(player) in range of attack
            Collider2D hitEnemies = Physics2D.OverlapBox(attackHitbox.position, new Vector2(sizex, sizey), angle, stats.playerLayers);
            //Debug.Log(hitEnemies[0]);
            //Damage the enemy(player)
            if (hitEnemies != null)
            {
                atksfx.Play();
                hit = true;
                healthPoint.Instance.TakeDamage(stats.dmg);
                    // Debug.Log("Player hit!!!" + enemy.name);
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
            CancelInvoke("startatk");
            CancelInvoke("resetCounter");
            movement.enabled = false;
            counter++;
            Invoke("resetCounter", 3);
            stopmoving();
            enemyanim.Play("EnemyKnockBack", 0, 0);
        }

    }

    private void resetCounter()
    {
        counter = 0;
    }
    private void hitknockback()
    {
        gameObject.layer = LayerMask.NameToLayer("ghostenemy");

        if(counter >= 4)
        {
            counter = 0;
            if (transform.localScale.x > 1)
            {
                rb.AddForce(new Vector2(-stats.XknockbackForce,stats.YknockbackForce), ForceMode2D.Impulse);
            }
            else if (transform.localScale.x < 1)
            {
                rb.AddForce(new Vector2(stats.XknockbackForce, stats.YknockbackForce), ForceMode2D.Impulse);
            }

        }
        //gameObject.layer = LayerMask.NameToLayer("enemy");
        //stopmoving();
        //movement.enabled = true;
        //resetmove();
        //yield return new WaitForSecondsRealtime(stats.atkcooldown /2);
        //AttackSensor.SetActive(true);
    }

    public void stopmoving()
    {
        rb.velocity = Vector2.zero;
    }


    void OnTriggerEnter2D (Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            if (transform.localScale.x >= 1)
            {
                StartCoroutine(chargeAtkRight());
               // Debug.Log("Moved right");
            }
            else
            {
                StartCoroutine(chargeAtkLeft());
                //Debug.Log("Moved left");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        //Gizmos.DrawWireCube(attackPoint.position, new Vector2(sizex,sizey));
        Gizmos.DrawWireCube(attackHitbox.position, new Vector2(sizex, sizey));
    }

    private IEnumerator chargeAtkLeft()
    {
        CancelInvoke("startatk");
        gameObject.layer = LayerMask.NameToLayer("enemy");
        hit = false;
        enemyanim.Play("EnemyAttack", 0, 0);
        stopmoving();
        movement.enabled = false;
        AttackSensor.SetActive(false);
        //yield return new WaitForSeconds(1.5f);
        ////AttackHtibox.SetActive(true);
        //rb.AddForce(new Vector2(-2 * chargepower, 0), ForceMode2D.Impulse);
        ////enemyAttack();
        //yield return new WaitForSeconds(1.1f);
        ////AttackHtibox.SetActive(false);
        //stopmoving();
        //movement.enabled = true;
        yield return new WaitForSecondsRealtime(stats.atkcooldown);
        AttackSensor.SetActive(true);
    }

    private IEnumerator chargeAtkRight()
    {
        CancelInvoke("startatk");
        gameObject.layer = LayerMask.NameToLayer("enemy");
        hit = false;
        enemyanim.Play("EnemyAttack", 0, 0);
        stopmoving();
        movement.enabled = false;
        AttackSensor.SetActive(false);
       // yield return new WaitForSeconds(1.5f);
        //AttackHtibox.SetActive(true);
        //rb.AddForce(new Vector2(2 * chargepower, 0), ForceMode2D.Impulse);
        //enemyAttack();
        //yield return new WaitForSeconds(1.1f);
        //AttackHtibox.SetActive(false);
        //stopmoving();
        //movement.enabled = true;
        yield return new WaitForSecondsRealtime(stats.atkcooldown);
        AttackSensor.SetActive(true);
    }


    public void resetmove()
    {
        stopmoving();
        movement.enabled = true;
        enemyanim.Play("EnemyWalk", 0, 0);
    }

    public void charge()
    {
        if (transform.localScale.x > 1)
        {
            rb.AddForce(new Vector2(2 * chargepower, 0), ForceMode2D.Impulse);

        }
        else if (transform.localScale.x < 1)
        {
            rb.AddForce(new Vector2(-2 * chargepower, 0), ForceMode2D.Impulse);
        }

    }

    public void startatk()
    {
        AttackSensor.SetActive(true);
    }

    public void stopatk()
    {
        AttackSensor.SetActive(false);
    }

    public void returnToOriState()
    {
        gameObject.layer = LayerMask.NameToLayer("enemy");
        resetmove();
        movement.enabled = true;
        Invoke("startatk", 0.1f);

    }
}
