using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiMelee : MonoBehaviour
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
    public bool ded;
    private bool hit;

    // Start is called before the first frame update
    void Start()
    {
        atksfx = GameObject.FindGameObjectWithTag("sfxPlayerAndEnemy").GetComponent<AudioSource>();
        currentHealth = stats.maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        //lastPos.x = curPos.x;
        //curPos.x = transform.position.x;


        if (enemyanim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && enemyanim.GetCurrentAnimatorStateInfo(0).IsTag("death"))
        {
            ProgressBar.instance.UpdateProgressBar();
            if (!PlayerPrefs.HasKey("enemiesKilled"))
            {
                PlayerPrefs.SetInt("enemiesKilled", 1);
            }
            else
            {
                PlayerPrefs.SetInt("enemiesKilled", PlayerPrefs.GetInt("enemiesKilled") + 1);
            }
            PlayerPrefs.Save();
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
        if (currentHealth <= 0 )
        {
            ded = true;
            CancelInvoke("startatk");
            CancelInvoke("resetCounter");
            stopmoving();
            gameObject.layer = LayerMask.NameToLayer("ghostenemy");
            enemyanim.Play("EnemyDeath", 0, 0);
            movement.enabled = false;
            AttackSensor.SetActive(false);
        }
        else
        {
            CancelInvoke("startatk");
            movement.enabled = false;
            counter ++;
            stopmoving();
            if(counter < 3)
            {
                enemyanim.Play("EnemyNoKnockback", 0, 0);
            }
            else
            {
                enemyanim.Play("EnemyKnockBack", 0, 0);
            }
        }

    }

    private void hitknockback()
    {
        gameObject.layer = LayerMask.NameToLayer("ghostenemy");
        counter = 0;
            if (transform.localScale.x > 1)
            {
                rb.AddForce(new Vector2(-stats.XknockbackForce, stats.YknockbackForce), ForceMode2D.Impulse);
            }
            else if (transform.localScale.x < 1)
            {
                rb.AddForce(new Vector2(stats.XknockbackForce, stats.YknockbackForce), ForceMode2D.Impulse);
            }

        //yield return new WaitForSecondsRealtime(0.4f);
        //gameObject.layer = LayerMask.NameToLayer("enemy");
        ////stopmoving();
        //movement.enabled = true;
        //resetmove();
        //yield return new WaitForSeconds(stats.atkcooldown / 2);
        //AttackSensor.SetActive(true);
    }

    public void stopmoving()
    {
        rb.velocity = Vector2.zero;
    }
    
    private void resetCounter()
    {
        counter = 0;
    }

    private void resetcolor()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1,1);

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
        hit = false;
        CancelInvoke("startatk");
        gameObject.layer = LayerMask.NameToLayer("enemy");
        enemyanim.Play("EnemyAttack", 0, 0);
        stopmoving();
        movement.enabled = false;
        AttackSensor.SetActive(false);
        //yield return new WaitForSeconds(1.5f);
        //enemyAttack();
        //yield return new WaitForSeconds(1.1f);
        //stopmoving();
        //movement.enabled = true;
        yield return new WaitForSeconds(Random.Range(stats.minatkcooldown, stats.maxatkcooldown));
        AttackSensor.SetActive(true);
    }

    IEnumerator swingAtkRight()
    {
        hit = false;
        CancelInvoke("startatk");
        gameObject.layer = LayerMask.NameToLayer("enemy");
        enemyanim.Play("EnemyAttack", 0, 0);
        stopmoving();
        movement.enabled = false;
        AttackSensor.SetActive(false);
        //yield return new WaitForSeconds(1.5f);
        //enemyAttack();
        //yield return new WaitForSeconds(1.1f);
        //stopmoving();
        yield return new WaitForSeconds(Random.Range(stats.minatkcooldown, stats.maxatkcooldown));
        AttackSensor.SetActive(true);
    }


    public void resetmove()
    {
        stopmoving();
        movement.enabled = true;
        enemyanim.Play("EnemyWalk", 0, 0);
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
        if (!GetComponent<StunEnemy>().stunned)
        {
            movement.enabled = true;
            gameObject.layer = LayerMask.NameToLayer("enemy");
            resetmove();
            Invoke("startatk", Random.Range(0.3f, 0.4f));
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("enemy");
            enemyanim.Play("EnemyStun", 0, 0);
        }
    }
    private void disablemove()
    {
        movement.enabled = false;
    }
    private void moveforward()
    {
        if (transform.localScale.x > 0)
        {
            LeanTween.moveLocalX(this.gameObject, transform.position.x - 0.4f, 0.1f).setEaseOutExpo();
        }
        else
        {
            LeanTween.moveLocalX(this.gameObject, transform.position.x + 0.4f, 0.1f).setEaseOutExpo();

        }
    }

    private void disablecollider()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void enablecollider()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
