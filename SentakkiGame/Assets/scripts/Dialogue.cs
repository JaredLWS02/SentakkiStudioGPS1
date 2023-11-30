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
    private int index;
    public Image imaged;
    public List<Sprite> imageChoices;

    private bool showNextLine = false;
    private bool dialogueFinished = false;

    void Start()
    {
        textComponent.text = string.Empty;
        StartCoroutine(ShowDialogue());
    }

    void Update()
    {
        if (showNextLine && Input.GetMouseButtonDown(0))
        {
            ShowNextLine();
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
            yield return new WaitForSeconds(textSpeed);
        }
        showNextLine = true;
    }
}
