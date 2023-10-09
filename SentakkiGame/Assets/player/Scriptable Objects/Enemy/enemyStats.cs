using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStats")]
public class enemyStats : ScriptableObject
{
    public Sprite skin; 
    public float hp;
    public float atk;
    public float speed;
}
