using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public int maximum;
    public int minimum;
    public int current;
    public int incrementAmount = 10;
    public Image mask;
    public Spawn spawnScript;
    public CameraScript CameraScript;

    void Start()
    {
        current = minimum; // Initialize the current value
    }

    // Update the progress bar when an enemy is killed
    public void UpdateProgressBar()
    {
        if (current < maximum)
        {
            current += incrementAmount;
            GetCurrentFill();
        }

        if (current >= maximum)
        {
            MaxProgressBar();
        }
    }

    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;
        mask.fillAmount = fillAmount;
    }

    void MaxProgressBar()
    {
        if (current == maximum)
        {
            spawnScript.PauseSpawning();
            CameraScript.StopFollowing();
            Debug.Log ("Spawn Boss");
        }
    }
}