using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterimage : MonoBehaviour
{
    public movement movement;
    public float timeBtwspawns;
    public float startimeBtwSpawns;

    public GameObject echo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(movement.moveKeyPress)
        {
            if (timeBtwspawns <= 0)
            {
                if (transform.localScale.x >= 1)
                {
                    var clone = Instantiate(echo, transform.position, Quaternion.identity);
                    Destroy(clone, 3);
                }
                else
                {
                    var clone = Instantiate(echo, transform.position, Quaternion.Euler(0, 180, 0));
                    Destroy(clone, 3);
                }

                timeBtwspawns = startimeBtwSpawns;
            }
            else
            {
                timeBtwspawns -= Time.deltaTime;
            }
        }

    }
}
