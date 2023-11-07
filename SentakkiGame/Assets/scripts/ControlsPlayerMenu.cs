using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsPlayerMenu : MonoBehaviour
{

    [SerializeField] private Animator atkanim;
    [SerializeField] private Animator animationskill;
    [SerializeField] private Animator moveanim;
    [SerializeField] private Rigidbody2D rb;
    public playerstats stats;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            atkanim.Play("attack", 0, 0);

        }

         if (Input.GetKeyDown(KeyCode.U))
        {
             animationskill.Play("skill", 0, 0);

        }

                 if (Input.GetKeyDown(KeyCode.Space))
        {
             moveanim.Play("jump", 0, 0);
             rb.AddForce(Vector2.up * stats.jumpingPower, ForceMode2D.Impulse);

        }



        
    }
}
