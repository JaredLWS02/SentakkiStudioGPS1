using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TutorialSpawn : MonoBehaviour
{
    public List<GameObject> enemy;
    public List<GameObject> clone;
    private bool spawned = false;
    private float tracker = 0;
    private void Update()
    {
        if (spawned)
        {
            for (int i = 0; i < clone.Count; i++)
            {
                if (clone[i] == null)
                {
                    tracker++;
                    clone.RemoveAt(i);
                    //removeBarrier.instance.count++;
                }
            }
        }
        if(tracker >= enemy.Count)
        {
            CameraScript.instance.ResumeFollowing();
            clone.Clear();
            spawned = false;
            tracker = 0;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            CameraScript.instance.StopFollowing();
            for(int i = 0; i<enemy.Count; i++)
            {
                GameObject c = Instantiate(enemy[i],new Vector2(transform.position.x + 6, transform.position.y), Quaternion.identity);
                clone.Add(c);
            }
            spawned = true;
            GetComponent<BoxCollider2D>().enabled = false;

        }
    }
}
