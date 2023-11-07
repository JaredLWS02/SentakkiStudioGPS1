using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Atk5 : MonoBehaviour
{
    [SerializeField] private BoxCollider2D col2d;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject origin;
    [SerializeField] private GameObject origin2;
    private Vector2 spawnPoint5;
    private Vector2 spawnPoint6;

    void Start()
    {
        col2d.enabled = true;
        spawnPoint5 = origin.transform.position;
        spawnPoint6 = origin2.transform.position;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col2d.enabled = false;
            if (col2d.enabled == false)
            {
                Debug.Log("Atk2");
                StartCoroutine(atk5());
            }
            else
            {
                col2d.enabled = false;
            }
        }
    }

    private IEnumerator atk5()
    {
        yield return new WaitForSecondsRealtime(45);
        origin.SetActive(true);
        origin2.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        for (int i = 0; i < 5; i++)
        {
            Instantiate(bullet, spawnPoint5, Quaternion.identity);
            Instantiate(bullet, spawnPoint6, Quaternion.identity);
            yield return new WaitForSecondsRealtime(0.8f);
        }
        origin.SetActive(false);
        origin2.SetActive(false);
        yield return new WaitForSecondsRealtime(3);
        col2d.enabled = true;
    }
}
