using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class swapmechanic : MonoBehaviour
{
    public static swapmechanic instance;
    [SerializeField] private SwapScript swap;
    [SerializeField] private AudioSource p1;
    [SerializeField] private AudioSource p2;
    [SerializeField] private AudioSource swapSource;
    [SerializeField] private movement playerControl;
    [SerializeField] private playerattack playerattack;
    [SerializeField] private GameObject p1Icon;
    [SerializeField] private GameObject p2Icon;


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
        if (Input.GetKeyDown(KeyCode.Q) && (Time.time - lastswapTime ) >= swap.swapcooldown)
        {
            swapSource.Play();
            SwitchPlayer();

            lastswapTime = Time.time;
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
            //playerattack.Attack();
            //GetComponent<SpriteRenderer>().sprite = swap.character1
            setVolume();
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
            //playerattack.Attack();
            //GetComponent<SpriteRenderer>().sprite = swap.character2;
            setVolume();
            player1Active = true;
            healthPoint.Instance.UpdateHealth();
        }
    }

    public void setVolume()
    {
        if (player1Active)//swap to player2
        {
            Debug.Log("player2Music");
            p1.volume = 0.0f;
            p2.volume = volume;
        }
        else // swap to player1
        {
            Debug.Log("player1Music");
            p1.volume = volume;
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
    
}
