using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atk1 : MonoBehaviour
{
    [SerializeField] private BoxCollider2D col2d;
    [SerializeField] private GameObject[] atk1Warning;
    [SerializeField] private GameObject[] atk1;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col2d.enabled = false;
            if (col2d.enabled == false)
            {
                Debug.Log("Sensor Triggered");
                StartCoroutine(AOE1());
                atk1Warning[0].SetActive(true);
            }
            else
            {
                col2d.enabled = false; 
            }
        }
    }

    private IEnumerator AOE1()
    {
        yield return new WaitForSecondsRealtime(3);
        atk1Warning[0].SetActive(false);
        atk1[0].SetActive(true);

        yield return new WaitForSecondsRealtime(1);
        atk1Warning[1].SetActive(true);
        atk1Warning[2].SetActive(true);

        yield return new WaitForSecondsRealtime(3);
        atk1Warning[1].SetActive(false);
        atk1Warning[2].SetActive(false);
        atk1[0].SetActive(false);

        yield return new WaitForSecondsRealtime(1);
        atk1[1].SetActive(true);
        atk1[2].SetActive(true);

        yield return new WaitForSecondsRealtime(1);
        atk1Warning[3].SetActive(true);
        atk1Warning[4].SetActive(true);

        yield return new WaitForSecondsRealtime(3);
        atk1Warning[3].SetActive(false);
        atk1Warning[4].SetActive(false);
        atk1[1].SetActive(false);
        atk1[2].SetActive(false);

        yield return new WaitForSecondsRealtime(1);
        atk1[3].SetActive(true);
        atk1[4].SetActive(true);

        yield return new WaitForSecondsRealtime(3);
        atk1[3].SetActive(false);
        atk1[4].SetActive(false);

        yield return new WaitForSecondsRealtime(15);
        col2d.enabled = true;
    }
}
