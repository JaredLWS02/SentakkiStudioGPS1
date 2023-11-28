using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public static Spawn instance;
    [SerializeField]
    public GameObject[] enemyPrefabs;
    [SerializeField]

    private float spawnInterval;
    public float limitSpawn;

    private Camera mainCamera;

    private bool isSpawningPaused = false;
    public GameObject player;
    public GameObject goui;
    public GameObject CameraParent;
    public int enemycounter = 0;
    private bool isspawning;
    public bool stage1spawn;
    public List<GameObject> enemyList = new List<GameObject>();


    public List<GameObject> enemyClone;
    private Vector2 spawnPosition;

    private float tracker;

    public List<float> campos;
    public List<GameObject> blockers;
    private int listCounter = 0;
    public void PauseSpawning()
    {
        enemycounter = 0;
        isSpawningPaused = true;
    }


    public void ResumeSpawning()
    {
        enemycounter = 0;
        isSpawningPaused = false;
    }

    void Start()
    {
        instance = this;
        CameraParent.GetComponent<CameraScript>().maxLimit = campos[listCounter];
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (stage1spawn)
        {
            if (!isSpawningPaused && enemycounter < limitSpawn && !isspawning)
            {
                StartCoroutine(SpawnEnemiesContinuously());
            }

            for (int l = 0; l < enemyClone.Count; l++)
            {
                if (enemyClone[l] == null)
                {
                    tracker++;
                    enemyClone.RemoveAt(l);
                }
            }

            if(tracker >= limitSpawn)
            {
                tracker = 0;
                blockers[listCounter].SetActive(false);
                listCounter += 1;
                CameraParent.GetComponent<CameraScript>().maxLimit = campos[listCounter];
                goui.SetActive(true);
                Invoke("disableUi", 2);
            }

        }

    }

    private IEnumerator SpawnEnemiesContinuously()
    {
        isspawning = true;
        float rightspawnX = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;
        float leftspawnX = mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect;

        int i = Random.Range(1,10);

        if (i > 5)
        {
            spawnPosition = new Vector3(rightspawnX, -1.82f, 0);
        }
        else
        {
            spawnPosition = new Vector3(leftspawnX, -1.82f, 0);
        }

        // Randomly select an enemy prefab from the array
        GameObject selectedEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        GameObject newEnemy = Instantiate(selectedEnemyPrefab, spawnPosition, Quaternion.identity);
        enemyClone.Add(newEnemy);
        Debug.Log("spawn");
        enemycounter += 1;

        yield return new WaitForSeconds(spawnInterval);
        isspawning = false;
    }

    // Add this method to your Spawn script to spawn a specific number of enemies.



    public void SpawnEnemiesFromAbove(int numberOfEnemies)
    {
        PauseSpawning();
        //foreach (GameObject enemy in enemyClone)
        //{
        //    if(enemy != null)
        //    {
        //        Destroy(enemy);
        //    }
        //}
        //enemyClone.Clear();

        // Implement logic to spawn 'numberOfEnemies' from above.
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Calculate spawn positions from above.
            float spawnX = Random.Range(player.transform.position.x - 7.5f, player.transform.position.x + 7.5f); // Adjust as needed.
            float spawnY = 8f; // Spawn from above.
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

            // Randomly select an enemy prefab from the array
            GameObject selectedEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            

            GameObject newEnemy = Instantiate(selectedEnemyPrefab, spawnPosition, Quaternion.identity);
            enemyList.Add(newEnemy);
            newEnemy.GetComponent<Animator>().Play("EnemyAmbush", 0, 0);

            //declare a list above 
            //add enemies spawn in instantiate into the list
            //refer to line 61 on how to do it

        }
    }

    public IEnumerator SpawnEnemiesFromAboveSlow(int numberOfEnemies)
    {
        PauseSpawning();

        int t = 0;
        while(t< numberOfEnemies)
        {
            for(int y = 0; y < 4; y++ )
            {
                float rightspawnX = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;
                float leftspawnX = mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect;
                int i = Random.Range(1, 10);

                if (i > 5)
                {
                    float spawnY = 7f; // Spawn from above.
                    int x = Random.Range(1, 10);
                    if (x > 5)
                    {
                        spawnPosition = new Vector2(rightspawnX, spawnY);
                    }
                    else
                    {
                        spawnPosition = new Vector2(leftspawnX, spawnY);
                    }
                }
                else
                {
                    float spawnY = -1.82f;
                    int x = Random.Range(1, 10);
                    if (x > 5)
                    {
                        spawnPosition = new Vector2(rightspawnX, spawnY);
                    }
                    else
                    {
                        spawnPosition = new Vector2(leftspawnX, spawnY);
                    }
                }


                // Randomly select an enemy prefab from the array
                GameObject selectedEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

                GameObject newEnemy = Instantiate(selectedEnemyPrefab, spawnPosition, Quaternion.identity);
                enemyList.Add(newEnemy);
                t++;
                if (i > 5)
                {
                    newEnemy.GetComponent<Animator>().Play("EnemyAmbush", 0, 0);
                }
            }
            yield return new WaitForSeconds(spawnInterval * 3);
        }

    }

    public void disableUi()
    { 
            goui.SetActive(false);
    }
}
