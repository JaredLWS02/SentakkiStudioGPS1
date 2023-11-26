    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Ambush : MonoBehaviour
    {
        private CameraScript cameraScript;
        public Spawn spawnScript; // Reference to your Spawn script.
        public GameObject goui;
        public bool ambushstart;
        public int enemyamt;
        public bool stage1;

    public void Update()
    {
        // check whehter ambushstart is true or false
        // if true, use a for loop to check on whether if the gameobject inside the list u created inside spawn script is null or not, if null you use spawnScript.NAMEOFLIST.RemoveAt()
        // after that u then check again on whether if the list is empty, if true start the corountine EnemyDefeated()

        if (ambushstart == true)
        {
            for (int i = spawnScript.enemyList.Count - 1; i >=0; i--)
            {
                if(spawnScript.enemyList[i] == null)
                {
                    spawnScript.enemyList.RemoveAt(i);
                }
            }
            if (spawnScript.enemyList.Count == 0)
            {
                StartCoroutine(EnemyDefeated());
            }
        }



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
                    
                if(stage1)
                {
                    int i = Random.Range(0, 10);
                    switch(ProgressBar.instance.current)
                    {
                        case 12:
                            {
                                spawnScript.limitSpawn = 7;
                                if (i > 8)
                                {
                                    spawnScript.SpawnEnemiesFromAbove(enemyamt);
                                }
                                else
                                {
                                    StartCoroutine(spawnScript.SpawnEnemiesFromAboveSlow(enemyamt));
                                }

                            }
                            break;
                        case 38:
                            {
                                spawnScript.limitSpawn = 6;
                                if (i > 6)
                                {
                                    spawnScript.SpawnEnemiesFromAbove(enemyamt);
                                }
                                else
                                {
                                    StartCoroutine(spawnScript.SpawnEnemiesFromAboveSlow(enemyamt));
                                }

                            }
                            break;
                        case 62:
                            {
                                spawnScript.limitSpawn = 7;
                                if (i > 5)
                                {
                                    spawnScript.SpawnEnemiesFromAbove(enemyamt);
                                }
                                else
                                {
                                    StartCoroutine(spawnScript.SpawnEnemiesFromAboveSlow(enemyamt));
                                }

                            }
                            break;
                        case 88:
                            {
                                spawnScript.SpawnEnemiesFromAbove(enemyamt);
                            }
                            break;

                    }
                }
                else
                {
                    spawnScript.SpawnEnemiesFromAbove(enemyamt);
                }
                    // Spawn 6 enemies using the Spawn script.

                    Debug.Log("Ambush event triggered.");

                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    ambushstart = true;

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
        if(ProgressBar.instance.current < 88)
        {
            Spawn.instance.ResumeSpawning();

        }
            ambushstart = false;
            yield return new WaitForSeconds(1f);
            goui.SetActive(false);

        }
    }
