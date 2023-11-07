using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.gameObject.tag == "Player")
    //    {
    //        healthPoint.Instance.TakeDamage(5);
    //        Debug.Log("Player hit AOE!!!");
    //        Destroy(gameObject);
    //    }
    //}
}
