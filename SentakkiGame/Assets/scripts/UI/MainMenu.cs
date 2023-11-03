using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        scenemanager.instance.switchPlay();
    }

    public void GoToMainMenu()
    {
        scenemanager.instance.switchmenu();
    }

    public void QuitGame()
    {
        Application.Quit();
    }



}
