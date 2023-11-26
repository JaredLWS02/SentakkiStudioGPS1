using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class extiScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPrefs.SetFloat("hpP1", healthPoint.Instance.currenthealthAmountP1);
        PlayerPrefs.SetFloat("hpP2", healthPoint.Instance.currenthealthAmountP2);
        PlayerPrefs.SetFloat("Gauge", GaugePoint.Instance.gaugePointAmount);
        PlayerPrefs.Save();
        SceneManager.LoadSceneAsync(3);
    }
}
