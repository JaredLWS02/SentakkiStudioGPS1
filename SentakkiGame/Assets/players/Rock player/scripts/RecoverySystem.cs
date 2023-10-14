using UnityEngine;

public class RecoverySystem : MonoBehaviour
{
    [SerializeField] private playerstats stats;
    [SerializeField] private healthPoint HealthBar;
    [SerializeField] private GaugePoint gaugePoint;

private void OnTriggerEnter2D(Collider2D col)
{
    if (col.CompareTag("Player"))
    {
        if (HealthBar.healthAmount < HealthBar.maxHealthAmount)
        {
            Destroy(gameObject);
            HealthBar.RestoreHealthPoints(stats.healthrestore);
        }

        if (gaugePoint.gaugePointAmount < gaugePoint.maxGaugePointAmount)
        {
                Destroy(gameObject);
            gaugePoint.RestoreGaugePoints(stats.gaugerestore);
        }
    }
}
}