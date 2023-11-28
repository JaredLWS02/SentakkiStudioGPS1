using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AOEDmg : MonoBehaviour
{
    [SerializeField] private float dmg;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            healthPoint.Instance.TakeDamage(dmg);
        }
    }
}
