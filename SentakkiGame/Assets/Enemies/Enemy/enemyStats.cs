using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStats")]
public class enemyStats : ScriptableObject
{
    public Sprite skin; 
    public float maxhp;
    public float dmg;
    public float chargetime;
    public float chargeSpd;
    public float atkcooldown;
    public float XknockbackForce;
    public float YknockbackForce;
    public LayerMask playerLayers;
}
