using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public Renderer myRenderer;
    public Renderer NextRenderer;

    void Start()
    {
        Color randomColour = Random.ColorHSV();   
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == NextRenderer.gameObject)
            {
                ChangeMyRendererColor();
            }
        }


    }

    void ChangeMyRendererColor()
    {
        myRenderer.material.color = Random.ColorHSV();
    }
}