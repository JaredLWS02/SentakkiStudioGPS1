using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        if (PlayerPrefs.HasKey("hpP1") && PlayerPrefs.HasKey("hpP2"))
        {
            currenthealthAmountP1 = PlayerPrefs.GetFloat("hpP1");
            currenthealthAmountP2 = PlayerPrefs.GetFloat("hpP2");
        }
        else
        {
            currenthealthAmountP1 = statsP1.maxhealth;
            currenthealthAmountP2 = statsP2.maxhealth;
        }
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

        UpdateHealth();
        if(!hit)
        {
            hit = true;
            Knockback();
        }

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

        if (healthBar.fillAmount <= 0 )
        {
            GetComponent<Animator>().Play("death", 0, 0);
        }
    }

    private void Knockback()
    {
        if(healthBar.fillAmount > 0)
        {
            CancelInvoke("returnOriState");
            Debug.Log("knock");
            GetComponent<movement>().enabled = false;
            GetComponent<playerattack>().enabled = false;
            GetComponent<playerattack>().isPlunging = false;
            GetComponent<skillandultimate>().enabled = false;
            GetComponent<swapmechanic>().enabled = false;
            GetComponent<Animator>().Play("knockback", 0, 0);
            gameObject.layer = LayerMask.NameToLayer("ghostplayer");
            Time.timeScale = 1.0f;
            if (transform.localScale.x > 1)
            {
                rb.AddForce(new Vector2(-statsP1.XknockbackForce, statsP1.YknockbackForce) * 4, ForceMode2D.Impulse);
            }
            else if (transform.localScale.x < 1)
            {
                rb.AddForce(new Vector2(statsP1.XknockbackForce, statsP1.YknockbackForce) * 4, ForceMode2D.Impulse);
            }
        }
    }

    private void ded()
    {
        hit = false;
        StopAllCoroutines();
        gameObject.layer = LayerMask.NameToLayer("ghostplayer");
        GetComponent<movement>().enabled = false;
        GetComponent<playerattack>().enabled = false;
        GetComponent<skillandultimate>().enabled = false;
        GetComponent<swapmechanic>().enabled = false;
    }

    private void switchDeathScene()
    { 
        if(currenthealthAmountP1 <= 0 && currenthealthAmountP2 <= 0)
        {
            SceneManager.LoadSceneAsync(2);
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("player");
            swapmechanic.instance.SwitchPlayer();
            GetComponent<movement>().enabled = true;
            GetComponent<playerattack>().enabled = true;
            GetComponent<skillandultimate>().enabled = true;
        }
    }

    private void returnOriState()
    {
        rb.velocity = Vector2.zero;
        GetComponent<movement>().enabled = true;
        GetComponent<playerattack>().enabled = true;
        GetComponent<skillandultimate>().enabled = true;
        GetComponent<swapmechanic>().enabled = true;
        GetComponent<Animator>().Play("idle", 0, 0);
        Invoke("removeiframe", 2);
    }

    private void removeiframe()
    {
        gameObject.layer = LayerMask.NameToLayer("player");
        hit = false;
    }
}