using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugePoint : MonoBehaviour
{
    public Image gaugeBar;
    public float maxGaugePointAmount = 100f;

    public static GaugePoint Instance { get; private set; }

    public float gaugePointAmount { get; private set; }

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
        gaugePointAmount = maxGaugePointAmount;
        UpdateGauge();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && gaugePointAmount > 32)
        {
            TakeDamage(33);
        }
        if (Input.GetKeyDown(KeyCode.K) && gaugePointAmount == 100)
        {
            TakeDamage(100);
        }
    }

    public void TakeDamage(float damage)
    {
        gaugePointAmount -= damage;
        if (gaugePointAmount < 0)
        {
            gaugePointAmount = 0;
        }
        UpdateGauge();
    }

    public void RestoreGaugePoints(float amountToRestore)
    {
            Debug.Log("Restoring gauge: " + amountToRestore);
        gaugePointAmount += amountToRestore;
        if (gaugePointAmount > maxGaugePointAmount)
        {
            gaugePointAmount = maxGaugePointAmount;
        }
        UpdateGauge();
    }

    private void UpdateGauge()
    {
        gaugeBar.fillAmount = gaugePointAmount / maxGaugePointAmount;
    }
}