using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    public GameObject PausePanel;
    public bool isPaused;
    public AudioSource[] allAudioSources;
    public AudioSource pause;
    public AudioSource unpause;
    public GameObject optionObject;
    public float tempPause;

    private void Start()
    {
        instance = this;
    }
    void Update()
    {
        if(!optionObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (PausePanel.activeSelf)
                {
                    Continue();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void Pause ()
    {
        tempPause = Time.timeScale;
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            if(!audioS.CompareTag("stageMusic"))
            {
                audioS.Pause();
            }
        }
        isPaused = true;
        PausePanel.SetActive(true);
        Time.timeScale = 0;
        pause.Play();
    }

    public void Continue()
    {
        foreach (AudioSource audioS in allAudioSources)
        {
            if (!audioS.CompareTag("stageMusic"))
            {
                audioS.UnPause();
            }
        }
        isPaused = false;
        PausePanel.SetActive(false);
        Time.timeScale = tempPause;
        unpause.Play();
    }
}
