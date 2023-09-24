using System.Collections;
using UnityEngine;
using TMPro;

public class attack : MonoBehaviour
{
    public static attack instance;

    private bool canAttack;
    public bool isAttacking;
    private bool attackcooldown = false;
    private int combocounter;
    private int innercombocounter;

    /*    private bool plungeAttack;*/
    /*    private bool plungeCooldown = false;*/
    /*    public bool isPlunging;*/

    private float combotimeConstant = 0.6f;
    private float attacktimer;
    private bool isCombo;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerattackhitbox;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TextMeshProUGUI combo;


    private void Start()
    {
        innercombocounter = 0;
        combo.text = "x 0";
        instance = this;
    }

    void Update()
    {
        Debug.DrawRay(player.transform.position, Vector3.down * 2.5f, Color.blue);

        if (!movement.instance.isDashing)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
/*                if (Physics2D.Raycast(player.transform.position, Vector2.down, 2.5f, groundLayer))
                {*/
                    if(movement.instance.IsGrounded() && !attackcooldown)
                    {
                        Attack();
                        StartCoroutine(checkattackcooldown());
                    }
/*                }*/
/*                else
                {
                    if(!plungeCooldown && !isPlunging)
                    {
                        StartCoroutine(PlungeAttack());
                    }
                }*/
                
            }
        }

    }

    private IEnumerator checkattackcooldown()
    {
        attackcooldown = true;
        if(isCombo)
        {
            yield return new WaitForSeconds(0.15f);
            isAttacking = false;
            yield return new WaitForSeconds(0.25f);
            attackcooldown = false;
        }
        else
        {
            yield return new WaitForSeconds(0.15f);
            isAttacking = false;
            yield return new WaitForSeconds(0.2f);
            attackcooldown = false;
        }
    }
    private void Attack()
    {
        isAttacking = true;
        if (attacktimer <= 0)
        {
            attacktimer = Time.time;
        }

        if (isCombo)
        {
            if((Time.time - attacktimer) < combotimeConstant)
            {
                combocounter += 1;
                combo.text = "x " + combocounter.ToString();
                attacktimer = Time.time;
                innercombocounter += 1;
                if (innercombocounter == 1)
                {
                    Attack2();
                }
                else if (innercombocounter == 2)
                {
                    Attack3();
                }
                else if (innercombocounter == 3)
                {
                    Attack4();
                }
                else
                {
                    Debug.Log("Combo reset");
                    innercombocounter = 0;
                    Attack1();
                }
            }
            else
            {
                innercombocounter = 0;
                isCombo = false;
                combocounter = 0;
                combo.text = "x " + combocounter.ToString();
                attacktimer = Time.time;
                Debug.Log("fail combo");
            }
        }
        else
        {
            isCombo = true;
            if(canAttack)
            {
                combocounter += 1;
                combo.text = "x " + combocounter.ToString();
            }
            else
            {
                combo.text = "x " + combocounter.ToString();
            }
                attacktimer = Time.time;
                Attack1();
        }
 
    }

    private void Attack1()
    {
        if(canAttack) 
        {
            Debug.Log("Hit1");
        }
        else
        {
            isCombo = false;
            Debug.Log("Miss1");
        }
    }

    private void Attack2()
    {
        if (canAttack)
        {
            Debug.Log("Hit2");
        }
        else
        {
            isCombo = false;
            Debug.Log("Miss2");
        }
    }

    private void Attack3()
    {
        if (canAttack)
        {
            Debug.Log("Hit3");
        }
        else
        {
            isCombo = false;
            Debug.Log("Miss3");
        }
    }

    private void Attack4()
    {
        if (canAttack)
        {
            Debug.Log("Hit4");
        }
        else
        {
            isCombo = false;
            Debug.Log("Miss4");
        }
    }

    /*    private IEnumerator PlungeAttack()
        {
            isPlunging = true;
            plungeCooldown = true;
            rb.AddForce(Vector2.down * 40f, ForceMode2D.Impulse);

            if (Physics2D.Raycast(playerattackhitbox.transform.position, Vector2.down, 7f, enemyLayer))
            {
                plungeAttack = true;
                Debug.Log(plungeAttack);
            }
            else 
            {
                plungeAttack = false;
                Debug.Log(plungeAttack);
            }
            yield return new WaitForSeconds(0.1f);
            isPlunging = false;
            yield return new WaitForSeconds(0.4f);
            plungeCooldown = false;
        }*/

    private void OnTriggerEnter2D(Collider2D attackenemy)
    {
        if (attackenemy.CompareTag("enemyHitbox") && playerattackhitbox.CompareTag( "playerAttackHitbox"))
        {
            canAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D attackenemy)
    {
        canAttack = false;
    }
}
