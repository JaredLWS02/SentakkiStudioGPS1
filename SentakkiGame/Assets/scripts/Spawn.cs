using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public static Spawn instance;
    [SerializeField]
    private GameObject[] enemyPrefabs;
    [SerializeField]

    private GameObject AmbushEnemyPrefabs;

    private float spawnInterval = 3.5f;

    private Camera mainCamera;

    private bool isSpawningPaused = false;
    public GameObject player;
    public float enemycounter = 0;
    private bool isspawning;

    public void PauseSpawning()
    {
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
        if (!isSpawningPaused && enemycounter <= 3 && !isspawning)
        {
            StartCoroutine(SpawnEnemiesContinuously());
        }
    }

    private IEnumerator SpawnEnemiesContinuously()
    {
        isspawning = true;
        float spawnX = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect + 2.0f;
        
        Vector3 spawnPosition = new Vector3(spawnX, player.transform.position.y, 0);

        // Randomly select an enemy prefab from the array
        GameObject selectedEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        GameObject newEnemy = Instantiate(selectedEnemyPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("spawn");
        enemycounter += 1;

        yield return new WaitForSeconds(spawnInterval);
        isspawning = false;
    }

    // Add this method to your Spawn script to spawn a specific number of enemies.
    public void SpawnEnemiesFromAbove(int numberOfEnemies)
    {
        PauseSpawning();
        // Implement logic to spawn 'numberOfEnemies' from above.
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Calculate spawn positions from above.
            float spawnX = Random.Range(player.transform.position.x - 10, player.transform.position.x + 10); // Adjust as needed.
            float spawnY = 6f; // Spawn from above.
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

            // Randomly select an enemy prefab from the array
            GameObject selectedEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            GameObject newEnemy = Instantiate(selectedEnemyPrefab, spawnPosition, Quaternion.identity);

            // You may also need to adjust other properties of the spawned enemy.
        }
    }
}
