using System.Collections;
using System.Collections.Generic;
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
    public int enemycounter = 0;
    private bool isspawning;
    public bool stage1spawn;
    public List<GameObject> enemyList = new List<GameObject>();


    public List<GameObject> enemyClone;
    private Vector2 spawnPosition;
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
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if(stage1spawn)
        {
            if (!isSpawningPaused && enemycounter < limitSpawn && !isspawning)
            {
                StartCoroutine(SpawnEnemiesContinuously());
            }

        }

    }

    private IEnumerator SpawnEnemiesContinuously()
    {
        isspawning = true;
        float rightspawnX = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect + 1.3f;
        float leftspawnX = mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect - 1.3f;

        int i = Random.Range(0, 2);

        if (i == 0)
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

        for (int l = 0; l < enemyClone.Count; l++)
        {
            if (enemyClone[l] == null)
            {
                enemyClone.RemoveAt(l);
            }
        }

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
            float spawnX = Random.Range(player.transform.position.x - 8, player.transform.position.x + 8); // Adjust as needed.
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

        while(enemycounter < numberOfEnemies)
        {
        float spawnX = Random.Range(player.transform.position.x - 8, player.transform.position.x + 8); // Adjust as needed.
        float spawnY = 7f; // Spawn from above.

        Vector2 spawnPosition = new Vector2(spawnX, spawnY);
        
        // Randomly select an enemy prefab from the array
        GameObject selectedEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        GameObject newEnemy = Instantiate(selectedEnemyPrefab, spawnPosition, Quaternion.identity);
        enemyList.Add(newEnemy);
        enemycounter += 1;
        newEnemy.GetComponent<Animator>().Play("EnemyAmbush", 0, 0);
            yield return new WaitForSeconds(spawnInterval);
        }

    }
}
