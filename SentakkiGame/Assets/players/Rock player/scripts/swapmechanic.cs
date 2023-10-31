using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class swapmechanic : MonoBehaviour
{
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
        if (player1Active)
        {
            playerattack.Attack();
            //GetComponent<Animator>().enabled = true;
            //GetComponent<SpriteRenderer>().sprite = swap.character1
            p1.volume = 0.0f;
            p2.volume = 0.2f;
            player1Active = false;
        }
        else
        {
            playerattack.Attack();
            //GetComponent<Animator>().enabled = false;
            //GetComponent<SpriteRenderer>().sprite = swap.character2;
            p1.volume = 0.2f;
            p2.volume = 0.0f;
            player1Active = true;
        }
    }
}
