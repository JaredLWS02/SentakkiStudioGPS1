using UnityEngine;

public class RecoverySystem : MonoBehaviour
{
    [SerializeField] private playerstats stats;
    [SerializeField] private healthPoint HealthBar;
    [SerializeField] private GaugePoint gaugePoint;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        HealthBar = player.GetComponent<healthPoint>();
        gaugePoint = player.GetComponent<GaugePoint>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && this.CompareTag("hp"))
        {
            if (!swapmechanic.instance.player1Active)
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
        }

        if (col.CompareTag("Player") && this.CompareTag("mp"))
        {
            if (gaugePoint.gaugePointAmount < gaugePoint.maxGaugePointAmount)
            {
                Destroy(gameObject);
                gaugePoint.RestoreGaugePoints(stats.gaugerestoreItem);
            }

        }
    }
}