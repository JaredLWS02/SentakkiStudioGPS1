using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skip : MonoBehaviour
{
    public Button buttonPrefab;
    public Vector3 spawnPosition;

    void Start()
    {
        StartCoroutine(SpawnButtonAfterDelay(2f));
    }

    IEnumerator SpawnButtonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Button newButton = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity);

        newButton.transform.SetParent(GameObject.Find("Canvas").transform, false);

        newButton.transform.position = spawnPosition;

        newButton.onClick.AddListener(ButtonClicked);
    }

    void ButtonClicked()
    {
        Debug.Log("Button clicked!");
    }
}
