using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class combomanagerUI : MonoBehaviour
{
    public static combomanagerUI instance;

    public int innercomboUI;
    [SerializeField] private TextMeshProUGUI combotext;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        combotext.text = ("x " + innercomboUI);
    }
}
