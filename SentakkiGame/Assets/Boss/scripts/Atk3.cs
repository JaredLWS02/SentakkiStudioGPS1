using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Atk3 : MonoBehaviour
{
    [SerializeField] private BoxCollider2D col2d;
    [SerializeField] private GameObject smash;
    [SerializeField] private GameObject atk3;
    [SerializeField] private GameObject atk4;
    [SerializeField] private float speed;
    [SerializeField] private Transform attackPoint3;
    [SerializeField] private SpriteRenderer rend1;
    [SerializeField] private SpriteRenderer rend2;
    [SerializeField] private SpriteRenderer rend3;
    private Vector2 spawnLocation3;
    [SerializeField] private List<AudioSource> atkSfx;

    // Start is called before the first frame update
    void Start()
    {
        spawnLocation3 = attackPoint3.position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col2d.enabled = false;
            if (col2d.enabled == false)
            {
                Debug.Log("Atk3");
                int i = Random.Range(0, atkSfx.Count);
                atkSfx[i].Play();
                StartCoroutine(ShockAtk());

            }
            else
            {
                col2d.enabled = false;
            }
        }
    }

    private IEnumerator ShockAtk()
    {
        yield return new WaitForSeconds(3);//3
        rend1.enabled = false;
        rend2.enabled = false;
        rend3.enabled = false;
        smash.SetActive(true);
        yield return new WaitForSeconds(0.9f);//3.2
        smash.SetActive(false);
        Instantiate(atk3, new Vector3(spawnLocation3.x - 1.5f, spawnLocation3.y-1 , 0.0f), Quaternion.identity);
        Instantiate(atk4, new Vector3(spawnLocation3.x + 1.5f, spawnLocation3.y -1, 0.0f), Quaternion.identity);
        rend1.enabled = true;
        rend2.enabled = true;
        rend3.enabled = true;
        yield return new WaitForSeconds(3);//6.2
        rend1.enabled = false;
        rend2.enabled = false;
        rend3.enabled = false;
        smash.SetActive(true);
        yield return new WaitForSeconds(0.9f);//6.4
        smash.SetActive(false);
        Instantiate(atk3, new Vector3(spawnLocation3.x - 1.5f, spawnLocation3.y-1 , 0.0f), Quaternion.identity);
        Instantiate(atk4, new Vector3(spawnLocation3.x + 1.5f, spawnLocation3.y-1 , 0.0f), Quaternion.identity);
        rend1.enabled = true;
        rend2.enabled = true;
        rend3.enabled = true;
        yield return new WaitForSeconds (3);//9.4
        rend1.enabled = false;
        rend2.enabled = false;
        rend3.enabled = false;
        smash.SetActive(true);
        yield return new WaitForSeconds(0.9f);//9.6
        smash.SetActive(false);
        Instantiate(atk3, new Vector3(spawnLocation3.x - 1.5f, spawnLocation3.y-1 , 0.0f), Quaternion.identity);
        Instantiate(atk4, new Vector3(spawnLocation3.x + 1.5f, spawnLocation3.y -1, 0.0f), Quaternion.identity);
        rend1.enabled = true;
        rend2.enabled = true;
        rend3.enabled = true;
        yield return new WaitForSeconds(3);//12.6
        col2d.enabled = true;
    }
}
