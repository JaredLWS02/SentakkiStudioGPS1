using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atk2 : MonoBehaviour
{
    private GameObject target;
    [SerializeField] private BoxCollider2D col2d;
    [SerializeField] private GameObject atk2;
    [SerializeField] private float thrust;
    [SerializeField] private float hOffset;
    private Vector2 spawnLocation;
    private Rigidbody2D rb;
    [SerializeField] private List<AudioSource> atkSfx;
    [SerializeField] private List<AudioSource> noteSfx;
    // Start is called before the first frame update
    void Start()
    {
        rb = atk2.GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spawnLocation = target.transform.position;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col2d.enabled = false;
            if (col2d.enabled == false)
            {
                Debug.Log("Atk2");
                StartCoroutine(TrackAtk());
            }
            else
            {
                col2d.enabled = false;
            }
        }
    }

    private IEnumerator TrackAtk()
    {
        yield return new WaitForSeconds(2);
        int i = Random.Range(0, atkSfx.Count);
        noteSfx[i].Play();
        Instantiate(atk2, new Vector3(spawnLocation.x, spawnLocation.y + hOffset, 0.0f), Quaternion.identity);
            atk2.SetActive(true);
            yield return new WaitForSeconds(3);
            rb.AddForce(transform.up * thrust, ForceMode2D.Impulse);
            col2d.enabled = true;
    }
}
