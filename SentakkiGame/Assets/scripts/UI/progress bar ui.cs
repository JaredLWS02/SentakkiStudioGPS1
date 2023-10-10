using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progressbarui : MonoBehaviour
{
    private float maxbar = 100f;
    public float barfill;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void updatebar()
    {
        GetComponent<Image>().fillAmount = barfill /maxbar;
    }
}
