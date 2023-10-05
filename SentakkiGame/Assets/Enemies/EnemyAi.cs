using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public GameObject target1;
    public GameObject target2;
    public GameObject selfHitbox;
    public EnemyScriptable enemyData;
    private Rigidbody2D rb;
    private float hp;
    private float atk;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        hp = enemyData.hp;
        atk = enemyData.atk;
        speed = enemyData.speed;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void fixedUpdate()
    {

    }
}
