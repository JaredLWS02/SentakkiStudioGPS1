    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Ambush : MonoBehaviour
    {
        public CameraScript cameraScript;
        public Spawn spawnScript; // Reference to your Spawn script.
        public GameObject goui;
        public GameObject lockbg;
        public GameObject normbg;
    public bool ambushstart;
        public int enemyamt;
        public bool stage1;
        private float tracker;

    public void Update()
    {
        // check whehter ambushstart is true or false
        // if true, use a for loop to check on whether if the gameobject inside the list u created inside spawn script is null or not, if null you use spawnScript.NAMEOFLIST.RemoveAt()
        // after that u then check again on whether if the list is empty, if true start the corountine EnemyDefeated()

        if (ambushstart == true)
        {
            for (int i = 0; i < spawnScript.enemyList.Count; i++)
            {
                if(spawnScript.enemyList[i] == null)
                {
                    tracker++;
                    spawnScript.enemyList.RemoveAt(i);
                }
            }
            if (tracker >= enemyamt)
            {
                normbg.SetActive(true);
                ambushstart = false;
                lockbg.GetComponent<SpriteRenderer>().sortingOrder = -5;
                LeanTween.moveLocalY(lockbg, -9.92f, 2f).setEaseInOutQuart().setOnComplete(EnemyDefeated);
                spawnScript.enemyList.Clear();
            }
        }



    }

    private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player entered the trigger.");

                if (cameraScript != null)
                {
                    lockbg.transform.position = Camera.main.transform.position;
                    normbg.SetActive(false);
                    tracker = 0;
                    cameraScript.StopFollowing();

                    Debug.Log("Camera stopped following.");
                    
                if(stage1)
                {
                    LeanTween.moveLocalY(lockbg, -0.12f, 1f).setEaseInElastic();
                    int i = Random.Range(0, 10);
                    switch(ProgressBar.instance.current)
                    {
                        case 12:
                            {
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
                        case 36f:
                            {
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
                        case 60f:
                            {
                                spawnScript.limitSpawn = 9;
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
                        case 85.5f:
                            {
                                spawnScript.SpawnEnemiesFromAbove(enemyamt);
                            }
                            break;

                    }
                }
                else
                {
                    LeanTween.moveLocalY(lockbg, -0.74f, 1f).setEaseOutElastic();
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


        public void EnemyDefeated()
        {
            tracker = 0;
            goui.SetActive(true);
            cameraScript.ResumeFollowing();
            if(ProgressBar.instance.current < 85.5f)
            {
                Spawn.instance.ResumeSpawning();

            }
            ambushstart = false;
            Invoke("disableUI", 2);
        }

    private void disableUI()
    {
        if(stage1)
        {
            lockbg.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }
        goui.SetActive(false);

    }
}
