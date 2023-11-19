using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TutorialSpawn : MonoBehaviour
{
    public GameObject enemy;
    public List<GameObject> clone;
    private bool spawned = false;
    private void Update()
    {
        if(spawned)
        {
            for(int i = 0; i < clone.Count; i++)
            {
                if (clone[i] == null)
                {
                    clone.Clear();
                    removeBarrier.instance.count++;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameObject c= Instantiate(enemy,new Vector2(transform.position.x + 6, transform.position.y), Quaternion.identity);
            clone.Add(c);
            spawned = true;
            GetComponent<BoxCollider2D>().enabled = false;

        }
    }
}
