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
    public GameObject goui;
    public GameObject lockbg;
    public GameObject normbg;
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
            normbg.SetActive(true);
            LeanTween.moveLocalY(lockbg, -9.92f, 1f).setEaseInBack().setOnComplete(popui);
            clone.Clear();
            spawned = false;
            tracker = 0;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            lockbg.transform.position = Camera.main.transform.position;
            LeanTween.moveLocalY(lockbg, -0.74f, 1.5f).setEaseOutBack();
            normbg.SetActive(false);
            CameraScript.instance.StopFollowing();
            StartCoroutine(Startspawn());
            spawned = true;
            GetComponent<BoxCollider2D>().enabled = false;

        }
    }
    private void popui()
    {
        CameraScript.instance.ResumeFollowing();
        goui.SetActive(true);
        Invoke("disableGoUi", 2);
    }

    private void disableGoUi()
    {
        goui.SetActive(false);
    }

    public IEnumerator Startspawn()
    {
        for (int i = 0; i < enemy.Count; i++)
        {
            int a = Random.Range(1, 10);
            if(a > 5)
            {
                float posx = transform.position.x + 6;
                GameObject c = Instantiate(enemy[i], new Vector2(posx, -1.83f), Quaternion.identity);
                clone.Add(c);
            }
            else
            {
                float posx = transform.position.x - 10;
                GameObject c = Instantiate(enemy[i], new Vector2(posx, -1.83f), Quaternion.identity);
                clone.Add(c);
            }
            yield return new WaitForSeconds(Random.Range(2,3));
        }
    }
}
