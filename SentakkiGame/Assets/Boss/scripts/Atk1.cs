using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atk1 : MonoBehaviour
{
    [SerializeField] private GameObject atk3;
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
        yield return new WaitForSecondsRealtime(3);//3
        atk1Warning[0].SetActive(false);
        atk1[0].SetActive(true);

        yield return new WaitForSecondsRealtime(1);//4
        atk1Warning[1].SetActive(true);
        atk1Warning[2].SetActive(true);

        yield return new WaitForSecondsRealtime(3);//7
        atk1[0].SetActive(false);

        yield return new WaitForSecondsRealtime(1);//8
        atk1Warning[1].SetActive(false);
        atk1Warning[2].SetActive(false);
        atk1[1].SetActive(true);
        atk1[2].SetActive(true);

        yield return new WaitForSecondsRealtime(1);//9
        atk1Warning[3].SetActive(true);
        atk1Warning[4].SetActive(true);

        yield return new WaitForSecondsRealtime(3);//12
        atk1[1].SetActive(false);
        atk1[2].SetActive(false);

        yield return new WaitForSecondsRealtime(1);//13
        atk1Warning[3].SetActive(false);
        atk1Warning[4].SetActive(false);
        atk1[3].SetActive(true);
        atk1[4].SetActive(true);

        yield return new WaitForSecondsRealtime(1);//14
        atk1Warning[1].SetActive(true);
        atk1Warning[2].SetActive(true);

        yield return new WaitForSecondsRealtime(3);//17
        atk1[3].SetActive(false);
        atk1[4].SetActive(false);

        yield return new WaitForSecondsRealtime(1);//18
        atk1Warning[1].SetActive(false);
        atk1Warning[2].SetActive(false);
        atk1[1].SetActive(true);
        atk1[2].SetActive(true);

        yield return new WaitForSecondsRealtime(1);//19
        atk1Warning[0].SetActive(true);

        yield return new WaitForSecondsRealtime(3);//22
        atk1[1].SetActive(false);
        atk1[2].SetActive(false);

        yield return new WaitForSecondsRealtime(1);//23
        atk1Warning[0].SetActive(false);
        atk1[0].SetActive(true);

        yield return new WaitForSecondsRealtime(3);//26
        atk1[0].SetActive(false);

        yield return new WaitForSecondsRealtime(5);
        atk3.SetActive(true);
        yield return new WaitForSecondsRealtime(15);
        atk3.SetActive(false);
        col2d.enabled = true;
    }
}
