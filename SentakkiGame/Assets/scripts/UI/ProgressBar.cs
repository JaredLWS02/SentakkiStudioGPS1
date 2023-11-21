using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{

    public static ProgressBar instance;
    public int maximum;
    public int minimum;
    public int current;
    public int incrementAmount = 10;
    public Image mask;
    public Spawn spawnScript;
    public CameraScript CameraScript;
    [SerializeField] public AudioSource stage;
    public float fillAmount;
    private bool loading;



    void Start()
    {
        instance = this;
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

    }

    void Update()
    {
        //GetCurrentFill();
        mask.fillAmount = Mathf.Lerp(mask.fillAmount,fillAmount,Time.deltaTime);
        if (current >= maximum && !loading )
        {
            loading = true;
            Invoke("MaxProgressBar",3f);
        }
    }

    void GetCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        fillAmount = currentOffset / maximumOffset;

        switch (current)
        {
            case 20:
                {
                    stage.volume = 0.15f;

                    swapmechanic.instance.SetVolume(0.08f);
                }
                break;
            case 40:
                {
                    stage.volume = 0.1f;
                    swapmechanic.instance.SetVolume(0.12f);
                }
                break;
            case 70:
                {
                    stage.volume = 0.08f;
                    swapmechanic.instance.SetVolume(0.15f);

                }
                break;
            case 100:
                {
                    stage.volume = 0;
                    swapmechanic.instance.SetVolume(0.2f);

                }
                break;
        }

    }

    void MaxProgressBar()
    {
            spawnScript.PauseSpawning();
            CameraScript.StopFollowing();
            SceneManager.LoadSceneAsync(3);
            Debug.Log ("Spawn Boss");
    }

}