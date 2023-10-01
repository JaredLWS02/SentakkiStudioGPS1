using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMechanic : MonoBehaviour
{
    public GameObject CharPos;
    public GameObject Player1;
    public GameObject Player2;
    public AudioSource p1;
    public AudioSource p2;
    public movement player1Control;
    public movement player2Control;
    public SpriteRenderer P1Sprite;
    public SpriteRenderer P2Sprite;
    public bool player1Active = true;

    void Start()
    {
        P1Sprite.enabled = true;
        p1.volume = 0.2f;
        p2.volume = 0.0f;
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
            Player1.GetComponent<Rigidbody2D>().simulated = false;
            P1Sprite.enabled = false;
            player1Control.enabled = false;
            p1.volume = 0.0f;
<<<<<<< HEAD:SentakkiGame/Assets/SwitchMechanic.cs
            p2.volume = 0.2f;
            Player2.GetComponent<Collider2D>().enabled = true;
=======
            p2.volume = 1.0f;
            Player2.GetComponent<Rigidbody2D>().simulated = true;
>>>>>>> nyak2's-Branch:SentakkiGame/Assets/scripts/SwitchMechanic.cs
            Player2.transform.position = CharPos.transform.position;
            player2Control.enabled = true;
            P2Sprite.enabled = true;
            player1Active = false;
        }
        else
        {
            Player1.GetComponent<Rigidbody2D>().simulated = true;
            Player1.transform.position = CharPos.transform.position;
            player1Control.enabled = true;
            P1Sprite.enabled = true;
            p1.volume = 0.2f;
            p2.volume = 0.0f;
            Player2.GetComponent<Rigidbody2D>().simulated = false;
            player2Control.enabled = false;
            P2Sprite.enabled = false;
            player1Active = true;
        }
    }
}
