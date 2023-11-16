using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class movementUI : MonoBehaviour
{
    public static movementUI instance;
    [SerializeField] private GameObject tutorialText;
    [SerializeField] private GameObject escapetext;
    [SerializeField] private PauseMenu pause;
    private bool opened;
    public bool ControlUIPause;
    public GameObject PausePanelControls;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("tutorial",0.1f);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.J) && caninteract && !opened)
        //{
        //    ControlUIPause = true;
        //    PausePanelControls.SetActive(true);
        //    pause.enabled = false;
        //    opened = true;
        //    tutorialText.SetActive(true);
        //    Time.timeScale = 0f;
        //}

        if(Input.GetKeyDown(KeyCode.F) && opened)
        {
            PausePanelControls.SetActive(false);
            escapetext.SetActive(false);
            tutorialText.SetActive(false);
            PauseMenu.instance.isPaused = false;
            ControlUIPause = false;
            pause.enabled = true;
            opened = false;
            Time.timeScale = 1f;
        }
    }

    void tutorial()
    {
        PausePanelControls.SetActive(true);
        escapetext.SetActive(true);
        tutorialText.SetActive(true);
        PauseMenu.instance.isPaused = true;
        ControlUIPause = true;
        pause.enabled = false;
        opened = true;
        Time.timeScale = 0f;

    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        collision.gameObject.GetComponent<playerattack>().enabled = false;
    //        prompttext.SetActive(true);
    //        caninteract = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        collision.gameObject.GetComponent<playerattack>().enabled = true;
    //        prompttext.SetActive(false);
    //        caninteract = false;
    //    }
    //}
}
