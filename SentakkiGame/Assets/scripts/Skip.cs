using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skip : MonoBehaviour
{
    public Button buttonPrefab; // Assign the Button prefab in the Inspector
    public Vector3 spawnPosition; // Define a Vector3 variable to set the spawn position in the Inspector

    void Start()
    {
        StartCoroutine(SpawnButtonAfterDelay(2f)); // Start the coroutine to spawn the button after 2 seconds
    }

    IEnumerator SpawnButtonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay

        // Instantiate the button prefab at the specified position
        Button newButton = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity);

        // Set the parent of the instantiated button to the Canvas (so it's visible)
        newButton.transform.SetParent(GameObject.Find("Canvas").transform, false);

        // Set the position of the button to the desired spawn position
        newButton.transform.position = spawnPosition;

        // Optionally, you can set other properties of the button here, such as onClick events or text
        newButton.onClick.AddListener(ButtonClicked);
    }

    void ButtonClicked()
    {
        Debug.Log("Button clicked!");
    }
}
