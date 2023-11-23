using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsPlayerMenu : MonoBehaviour
{

    [SerializeField] private Animator anim;
    [SerializeField] private playerstats stats;
    [SerializeField] private SwapScript swap;
    private int combocounter;
    [SerializeField] private AudioSource combosource;
    [SerializeField] private AudioSource atksource;
    [SerializeField] private AudioSource jumpSource;
    private bool isPlaying;
    private bool p1active;

    private float horizontal;
    private bool isFacingRight = true;

    public GameObject rightbarrier;
    public GameObject leftbarrier;
    public GameObject topbarrier;
    public GameObject bottombarrier;

    private bool moveKeyPress;
    public bool jumping;
    private bool move;


    // Start is called before the first frame update
    void Start()
    {
        jumping = false;
        move = false;
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, leftbarrier.transform.position.x, rightbarrier.transform.position.x), transform.position.y);
        horizontal = Input.GetAxisRaw("Horizontal");

        if(!move)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                if(transform.localScale.x > 0)
                {
                    transform.position = new Vector2(transform.position.x + 0.05f, transform.position.y);
                }
                else
                {
                    transform.position = new Vector2(transform.position.x - 0.05f, transform.position.y);
                }
                moveKeyPress = true;
                anim.SetBool("move", true);
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f && anim.GetCurrentAnimatorStateInfo(0).IsTag("move"))
                {
                    anim.Play("walk loop", 0, 0);
                }

            }
            else
            {
                moveKeyPress = false;
                anim.SetBool("move", false);
            }

            if(Input.GetKeyDown(KeyCode.Space) && !jumping)
            {
                jumping = true;
                LeanTween.moveY(gameObject, topbarrier.transform.position.y, 0.3f).setIgnoreTimeScale(true).setEaseOutCirc();
                LeanTween.moveY(gameObject, bottombarrier.transform.position.y, 0.2f).setDelay(0.35f).setIgnoreTimeScale(true).setEaseOutCirc();
                jumpSource.Play(); 
                anim.Play("jump start", 0, 0);
            }

        }


        if (isPlaying)
        {
            //if(moveKeyPress)
            //{
            //    return;
            //}

            if(anim.GetCurrentAnimatorStateInfo(0).IsName("plunge"))
            {
                if (Mathf.Round(transform.position.y) <= bottombarrier.transform.position.y + 0.5f)
                {
                    isPlaying = false;
                    jumping = false;
                    anim.Play("plunge", 0, 0.6f);
                }
                return;
            }

            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
            {
                jumping = false;
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
                jumping = false;
                stats = swap.p2Stats;
                combocounter = 0;
                isPlaying = true;
                anim.runtimeAnimatorController = swap.player2;
                anim.Play("SwapAtk", 0, 0);
                p1active = true;
            }
            else
            {
                jumping = false;
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
            if (jumping)
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
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void setjump()
    {
        jumping = false;
    }
    private void playidle()
    {
        anim.Play("idle", 0, 0);
    }

    private void endjump()
    {
        if (Mathf.Round(transform.position.y) -1 <= bottombarrier.transform.position.y)
        {
            anim.Play("jump end", 0, 0);
        }
    }
    private void disablemovement()
    {
        move = true;
        //GetComponent<movementControl>().enabled = false;
    }

    private void enablemovement()
    {
        move = false;
        //GetComponent<movementControl>().enabled = true;
    }
    //private void addDownForce()
    //{
    //    rb.AddForce(new Vector2(0, -stats.jumpingPower), ForceMode2D.Impulse);
    //}
}
