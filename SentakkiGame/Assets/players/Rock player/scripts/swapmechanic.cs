using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting;

public class swapmechanic : MonoBehaviour
{
    public static swapmechanic instance;
    [SerializeField] private playerstats stats;
    [SerializeField] private SwapScript swap;
    [SerializeField] private AudioSource p1;
    [SerializeField] private AudioSource p2;
    [SerializeField] private AudioSource swapSource;
    [SerializeField] private movement playerControl;
    [SerializeField] private playerattack playerattack;
    [SerializeField] private GameObject p1Icon;
    [SerializeField] private GameObject p2Icon;
    [SerializeField] private Transform swapattackpoint;
    [SerializeField] private Collider2D[] hitenemiesSwap;
    [SerializeField] private combomanagerUI combomanagerUI;

    private float lastswapTime;

    public bool player1Active;
    public float volume;
    private Vector3 posP1;
    private Vector3 posP2;
    private Vector2 sizeP1;
    private Vector2 sizeP2;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        sizeP1 = p1Icon.GetComponent<RectTransform>().sizeDelta;
        sizeP2 = p2Icon.GetComponent<RectTransform>().sizeDelta;
        posP1 = p1Icon.transform.position;
        posP2 = p2Icon.transform.position;
        /*        skin = swap.character1;*/
        p1.volume = 0.0f;
        p2.volume = 0.0f;
        player1Active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthPoint.Instance.currenthealthAmountP1 > 0 && healthPoint.Instance.currenthealthAmountP2 > 0)
        {
            if (Input.GetKeyDown(KeyCode.Q) && (Time.time - lastswapTime ) >= swap.swapcooldown)
            {
                swapSource.Play();
                SwitchPlayer();

                lastswapTime = Time.time;
            }
        }
    }

    public void SwitchPlayer()
    {
        swapIcons();
        if (player1Active)//swap to player2
        {
            GetComponent<playerattack>().stats = swap.p2Stats;
            GetComponent<playerattack>().combocounter = 0;
            GetComponent<movement>().stats = swap.p2Stats;
            GetComponent<skillandultimate>().stats = swap.p2Stats;
            GetComponent<Animator>().runtimeAnimatorController = swap.player2;
            GetComponent<Animator>().Play("SwapAtk", 0, 0);
            //playerattack.Attack();
            //GetComponent<SpriteRenderer>().sprite = swap.character1
            StartCoroutine(setVolume());
            player1Active = false;
            healthPoint.Instance.UpdateHealth();
        }
        else // swap to player1
        {
            GetComponent<playerattack>().stats = swap.p1Stats;
            GetComponent<playerattack>().combocounter = 0;
            GetComponent<movement>().stats = swap.p1Stats;
            GetComponent<skillandultimate>().stats = swap.p1Stats;
            GetComponent<Animator>().runtimeAnimatorController = swap.player1;
            GetComponent<Animator>().Play("SwapAtk", 0, 0);
            //playerattack.Attack();
            //GetComponent<SpriteRenderer>().sprite = swap.character2;
            StartCoroutine(setVolume());
            player1Active = true;
            healthPoint.Instance.UpdateHealth();
        }
    }

    public IEnumerator setVolume()
    {
        if (player1Active)//swap to player2
        {
            Debug.Log("player2Music");
            p1.volume = 0.0f;
            yield return new WaitUntil(() => !swapSource.isPlaying);
            p2.volume = volume;
        }
        else // swap to player1
        {
            Debug.Log("player1Music");
            p1.volume = volume;
            yield return new WaitUntil(() => !swapSource.isPlaying);
            p2.volume = 0.0f;

        }
    }

    public void swapIcons()
    {
        if (player1Active) 
        {
            Debug.Log("player2");
            p1Icon.GetComponent<Image>().color = new Color(0.65f, 0.65f, 0.65f);
            p2Icon.GetComponent<Image>().color = new Color(1f, 1f, 1f);

            p1Icon.GetComponent<RectTransform>().sizeDelta = sizeP2;
            p2Icon.GetComponent<RectTransform>().sizeDelta = sizeP1;

            p1Icon.transform.SetSiblingIndex(5);
            p2Icon.transform.SetSiblingIndex(6);

            p1Icon.transform.position = posP2;
            p2Icon.transform.position = posP1;
        }
        else
        {
            Debug.Log("player1");
            p2Icon.GetComponent<Image>().color = new Color(0.65f, 0.65f, 0.65f);
            p1Icon.GetComponent<Image>().color = new Color(1f, 1f, 1f);

            p2Icon.GetComponent<RectTransform>().sizeDelta = sizeP2;
            p1Icon.GetComponent<RectTransform>().sizeDelta = sizeP1;

            p1Icon.transform.SetSiblingIndex(6);
            p2Icon.transform.SetSiblingIndex(5);

            p2Icon.transform.position = posP2;
            p1Icon.transform.position = posP1;
        }
    }
    
    public void swapAtk()
    {
        hitenemiesSwap = Physics2D.OverlapCircleAll(swapattackpoint.position, stats.atkrange, stats.enemylayer);

        foreach (Collider2D enemy in hitenemiesSwap)
        {
            if (enemy.CompareTag("enemy"))
            {
                enemy.GetComponent<EnemyAi>().takeDamage(stats.atkdmg);
            }

            if (enemy.CompareTag("enemyMelee"))
            {
                enemy.GetComponent<EnemyAiMelee>().takeDamage(stats.atkdmg);
            }
            combomanagerUI.innercomboUI++;
            combomanagerUI.checkcombostatus();
        }
    }

    private void disableThings()
    {

        gameObject.layer = LayerMask.NameToLayer("ghostplayer");
        GetComponent<movement>().enabled = false;
        GetComponent<playerattack>().enabled = false;
        GetComponent<skillandultimate>().enabled = false;
    }

    private void enableThings()
    {
        gameObject.layer = LayerMask.NameToLayer("player");
        GetComponent<movement>().enabled = true;
        GetComponent<playerattack>().enabled = true;
        GetComponent<skillandultimate>().enabled = true;
    }
}
