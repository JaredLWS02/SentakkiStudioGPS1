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
    public bool swaped;
    private bool hit;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private playerstats stats;


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


    private void Start()
    {
        swaped = false;
        currenthealthAmountP1 = maxHealthAmount;
        currenthealthAmountP2 = maxHealthAmount;
        UpdateHealth();
    }

    private void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        if(swaped)
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
        if(swaped)
        {
            currenthealthAmountP2 += amountToRestore;
            if (currenthealthAmountP1 > maxHealthAmount || currenthealthAmountP2 > maxHealthAmount)
            {
                currenthealthAmountP1 = maxHealthAmount;
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
        if(swaped)
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
        gameObject.layer = LayerMask.NameToLayer("ghostplayer");
        if (transform.localScale.x > 1)
        {
            rb.AddForce(new Vector2(-stats.XknockbackForce, 3) * 4,ForceMode2D.Impulse);
        }
        else if(transform.localScale.x < 1)
        {
            rb.AddForce(new Vector2(stats.XknockbackForce, 3) * 4, ForceMode2D.Impulse);
        }
        yield return new WaitForSecondsRealtime(1f);
        rb.velocity = Vector2.zero;
        GetComponent<movement>().enabled = true;
        GetComponent<playerattack>().enabled = true;
        GetComponent<skillandultimate>().enabled = true;
        yield return new WaitForSecondsRealtime(1f);
        gameObject.layer = LayerMask.NameToLayer("player");
        hit = false;
    }
}