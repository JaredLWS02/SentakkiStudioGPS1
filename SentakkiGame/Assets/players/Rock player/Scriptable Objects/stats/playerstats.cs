using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/player")]
public class playerstats : ScriptableObject
{
    //atack
    public float health;
    public float atkdmg;
    public float atkcooldown;
    public float atkrange;

    public float skilldmg;
    public float skillcooldown;
    public float skillrange;

    public float ultdmg;
    public float ultrange;

    public LayerMask enemylayer;
    public float healthrestore;
    public float gaugerestore;
    public List<attackscirptableobject> combo;

    //movement
    public float speed;
    public float jumpingPower;

    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;

    public LayerMask groundlayer;
}
