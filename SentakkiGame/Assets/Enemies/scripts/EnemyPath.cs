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
    [SerializeField] private float distanceOffset;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("player");
        anim = gameObject.GetComponent<Animator>();
        float tempSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 scale = transform.localScale;

        if(target.transform.position.x > (transform.position.x + distanceOffset)) // right
        {
            flip = true;
            //scale.x = Mathf.Abs(scale.x) * -1 * (flip? - 1 : 1);
            transform.Translate(x: speed * Time.deltaTime , y: 0, z: 0);
           // transform.eulerAngles = new Vector3(0, 0, 0);
            transform.localScale = new Vector2(4, 4);
        }
        else if (target.transform.position.x < (transform.position.x - distanceOffset)) // left
        {
            flip = false;
            //scale.x = Mathf.Abs(scale.x) * (flip? -1 : 1);
            transform.Translate(x: -speed * Time.deltaTime, y: 0, z: 0);
            //transform.eulerAngles = new Vector3(0, 180, 0);
            transform.localScale = new Vector2(-4, 4);
        }
        //transform.Translate(x: speed * Time.deltaTime, y: 0, z: 0);

        //transform.localScale = scale;


    }
}
