using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowFollow : MonoBehaviour
{
    public GameObject obj;
    private float posy;
    // Start is called before the first frame update
    void Start()
    {
        obj = this.gameObject;
        posy = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(obj.transform.position.x, Mathf.Clamp(transform.position.y,posy,posy));
    }
}
