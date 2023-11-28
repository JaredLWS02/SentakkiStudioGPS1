using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactables : MonoBehaviour
{
    [SerializeField] private GameObject prompttext;
    [SerializeField] private GameObject healText;
    [SerializeField] private GameObject gaugeText;
    [SerializeField] private GameObject escapetext;
    [SerializeField] private PauseMenu pause;
    [SerializeField] private GameObject Interactables;
    [SerializeField] private GameObject invisibleWall;
    private bool caninteract;
    private bool opened;
    public GameObject PausePanelControls;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && caninteract)
        {
            LeanTween.scaleX(healText, 0.8f, 0.3f);
            LeanTween.scaleY(healText, 0.72f, 0.3f);

            LeanTween.scaleX(gaugeText, 0.8f, 0.3f);
            LeanTween.scaleY(gaugeText, 0.72f, 0.3f);

            Interactables.SetActive(true);
            invisibleWall.SetActive(false);
            prompttext.SetActive(false);
        }
        //else if (Input.GetKeyDown(KeyCode.F) && opened)
        //{
        //    PausePanelControls.SetActive(false);
        //    escapetext.SetActive(false);
        //    tutorialText.SetActive(false);

        //    PauseMenu.instance.isPaused = false;
        //    pause.enabled = true;
        //    opened = false;
        //    Time.timeScale = 1f;
        //    Interactables.SetActive(true);
        //}
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.CompareTag("speaker"))
        {
            prompttext.SetActive(true);
            caninteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.CompareTag("speaker"))
        {
            LeanTween.scaleX(gaugeText, 0, 0.3f);
            LeanTween.scaleY(gaugeText, 0, 0.3f);

            LeanTween.scaleX(healText, 0, 0.3f);
            LeanTween.scaleY(healText, 0, 0.3f);

            prompttext.SetActive(false);
            caninteract = false;
        }
    }
}
