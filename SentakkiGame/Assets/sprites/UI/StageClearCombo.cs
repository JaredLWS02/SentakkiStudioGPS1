using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageClearCombo : MonoBehaviour
{
    public TextMeshProUGUI comboNumText;
    // Start is called before the first frame update
    void Start()
    {
        comboNumText.text = "x " + combomanagerUI.instance.comboCounter.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
