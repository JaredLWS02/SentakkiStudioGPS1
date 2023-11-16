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
        GetCurrentFill();
        if (current >= maximum)
        {
            Invoke("MaxProgressBar",2.5f);
        }
    }

    void GetCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        fillAmount = currentOffset / maximumOffset;
        mask.fillAmount = Mathf.Lerp(mask.fillAmount,fillAmount,Time.deltaTime);

        //switch(mask.fillAmount)
        //{
        //    case 0.2f:
        //        {
        //            stage.volume = 0.25f;
        //            swapmechanic.instance.volume = 0.1f;
        //            swapmechanic.instance.setVolume();
        //        }
        //        break;
        //    case 0.4f:
        //        {
        //            stage.volume = 0.2f;
        //            swapmechanic.instance.volume = 0.1f;
        //            swapmechanic.instance.setVolume();
        //        }
        //        break;
        //    case 0.6f:
        //        {
        //            stage.volume = 0.1f;
        //            swapmechanic.instance.volume = 0.2f;
        //            swapmechanic.instance.setVolume();

        //        }
        //        break;
        //    case 0.8f:
        //        {
        //            stage.volume = 0.05f;
        //            swapmechanic.instance.volume = 0.25f;
        //            swapmechanic.instance.setVolume();
        //        }
        //        break;
        //    case 1.0f:
        //        {
        //            stage.volume = 0;
        //            swapmechanic.instance.volume = 0.25f;
        //            swapmechanic.instance.setVolume();
        //        }
        //        break;
        //}

    }

    void MaxProgressBar()
    {
            spawnScript.PauseSpawning();
            CameraScript.StopFollowing();
            SceneManager.LoadSceneAsync(3);
            Debug.Log ("Spawn Boss");
    }

}