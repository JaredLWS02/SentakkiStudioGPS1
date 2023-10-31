using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStats")]
public class enemyStats : ScriptableObject
{
    public Sprite skin; 
    public float maxhp;
    public float dmg;
    public float chargeSpd;
    public float atkcooldown;
    public Vector2 knockbackForce;
    public LayerMask playerLayers;
}
