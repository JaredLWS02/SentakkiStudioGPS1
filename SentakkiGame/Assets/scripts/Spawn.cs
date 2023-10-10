using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float spawnInterval = 3.5f;

    private Camera mainCamera;

       private bool isSpawningPaused = false;

    public void PauseSpawning()
    {
        isSpawningPaused = true;
    }

    public void ResumeSpawning()
    {
        isSpawningPaused = false;
    }

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnEnemiesContinuously());
    }

    private IEnumerator SpawnEnemiesContinuously()
    {
        while (true)
        {
            float spawnX = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect + 2.0f;

            Vector3 spawnPosition = new Vector3(spawnX, Random.Range(-6f, 6f), 0);
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            StartCoroutine(MoveEnemyOntoScreen(newEnemy));

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private IEnumerator MoveEnemyOntoScreen(GameObject enemy)
    {
        float moveSpeed = 5.0f;
        Vector3 targetPosition = Vector3.zero;
        while (enemy.transform.position != targetPosition)
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    // Add this method to your Spawn script to spawn a specific number of enemies.
public void SpawnEnemiesFromAbove(int numberOfEnemies)
{
    // Implement logic to spawn 'numberOfEnemies' from above.
    for (int i = 0; i < numberOfEnemies; i++)
    {
        // Calculate spawn positions from above.
        float spawnX = Random.Range(-5f, 5f); // Adjust as needed.
        float spawnY = 8f; // Spawn from above.
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        
        // You may also need to adjust other properties of the spawned enemy.
    }
}
}


