using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class interactable : MonoBehaviour
{
    private bool caninteract;
    public float objectspeed;
    public float atkdmg;
    private bool thrown;
    private Collider2D hitplayer;
    [SerializeField] private GameObject prompttext;
    [SerializeField] private GameObject shadow;
    [SerializeField] private AudioSource sfx;
    [SerializeField] private Transform interactarea;
    [SerializeField] private float attackrange;
    [SerializeField] private LayerMask playerlayer;

    // Start is called before the first frame update
    void Start()
    {
        sfx = GameObject.FindGameObjectWithTag("sfxPlayerAndEnemy").GetComponent<AudioSource>();
        prompttext.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && caninteract)
        {
            checkside();
        }
    }

    private void checkside()
    {
        prompttext.SetActive(false);
        shadow.SetActive(false);
        hitplayer = Physics2D.OverlapCircle(interactarea.position, attackrange, playerlayer);

        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(3))
        {
            hitplayer.GetComponent<Animator>().Play("throwRadio", 0, 0);
        }
        else
        {
            hitplayer.GetComponent<Animator>().Play("throwDaruma", 0, 0);
        }
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        StartCoroutine(throwing());
    }

    private IEnumerator throwing()
    {
        yield return new WaitForSeconds(0.4f);
        thrown = true;
        if (hitplayer.GetComponent<Transform>().localScale.x > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(objectspeed, 4), ForceMode2D.Impulse);
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0 - objectspeed, 4), ForceMode2D.Impulse);
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        Invoke("DestroySelf", 2);

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
                sfx.Play();
                collision.GetComponent<EnemyAi>().takeDamage(atkdmg);
            }
            else if (collision.CompareTag("enemyMelee"))
            {
                sfx.Play();
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

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
