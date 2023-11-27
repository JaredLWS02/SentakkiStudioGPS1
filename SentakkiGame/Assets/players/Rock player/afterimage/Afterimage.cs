using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Afterimage : MonoBehaviour
{
    public float activeTime = 0.1f;
    [SerializeField] private float timeActivated;
    [SerializeField] private float alpha;
    [SerializeField] private float alphaset = 0.8f;
    [SerializeField] private float alphaMultiplier = 0.8f;
    [SerializeField] private Transform player;

    private SpriteRenderer Sr;
    private SpriteRenderer playerSr;
    public Color color;


    private void OnEnable()
    {
        Sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").transform;
        playerSr = player.GetComponent<SpriteRenderer>();

        alpha = alphaset;
        Sr.sprite = playerSr.sprite;
        transform.position = player.position;
        transform.localScale = player.localScale;

        timeActivated = Time.time;
        
    }

    private void Update()
    {
        alpha *= alphaMultiplier;
        color = new Color(1f, 1f, 1f, alpha);
        Sr.color = color;

        if(Time.time >= (timeActivated +activeTime))
            {
            AfterImagePooling.instance.AddToPool(gameObject);
            }
    }
  
}
