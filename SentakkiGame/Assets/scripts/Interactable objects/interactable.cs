using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class interactable : MonoBehaviour
{
    private bool caninteract;
    public float objectspeed;
    private bool righthit;
    private bool lefthit;
    [SerializeField] private GameObject prompttext;
    [SerializeField] private Transform rightside;
    [SerializeField] private Transform leftside;
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
        if(checkrightside() || checkleftside())
        {
            prompttext.SetActive(true);

            if (Input.GetKeyDown(KeyCode.J))
            {
                if (righthit)
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0 - objectspeed, 0), ForceMode2D.Impulse);
                }
                else if (lefthit)
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(objectspeed, 0), ForceMode2D.Impulse);
                }
            }
        }
        else
        {
            prompttext.SetActive(false);
        }


    }

    private bool checkrightside()
    {
        righthit = true;
        lefthit = false;
        return Physics2D.OverlapCircle(rightside.position, attackrange, playerlayer);
    }

    private bool checkleftside()
    {
        lefthit = true;
        righthit = false;
        return Physics2D.OverlapCircle(leftside.position, attackrange, playerlayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
/*        if(collision.CompareTag("Player"))
        {
            prompttext.SetActive(true);
            caninteract = true;
        }*/

        if(collision.CompareTag("enemy"))
        {
            Destroy(gameObject);
        }
    }

/*    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            prompttext.SetActive(false);
            caninteract = false;
        }
    }*/

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(rightside.position, attackrange);
        Gizmos.DrawWireSphere(leftside.position, attackrange);
    }
}
