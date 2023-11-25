using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImagePooling : MonoBehaviour
{
    [SerializeField] private GameObject echo;

    private Queue<GameObject> pool = new Queue<GameObject>();

    public static AfterImagePooling instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        GrowPool();
    }

    private void GrowPool()
    {
        for(int i =0; i< 10; i++)
        {
            var p = Instantiate(echo);
            p.transform.SetParent(transform);
            AddToPool(p);
        }
    }

    public void AddToPool(GameObject o)
    {
        o.SetActive(false);
        pool.Enqueue(o);
    }

    public GameObject GetFromPool()
    {
        if(pool.Count == 0)
        {
            GrowPool();
        }

        var instance = pool.Dequeue();
        instance.SetActive(true);
        return instance;
    }
}
