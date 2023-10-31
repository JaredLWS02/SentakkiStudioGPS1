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
            if(!swapmechanic.instance.player1Active)
            {
                if (HealthBar.currenthealthAmountP2 < HealthBar.maxHealthAmount)
                {
                    Destroy(gameObject);
                    HealthBar.RestoreHealthPoints(stats.healthrestore);
                }
            }
            else
            {
                if (HealthBar.currenthealthAmountP1 < HealthBar.maxHealthAmount)
                {
                    Destroy(gameObject);
                    HealthBar.RestoreHealthPoints(stats.healthrestore);
                }
            }

            if (gaugePoint.gaugePointAmount < gaugePoint.maxGaugePointAmount)
            {
                Destroy(gameObject);
                gaugePoint.RestoreGaugePoints(stats.gaugerestoreItem);
            }

        }
}
}