using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class healthPoint : MonoBehaviour
{
    public Image healthBar;
    public float maxHealthAmount;

    public static healthPoint Instance;

    public float currenthealthAmountP1;
    public float currenthealthAmountP2;
    private bool hit;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private playerstats statsP1;
    [SerializeField] private playerstats statsP2;


    private void Awake()  
     {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


     void Start()
    {
        currenthealthAmountP1 = statsP1.maxhealth;
        currenthealthAmountP2 = statsP2.maxhealth;
        healthBar.fillAmount = currenthealthAmountP1 / maxHealthAmount;
    }

    private void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        if(!swapmechanic.instance.player1Active)
        {
            currenthealthAmountP2 -= damage;
        }
        else
        {
            currenthealthAmountP1 -= damage;
        }

        if(!hit)
        {
            hit = true;
            StartCoroutine(Knockback());
        }

        UpdateHealth();
    }

    public void RestoreHealthPoints(float amountToRestore)
{
    Debug.Log("Restoring health: " + amountToRestore);
        if(!swapmechanic.instance.player1Active)
        {
            currenthealthAmountP2 += amountToRestore;
            if (currenthealthAmountP1 > maxHealthAmount || currenthealthAmountP2 > maxHealthAmount)
            {
                currenthealthAmountP2 = maxHealthAmount;
            }
        }
        else
        {
            currenthealthAmountP1 += amountToRestore;
            if (currenthealthAmountP1 > maxHealthAmount || currenthealthAmountP2 > maxHealthAmount)
            {
                currenthealthAmountP1 = maxHealthAmount;
            }
        }
        UpdateHealth();

    }
    public void UpdateHealth() // change P1 health
    {
        if(!swapmechanic.instance.player1Active)
        {
            healthBar.fillAmount = currenthealthAmountP2 / maxHealthAmount;
        }
        else
        {
            healthBar.fillAmount = currenthealthAmountP1 / maxHealthAmount;
        }
    }

    private IEnumerator Knockback()
    {
        Debug.Log("knock");
        GetComponent<movement>().enabled = false;
        GetComponent<playerattack>().enabled = false;
        GetComponent<skillandultimate>().enabled = false;
        GetComponent<Animator>().Play("knockback", 0, 0);
        gameObject.layer = LayerMask.NameToLayer("ghostplayer");
        Time.timeScale = 1.0f;
        if (transform.localScale.x > 1)
        {
            rb.AddForce(new Vector2(-statsP1.XknockbackForce, statsP1.YknockbackForce) * 4,ForceMode2D.Impulse);
        }
        else if(transform.localScale.x < 1)
        {
            rb.AddForce(new Vector2(statsP1.XknockbackForce, statsP1.YknockbackForce) * 4, ForceMode2D.Impulse);
        }
        yield return new WaitForSecondsRealtime(1f);
        rb.velocity = Vector2.zero;
        GetComponent<movement>().enabled = true;
        GetComponent<playerattack>().enabled = true;
        GetComponent<skillandultimate>().enabled = true;
        GetComponent<Animator>().Play("idle", 0, 0);
        yield return new WaitForSecondsRealtime(1f);
        gameObject.layer = LayerMask.NameToLayer("player");
        hit = false;
    }
}