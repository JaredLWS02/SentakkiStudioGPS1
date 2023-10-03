using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spriteDisplay : MonoBehaviour
{
    public SwapScript swap;
    public GameObject Character;
    public SpriteRenderer skin1;
    public SpriteRenderer skin2;
    public AudioSource p1;
    public AudioSource p2;
    public movement playerControl;
    public bool player1Active = true;
    // Start is called before the first frame update
    void Start()
    {
        skin = swap.character1;
        p1.volume = 0.2f;
        p2.volume = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchPlayer();
        }
    }

    public void SwitchPlayer()
    {
        if (player1Active)
        {
            skin = swap.character1;
            p1.volume = 0.0f;
            p2.volume = 0.2f;
            player1Active = false;
        }
        else
        {
            skin = swap.character2;
            p1.volume = 0.2f;
            p2.volume = 0.0f;
            player1Active = true;
        }
    }
}
