using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueImage : MonoBehaviour
{
    public Image imaged;
    public List<Sprite> imageChoices;

    private int counter;
    private int currentImage = 0;

    void Start()
    {

    }

    void Update()
    {

    }

    public void NextImage()
    {
        counter++;
        if (counter == 1)
        {
            currentImage++;
            counter = 0;
            if (currentImage >= imageChoices.Count)
            {
                currentImage = 0;
            }
            imaged.sprite = imageChoices[currentImage];
        }
    }
}