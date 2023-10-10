using UnityEngine;

public class RecoverySystem : MonoBehaviour
{
    public float hpRestore;
    public float gaugePointRestore;
    public healthPoint HealthBar;
    public GaugePoint gaugePoint;

private void OnTriggerEnter2D(Collider2D col)
{
    if (col.CompareTag("Player"))
    {
        if (HealthBar.healthAmount < HealthBar.maxHealthAmount)
        {
            Destroy(gameObject);
            HealthBar.RestoreHealthPoints(hpRestore);
        }
        
     if (gaugePoint.gaugePointAmount < gaugePoint.maxGaugePointAmount)
        {
            Destroy(gameObject);
            gaugePoint.RestoreGaugePoints(gaugePointRestore);
        }
    }
}
}