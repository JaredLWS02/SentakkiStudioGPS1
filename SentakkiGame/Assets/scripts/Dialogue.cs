using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public int index;
    public Image imaged;
    public List<Sprite> imageChoices;

    private bool showNextLine = false;
    private bool dialogueFinished = false;
    public GameObject starttext;

    void Start()
    {
        textComponent.text = string.Empty;
        StartCoroutine(ShowDialogue());
    }

    void Update()
    {
        if (showNextLine && index >= lines.Length)
        {
            starttext.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                scenemanager.instance.switchtoTutorialStage();
            }
            return;
        }

        if (showNextLine && Input.GetMouseButtonDown(0))
        {
            ShowNextLine();
        }

        if (index >= 2 && index < 5)
        {
            imaged.sprite = imageChoices[0];
        }
        else if(index >= 5)
        {
            imaged.sprite = imageChoices[1];
        }
    }

    IEnumerator ShowDialogue()
    {
        for (index = 0; index < lines.Length; index++)
        {
            showNextLine = false;
            textComponent.text = string.Empty;

            string line = lines[index];
            for (int i = 0; i <= line.Length; i++)
            {
                textComponent.text = line.Substring(0, i);
                if (line.Contains("Rokku:"))
                {
                    textComponent.color = Color.red; // Change color to red
                }
                else
                {
                    textComponent.color = new Color(0,0.96f,0.71f); // Reset color to white for other lines
                }
                yield return new WaitForSeconds(textSpeed);
            }

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); // Wait for mouse click

            showNextLine = true;
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0)); // Wait for mouse release
        }

        dialogueFinished = true;
    }

    void ShowNextLine()
    {
        index++;
        if (index < lines.Length)
        {
            StartCoroutine(TypeLine(lines[index]));
        }
        else
        {
            dialogueFinished = true;
        }
        showNextLine = false;
    }

    IEnumerator TypeLine(string line)
    {
        textComponent.text = string.Empty;
        for (int i = 0; i <= line.Length; i++)
        {
            textComponent.text = line.Substring(0, i);
            if (line.Contains("Rokku:"))
            {
                textComponent.color = Color.red; // Change color to red
            }
            else
            {
                textComponent.color = Color.blue; // Reset color to white for other lines
            }
            yield return new WaitForSeconds(textSpeed);
        }
        showNextLine = true;
    }
}
