using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(menuName = "Sprites")]
public class SwapScript : ScriptableObject
{
    public playerstats p1Stats;
    public playerstats p2Stats;
    public Sprite character1;
    public Sprite character2;
    public AnimatorController player1;
    public AnimatorController player2;

}
