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

    private void Start()
    {
        instance = this;
    }
    void Update()
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

    public void Pause ()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Pause();
        }
        isPaused = true;
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.UnPause();
        }
        isPaused = false;
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void Quit()
    {
        Application.Quit();
    }




}
