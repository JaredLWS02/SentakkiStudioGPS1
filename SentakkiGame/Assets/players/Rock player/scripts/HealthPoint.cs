using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthPoint : MonoBehaviour
{
    public Image healthBar;
    public float maxHealthAmount = 100f;

    public static healthPoint Instance;

    public float healthAmount;

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
        healthAmount = 80f;
        UpdateHealth();
    }

    private void Update()
    {

    }

    public void TakeDamage(float damage)
    {


    }

public void RestoreHealthPoints(float amountToRestore)
{
    Debug.Log("Restoring health: " + amountToRestore);
    healthAmount += amountToRestore;
    if (healthAmount > maxHealthAmount)
    {
        healthAmount = maxHealthAmount;
    }
    UpdateHealth();
}
    private void UpdateHealth()
    {
        healthBar.fillAmount = healthAmount / maxHealthAmount;
    }
}