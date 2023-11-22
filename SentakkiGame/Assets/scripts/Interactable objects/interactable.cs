using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class interactable : MonoBehaviour
{
    private bool caninteract;
    public float objectspeed;
    public float atkdmg;
    private bool thrown;
    [SerializeField] private GameObject prompttext;
    [SerializeField] private Transform interactarea;
    [SerializeField] private float attackrange;
    [SerializeField] private LayerMask playerlayer;

    // Start is called before the first frame update
    void Start()
    {
        prompttext.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && caninteract)
        {
            checkside();
        }
    }

    private void checkside()
    {
        Collider2D hitplayer = Physics2D.OverlapCircle(interactarea.position, attackrange, playerlayer);

        if(hitplayer.GetComponent<Transform>().localScale.x > 0)
        {
            thrown = true;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(objectspeed, 0), ForceMode2D.Impulse);
        }
        else
        {
            thrown = true;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0 - objectspeed, 0), ForceMode2D.Impulse);
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            prompttext.SetActive(true);
            caninteract = true;
        }

        if ((collision.CompareTag("enemy") || collision.CompareTag("enemyMelee") )&& thrown)
        {
            if(collision.CompareTag("enemy"))
            {
                collision.GetComponent<EnemyAi>().takeDamage(atkdmg);
            }
            else if (collision.CompareTag("enemyMelee"))
            {
                collision.GetComponent<EnemyAiMelee>().takeDamage(atkdmg);
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            prompttext.SetActive(false);
            caninteract = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(interactarea.position, attackrange);
    }
}
