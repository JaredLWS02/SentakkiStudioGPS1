using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;


public class BossHpManager : MonoBehaviour
{
    [SerializeField] private float maxHp;
    [SerializeField] private float curHp;
    [SerializeField] private GameObject progressBar;
    [SerializeField] private GameObject progressBarBoss;
    [SerializeField] private GameObject barfill;
    [SerializeField] private GameObject BossBarfill;
    [SerializeField] private GameObject deathAnim;
    [SerializeField] private GameObject StageClearCol;

    private void Awake()
    {
        progressBar.SetActive(false);
        barfill.SetActive(false);

        progressBarBoss.SetActive(true);
        BossBarfill.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        BossBarfill.GetComponent<Image>().fillAmount = Mathf.Lerp(BossBarfill.GetComponent<Image>().fillAmount, curHp/maxHp, Time.deltaTime);
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
        Instantiate(deathAnim, new Vector3(195, 0, 0), Quaternion.identity);
        Instantiate(StageClearCol, new Vector3(203.5f, -1.5f, 0), Quaternion.identity);

    }
}
