    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Ambush : MonoBehaviour
    {
        private CameraScript cameraScript;
        public GameObject enemyPrefab;
        public Spawn spawnScript; // Reference to your Spawn script.
        public GameObject goui;
        public bool ambushstart;
        
    public void Update()
    {
        // check whehter ambushstart is true or false
        // if true, use a for loop to check on whether if the gameobject inside the list u created inside spawn script is null or not, if null you use spawnScript.NAMEOFLIST.RemoveAt()
        // after that u then check again on whether if the list is empty, if true start the corountine EnemyDefeated()

    }
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
                    spawnScript.SpawnEnemiesFromAbove(5);

                    Debug.Log("Ambush event triggered.");

                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    ambushstart = true;
                    spawnScript.enemycounter = 0;
                }
                else
                {
                    Debug.LogWarning("CameraScript not found on the main camera.");
                }
            }
        }


        public IEnumerator EnemyDefeated()
        {
            goui.SetActive(true);
            cameraScript.ResumeFollowing();
            Spawn.instance.ResumeSpawning();
            ambushstart = false;
            yield return new WaitForSecondsRealtime(1f);
            goui.SetActive(false);

        }
    }
