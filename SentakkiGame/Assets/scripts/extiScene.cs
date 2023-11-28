using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class extiScene : MonoBehaviour
{
    [SerializeField] private GameObject prompttext;
    private bool caninteract;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && caninteract)
        {
            PlayerPrefs.SetFloat("hpP1", healthPoint.Instance.currenthealthAmountP1);
            PlayerPrefs.SetFloat("hpP2", healthPoint.Instance.currenthealthAmountP2);
            PlayerPrefs.SetFloat("Gauge", GaugePoint.Instance.gaugePointAmount);
            PlayerPrefs.Save();
            SceneManager.LoadSceneAsync(4);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.CompareTag("speaker"))
        {
            prompttext.SetActive(true);
            caninteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.CompareTag("speaker"))
        {
            prompttext.SetActive(false);
            caninteract = false;
        }
    }
}
