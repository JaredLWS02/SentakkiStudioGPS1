using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMechanic : MonoBehaviour
{
    public GameObject CharPos;
    public GameObject Player1;
    public GameObject Player2;
    public PlayerController player1Control;
    public PlayerController player2Control;
    public SpriteRenderer P1Sprite;
    public SpriteRenderer P2Sprite;
    public bool player1Active = true;

    void Start()
    {
        P1Sprite.enabled = true;
    }
    void Update()
    {
        if(player1Active)
        {
            CharPos.transform.position = Player1.transform.position;
        }
        else
        {
            CharPos.transform.position = Player2.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchPlayer();
        }
    }

    public void SwitchPlayer()
    {
        if (player1Active)
        {
            Player1.SetActive(false);
            player1Control.enabled = false;
            P1Sprite.enabled = false;
            Player2.SetActive(true);
            Player2.transform.position = CharPos.transform.position;
            player2Control.enabled = true;
            P2Sprite.enabled = true;
            player1Active = false;
        }
        else
        {
            Player1.SetActive(true);
            Player1.transform.position = CharPos.transform.position;
            player1Control.enabled = true;
            P1Sprite.enabled = true;
            Player2.SetActive(false);
            player2Control.enabled = false;
            P2Sprite.enabled = false;
            player1Active = true;
        }
    }
}
