using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    public GameObject target;
    public bool flip;
    public float speed;
    public Animator anim;
    public bool isFacingRight;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("player");
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = transform.localScale;

        if(target.transform.position.x > transform.position.x)
        {
            flip = true;
            anim.SetBool("isMoving", true);
            scale.x = Mathf.Abs(scale.x) * -1 * (flip? - 1 : 1);
            transform.Translate(x: speed * Time.deltaTime, y: 0, z: 0);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            flip = false;
            anim.SetBool("isMoving", true);
            scale.x = Mathf.Abs(scale.x) * (flip? -1 : 1);
            transform.Translate(x: speed * Time.deltaTime * 1, y: 0, z: 0);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        transform.localScale = scale;


    }
}
