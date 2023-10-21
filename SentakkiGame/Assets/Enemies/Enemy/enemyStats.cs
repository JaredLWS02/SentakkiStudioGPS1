using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStats")]
public class enemyStats : ScriptableObject
{
    public Sprite skin; 
    public float maxhp;
    public float atk;
    public float speed;
    public float chargeSpd;
    public LayerMask playerLayers;
}
