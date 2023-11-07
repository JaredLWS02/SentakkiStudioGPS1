using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsPlayerMenu : MonoBehaviour
{

    [SerializeField] private Animator atkanim;
    [SerializeField] private Animator animationskill;
    [SerializeField] private Animator moveanim;




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

         if (Input.GetKeyDown(KeyCode.I))
        {
             animationskill.Play("skill", 0, 0);

        }

                 if (Input.GetKeyDown(KeyCode.Space))
        {
             moveanim.Play("jump", 0, 0);

        }



        
    }
}
