using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeBarrier : MonoBehaviour
{
    public static removeBarrier instance;
    public GameObject barrier;
    public float count;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(count >= 5)
        {
            Destroy(barrier);
        }
    }
}
