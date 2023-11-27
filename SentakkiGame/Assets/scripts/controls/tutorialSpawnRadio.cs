using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialSpawnRadio : MonoBehaviour
{
    public List<GameObject> Radio;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            foreach(GameObject go in Radio) 
            {
                go.SetActive(true);
            }
        }
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
