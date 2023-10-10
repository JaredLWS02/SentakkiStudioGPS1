using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambush : MonoBehaviour
{
    private CameraScript cameraScript;
    public GameObject enemyPrefab;
    public Spawn spawnScript; // Reference to your Spawn script.
    private int defeatedEnemies = 0;
    public GameObject goui;
    public bool ambushstart;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger.");
            cameraScript = Camera.main.GetComponent<CameraScript>();

            if (cameraScript != null)
            {
                cameraScript.StopFollowing();

                Debug.Log("Camera stopped following.");

                // Spawn 6 enemies using the Spawn script.
                spawnScript.SpawnEnemiesFromAbove(6);

                Debug.Log("Ambush event triggered.");

                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                Debug.LogWarning("CameraScript not found on the main camera.");
            }
        }
    }


    public void EnemyDefeated()
    {
        defeatedEnemies++;

        if (defeatedEnemies >= 6)
        {
            goui.SetActive(true);
            cameraScript.ResumeFollowing(); // Update this line to match your CameraScript method.
            Spawn.instance.ResumeSpawning();
        }
    }
}
