using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawn : MonoBehaviour
{
    public GameObject enemy; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Instantiate(enemy,new Vector2(transform.position.x + 6, transform.position.y), Quaternion.identity);
            GetComponent<BoxCollider2D>().enabled = false;

        }
    }
}
