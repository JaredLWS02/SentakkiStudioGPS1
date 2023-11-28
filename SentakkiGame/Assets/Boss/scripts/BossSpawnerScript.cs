using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnerScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rend;
    [SerializeField] private GameObject boss;
    [SerializeField] private BoxCollider2D c2d;
    [SerializeField] private GameObject prompttext;
    private bool caninteract;
    private bool opened;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && caninteract && !opened)
        {
            rend.enabled = false;
            c2d.enabled = false;
            StartCoroutine(bossSpawn());
            opened = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.CompareTag("BossSpawner"))
        {
            prompttext.SetActive(true);
            caninteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.CompareTag("BossSpawner"))
        {
            prompttext.SetActive(false);
            caninteract = false;
        }
    }

    private IEnumerator bossSpawn()
    {
        //Appear with animations or fade in
        yield return new WaitForSecondsRealtime(3);
        boss.SetActive(true);
    }
}
