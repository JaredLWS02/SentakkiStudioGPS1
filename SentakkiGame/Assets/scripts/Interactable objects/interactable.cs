using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactable : MonoBehaviour
{
    private bool caninteract;
    [SerializeField] private GameObject prompttext;
    
    // Start is called before the first frame update
    void Start()
    {
        prompttext.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && caninteract)
        {
            Debug.Log("break");
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            prompttext.SetActive(true);
            caninteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            prompttext.SetActive(false);
            caninteract = false;
        }
    }
}
