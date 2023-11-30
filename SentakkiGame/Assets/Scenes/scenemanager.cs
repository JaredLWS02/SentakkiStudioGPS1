using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
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
        HomeScreen,
        mainmenu,
        controls,
        Dialogue,
        tutorialStage,
        stageClearTutorial,
        deathTutorial,
        stage1,
        stageClearStage1,   
        deathStage1,
        Congrat

    }
    public void switchmenu()
    {
        SceneManager.LoadScene(Scene.mainmenu.ToString());
    }

    public void switchtoTutorialStage()
    {
        SceneManager.LoadScene(Scene.tutorialStage.ToString());
    }

    public void switchtoDialogue()
    {
        SceneManager.LoadScene(Scene.Dialogue.ToString());
    }

    public void switchtoStage1()
    {
        SceneManager.LoadScene(Scene.stage1.ToString());
    }

    public void switchCongrats()
    {
        SceneManager.LoadScene(Scene.Congrat.ToString());
    }

    public void switchDeathTutorialStage()
    {
        SceneManager.LoadScene(Scene.deathTutorial.ToString());
    }

    public void switchTutorialStageClear()
    {
        SceneManager.LoadScene(Scene.stageClearTutorial.ToString());
    }

    public void switchDeathStage1()
    {
        SceneManager.LoadScene(Scene.deathStage1.ToString());
    }

    public void switchStage1Clear()
    {
        SceneManager.LoadScene(Scene.stageClearStage1.ToString());
    }

    public void switchControls()    
    {
        SceneManager.LoadScene(Scene.controls.ToString());
    }

    public void Exit()
    {
        Application.Quit();
    }

}