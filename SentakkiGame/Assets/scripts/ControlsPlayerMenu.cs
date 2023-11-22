using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsPlayerMenu : MonoBehaviour
{

    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private playerstats stats;
    [SerializeField] private SwapScript swap;
    private int combocounter;
    [SerializeField] private AudioSource combosource;
    [SerializeField] private AudioSource atksource;
    private bool isPlaying;
    private bool p1active;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlaying)
        {
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("plunge"))
            {
                if (movement.instance.IsGrounded())
                {
                    isPlaying = false;
                    anim.Play("plunge", 0, 0.6f);
                }
                return;
            }

            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
            {
                isPlaying = false;
                anim.Play("idle", 0, 0);
            }
            return;
        }

        if (combocounter >= stats.combo.Count)
        {
            combocounter = 0;
        }

        if (Input.GetKeyDown(KeyCode.Q) && !isPlaying)
        {
            if(!p1active)
            {
                stats = swap.p2Stats;
                combocounter = 0;
                isPlaying = true;
                anim.runtimeAnimatorController = swap.player2;
                anim.Play("SwapAtk", 0, 0);
                p1active = true;
            }
            else
            {
                stats = swap.p1Stats;
                combocounter = 0;
                isPlaying = true;
                anim.runtimeAnimatorController = swap.player1;
                anim.Play("SwapAtk", 0, 0);
                p1active = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.H) && !isPlaying)
        {
            isPlaying = true;

            anim.Play("skill", 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.U) && !isPlaying)
        {
            //isPlaying = true;

            //anim.Play("skill", 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.J) && !isPlaying)
        {
            if (!movement.instance.IsGrounded())
            {
                isPlaying = true;
                anim.Play("plunge", 0, 0);
            }
            else
            {
                isPlaying = true;
                combosource.clip = stats.combosfx[combocounter];
                atksource.clip = stats.atksfx;
                combosource.Play();
                atksource.Play();
                if (p1active)
                {
                    if (combocounter == 0)
                    {
                        anim.Play("attack", 0, 0);
                    }
                    else if (combocounter == 1)
                    {
                        anim.Play("attack 2", 0, 0);
                    }
                    else if (combocounter == 2)
                    {
                        anim.Play("attack 3", 0, 0);
                    }
                }
                else
                {
                    if(combocounter == 0)
                    {
                        anim.Play("attack", 0, 0);
                    }
                    else if (combocounter == 1)
                    {
                        anim.Play("attack 2", 0, 0);
                    }
                    else if (combocounter == 2)
                    {
                        anim.Play("attack 3", 0, 0);
                    }
                    else if (combocounter == 3)
                    {
                        anim.Play("attack 4", 0, 0);
                    }
                    else if (combocounter == 4)
                    {
                        anim.Play("attack 5", 0, 0);
                    }

            }
            }
            combocounter++;
        }

    }

    private void disablemovement()
    {
        GetComponent<movement>().enabled = false;
    }

    private void enablemovement()
    {
        GetComponent<movement>().enabled = true;
    }
    private void addDownForce()
    {
        rb.AddForce(new Vector2(0, -stats.jumpingPower), ForceMode2D.Impulse);
    }
}
