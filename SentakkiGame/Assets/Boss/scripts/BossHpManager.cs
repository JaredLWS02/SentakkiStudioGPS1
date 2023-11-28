using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHpManager : MonoBehaviour
{
    [SerializeField] private float maxHp;
    [SerializeField] private float curHp;
    // Start is called before the first frame update
    void Start()
    {
        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float damage)
    {
        curHp -= damage;
        if(curHp <= 0)
        {
            bossDeath();
        }
    }

    void bossDeath()
    {
        Destroy(gameObject);
    }
}
