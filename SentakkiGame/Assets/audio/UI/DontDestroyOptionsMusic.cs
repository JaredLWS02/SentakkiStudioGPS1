using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOptionsMusic : MonoBehaviour
{
    public static DontDestroyOptionsMusic Instance;
    public GameObject mainMenuMusic;
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
