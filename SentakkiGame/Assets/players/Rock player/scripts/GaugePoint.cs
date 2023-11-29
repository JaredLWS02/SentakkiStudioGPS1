using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugePoint : MonoBehaviour
{
    public Image gaugeBar;
    public float maxGaugePointAmount = 100f;

    public static GaugePoint Instance;

    public float gaugePointAmount;
    public bool ultiReady;

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
        if(PlayerPrefs.HasKey("Gauge"))
        {
            gaugePointAmount = PlayerPrefs.GetFloat("Gauge");
        }
        else
        {
            gaugePointAmount = maxGaugePointAmount;
        }
        UpdateGauge();
    }

    private void Update()
    {
        if (gaugeBar.fillAmount < 1)
        {
            gaugeBar.color = new Color(1, 1, 1, 1);
            ultiReady = false; 
        }
    }

    public void ReduceGauge(float damage)
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
        Debug.Log(gaugeBar.fillAmount);
        gaugeBar.fillAmount = gaugePointAmount / maxGaugePointAmount;
        if(gaugeBar.fillAmount >= 1)
        {
            gaugeBar.color = new Color(1, 0, 0, 1);
        }
    }
}