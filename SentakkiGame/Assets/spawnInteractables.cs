using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spawnInteractables : MonoBehaviour
{
    public List<GameObject> interactables;
    private float spawnX;
    private float negSpawnX;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawn",10,30);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void spawn()
    {
        spawnX = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect - 1;
        negSpawnX = Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect - 1;
        GameObject c = Instantiate(interactables[Random.Range(0, interactables.Count)], new Vector2(Random.Range(negSpawnX ,spawnX) ,-2.4f),Quaternion.identity);
        StartCoroutine(clear(c));
    }

    private IEnumerator clear(GameObject a)
    {
        yield return new WaitForSeconds(20);
        Destroy(a);
    }

}
