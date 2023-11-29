using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageClearCombo : MonoBehaviour
{
    public TextMeshProUGUI comboNumText;
    public TextMeshProUGUI enemyDedText;
    // Start is called before the first frame update
    void Start()
    {
        comboNumText.text = "x " + combomanagerUI.instance.comboCounter.ToString();
        enemyDedText.text = PlayerPrefs.GetInt("enemiesKilled").ToString();
        PlayerPrefs.SetInt("enemiesKilled", 0);
        PlayerPrefs.Save();
    }

}
