using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spawnInteractables : MonoBehaviour
{
    public List<GameObject> interactables;
    private void OnDestroy()
    {
        int i = Random.Range(1, 10);
        if(i > 5)
        {
            Instantiate(interactables[Random.Range(0, interactables.Count)], new Vector2(gameObject.transform.position.x,-2.37f), Quaternion.identity);
        }
    }

}
