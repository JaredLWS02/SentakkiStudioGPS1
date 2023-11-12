using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenemanager : MonoBehaviour
{
    public static scenemanager instance;

    private void Start()
    {
        instance = this;
    }

    public enum Scene
    {
        mainmenu,
        settings,
        controls,
        play,
        death,
        stageClear,

    }

    public void switchmenu()
    {
        SceneManager.LoadScene(Scene.mainmenu.ToString());
    }

    public void switchPlay()
    {
        SceneManager.LoadScene(Scene.play.ToString());
    }

    public void switchDeath()
    {
        SceneManager.LoadScene(Scene.death.ToString());
    }

    public void switchStageClear()
    {
        SceneManager.LoadScene(Scene.stageClear.ToString());
    }

    public void switchControls()    
    {
        SceneManager.LoadScene(Scene.controls.ToString());
    }

}