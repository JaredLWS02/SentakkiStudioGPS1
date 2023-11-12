using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactables : MonoBehaviour
{
    [SerializeField] private GameObject prompttext;
    [SerializeField] private GameObject tutorialText;
    [SerializeField] private GameObject escapetext;
    [SerializeField] private PauseMenu pause;
    [SerializeField] private GameObject Interactables;
    private bool caninteract;
    private bool opened;
    public GameObject PausePanelControls;
    private bool activated;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.J) && caninteract && !opened)
        {
            PausePanelControls.SetActive(true);
            escapetext.SetActive(true);
            tutorialText.SetActive(true);

            PauseMenu.instance.isPaused = true;
            pause.enabled = false;
            Time.timeScale = 0f;
            opened = true;
        }
        else if (Input.GetKeyDown(KeyCode.J) && opened)
        {
            PausePanelControls.SetActive(false);
            escapetext.SetActive(false);
            tutorialText.SetActive(false);

            PauseMenu.instance.isPaused = false;
            pause.enabled = true;
            opened = false;
            Time.timeScale = 1f;
            Interactables.SetActive(true);
            activated = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<playerattack>().enabled = false;
            prompttext.SetActive(true);
            caninteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<playerattack>().enabled = true;
            prompttext.SetActive(false);
            caninteract = false;
        }
    }
}
