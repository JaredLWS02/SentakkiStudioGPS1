using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class swapmechanic : MonoBehaviour
{
    public static swapmechanic instance;
    [SerializeField] private SwapScript swap;
    [SerializeField] private AudioSource p1;
    [SerializeField] private AudioSource p2;
    [SerializeField] private movement playerControl;
    [SerializeField] private Animator animplayer2;
    [SerializeField] private playerattack playerattack;

    public bool player1Active = true;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        /*        skin = swap.character1;*/
        p1.volume = 0.2f;
        p2.volume = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchPlayer();
        }
    }

    public void SwitchPlayer()
    {
        if (player1Active)//swap to player2
        {
            GetComponent<playerattack>().stats = swap.p2Stats;
            GetComponent<movement>().stats = swap.p2Stats;
            GetComponent<skillandultimate>().stats = swap.p2Stats;
            GetComponent<Animator>().runtimeAnimatorController = swap.player2;
            healthPoint.Instance.UpdateHealth();
            //playerattack.Attack();
            //GetComponent<SpriteRenderer>().sprite = swap.character1
            p1.volume = 0.0f;
            p2.volume = 0.2f;
            player1Active = false;
        }
        else // swap to player1
        {
            GetComponent<playerattack>().stats = swap.p1Stats;
            GetComponent<movement>().stats = swap.p1Stats;
            GetComponent<skillandultimate>().stats = swap.p1Stats;
            GetComponent<Animator>().runtimeAnimatorController = swap.player1;
            healthPoint.Instance.UpdateHealth();
            //playerattack.Attack();
            //GetComponent<SpriteRenderer>().sprite = swap.character2;
            p1.volume = 0.2f;
            p2.volume = 0.0f;
            player1Active = true;
        }
    }
    
}
