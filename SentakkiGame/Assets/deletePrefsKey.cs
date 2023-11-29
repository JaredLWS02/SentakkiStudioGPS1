using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deletePrefsKey : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = 1;
        if(PlayerPrefs.HasKey("hpP1") && PlayerPrefs.HasKey("hpP2") && PlayerPrefs.HasKey("Gauge"))
        {
            PlayerPrefs.DeleteKey("hpP1");
            PlayerPrefs.DeleteKey("hpP2");
            PlayerPrefs.DeleteKey("Gauge");
            PlayerPrefs.DeleteKey("enemieskilled");
        }
    }

}
