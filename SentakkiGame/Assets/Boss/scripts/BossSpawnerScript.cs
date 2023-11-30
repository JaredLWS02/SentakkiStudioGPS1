using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class BossSpawnerScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rend;
    [SerializeField] private GameObject boss;
    [SerializeField] private BoxCollider2D c2d;
    [SerializeField] private GameObject prompttext;
    [SerializeField] private GameObject SpawnAnim;
    [SerializeField] private GameObject Cam;
    [SerializeField] private AudioSource stageBossMusic;
    [SerializeField] private AudioSource stageMusic;
    [SerializeField] private AudioSource p1Music;
    [SerializeField] private AudioSource p2Music;
    [SerializeField] private GameObject lockwall;
    [SerializeField] private GameObject middleground;
    private bool caninteract;
    private bool opened;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && caninteract && !opened)
        {
            rend.enabled = false;
            c2d.enabled = false;
            StartCoroutine(bossSpawn());
            p1Music.Stop();
            p2Music.Stop();
            stageMusic.Stop();
            stageBossMusic.Play();
            opened = true;
            Cam.transform.position = new Vector3(195, 0, 0);
            CameraScript.instance.StopFollowing();
            middleground.SetActive(false);
            lockwall.transform.position = Camera.main.transform.position;
            LeanTween.moveLocalY(lockwall, -0.12f, 1.5f).setEaseOutBack();
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
        SpawnAnim.SetActive(true);
        //Appear with animations or fade in
        yield return new WaitForSeconds(3);
        SpawnAnim.SetActive(false);
        boss.SetActive(true);
    }
}
