using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats/player")]
public class playerstats : ScriptableObject
{
    //atack
    public float maxhealth;
    public float atkdmg;
    public float atkcooldown;
    public float atkrange;
    public AudioClip atksfx;

    public float skilldmg;
    public float skillcooldown;
    public float skillrange;
    public AudioClip skillsfx;

    public float ultdmg;
    public float Rockultrange;
    public Vector2 edmUltRange;
    public AudioClip ultsfx;
    public AudioClip ultReadysfx;

    public LayerMask enemylayer;
    public float healthrestore;
    public float gaugerestoreItem;
    public float gaugerestoreHit;
    public List<attackscirptableobject> combo;
    public List<AudioClip> combosfx;

    //movement
    public float speed;
    public float jumpingPower;

    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;

    public LayerMask groundlayer;
    public float XknockbackForce;
    public float YknockbackForce;
    public Vector2 atkpointpos;
}
