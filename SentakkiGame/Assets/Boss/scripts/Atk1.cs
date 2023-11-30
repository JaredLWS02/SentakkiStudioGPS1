using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atk1 : MonoBehaviour
{
    [SerializeField] private GameObject atk3;
    [SerializeField] private GameObject atk2;
    [SerializeField] private BoxCollider2D col2d;
    [SerializeField] private GameObject[] atk1Warning;
    [SerializeField] private GameObject[] atk1;
    [SerializeField] private GameObject summon1;
    [SerializeField] private GameObject summon2;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Vector3 pos1;
    [SerializeField] private GameObject enemySpawnAnim;
    [SerializeField] private List<AudioSource> atkSfx;
    [SerializeField] private AudioSource warningsfx;
    [SerializeField] private AudioSource lightingsfx;

    void Awake()
    {
        pos1 = spawnPoint.position;
    }

    void spawnAnim()
    {

        Instantiate(enemySpawnAnim, new Vector3(pos1.x - 5.0f, pos1.y, pos1.z), Quaternion.identity);
        Instantiate(enemySpawnAnim, new Vector3(pos1.x + 5.0f, pos1.y, pos1.z), Quaternion.identity);
    }

    void summon()//atk 4
    {
        int i = Random.Range(0, atkSfx.Count);
        atkSfx[i].Play();
        Instantiate(summon1, new Vector3(pos1.x - 5.0f, pos1.y, pos1.z), Quaternion.identity);
        Instantiate(summon2, new Vector3(pos1.x + 5.0f, pos1.y, pos1.z), Quaternion.identity);
        summon1.SetActive(true);
        summon2.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            atk2.SetActive(true);
            col2d.enabled = false;
            if (col2d.enabled == false)
            {
                Debug.Log("Sensor Triggered");
                int i = Random.Range(0, atkSfx.Count);
                atkSfx[i].Play();
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
        yield return new WaitForSeconds(1);//3
        warningsfx.Play();
        yield return new WaitForSeconds(3);//3
        lightingsfx.Play();
        atk1Warning[0].SetActive(false);
        atk1[0].SetActive(true);

        yield return new WaitForSeconds(1);//4
        warningsfx.Play();

        atk1Warning[1].SetActive(true);
        atk1Warning[2].SetActive(true);

        yield return new WaitForSeconds(3);//7
        atk1[0].SetActive(false);

        yield return new WaitForSeconds(1);//8
        lightingsfx.Play();

        atk1Warning[1].SetActive(false);
        atk1Warning[2].SetActive(false);
        atk1[1].SetActive(true);
        atk1[2].SetActive(true);

        yield return new WaitForSeconds(1);//9

        warningsfx.Play();
        atk1Warning[3].SetActive(true);
        atk1Warning[4].SetActive(true);

        yield return new WaitForSeconds(3);//12

        atk1[1].SetActive(false);
        atk1[2].SetActive(false);

        yield return new WaitForSeconds(1);//13
        lightingsfx.Play();

        atk1Warning[3].SetActive(false);
        atk1Warning[4].SetActive(false);
        atk1[3].SetActive(true);
        atk1[4].SetActive(true);

        yield return new WaitForSeconds(1);//14

        warningsfx.Play();
        atk1Warning[1].SetActive(true);
        atk1Warning[2].SetActive(true);

        yield return new WaitForSeconds(3);//17
        atk1[3].SetActive(false);
        atk1[4].SetActive(false);

        yield return new WaitForSeconds(1);//18
        lightingsfx.Play();

        atk1Warning[1].SetActive(false);
        atk1Warning[2].SetActive(false);
        atk1[1].SetActive(true);
        atk1[2].SetActive(true);

        yield return new WaitForSeconds(1);//19
        warningsfx.Play();
        atk1Warning[0].SetActive(true);

        yield return new WaitForSeconds(3);//22
        atk1[1].SetActive(false);
        atk1[2].SetActive(false);

        yield return new WaitForSeconds(1);//23
        lightingsfx.Play();

        atk1Warning[0].SetActive(false);
        atk1[0].SetActive(true);

        yield return new WaitForSeconds(3);//26
        atk1[0].SetActive(false);

        yield return new WaitForSeconds(1);//27
        atk3.SetActive(true);
        yield return new WaitForSeconds(15);//42
        atk3.SetActive(false);
        spawnAnim();
        yield return new WaitForSeconds(1);//43
        summon();
        yield return new WaitForSeconds(10);//53
        col2d.enabled = true;
    }
}
