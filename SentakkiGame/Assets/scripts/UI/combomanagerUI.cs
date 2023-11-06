using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class combomanagerUI : MonoBehaviour
{
    public int innercomboUI;
    public int x;
    [SerializeField] private playerattack playerattack;
    [SerializeField] private TextMeshProUGUI combonum;
    [SerializeField] private GameObject combotextObject;

    [SerializeField] private List<Sprite> combotypes;
    [SerializeField] public LeanTweenType tween;
    [SerializeField] private combosfxscriptableobject sfx;
    [SerializeField] private AudioSource comboAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        //removeAplhaCombo();
        //removeAplhaText();
    }

    // Update is called once per frame
    void Update()
    {
        combonum.text = ("x " + innercomboUI);
        combotextObject.GetComponent<Image>().sprite = combotypes[x];
    }

    public void checkcombostatus()
    {
        if (innercomboUI == 0)
        {
            x = 0;
            shake();
            playerattack.extra = 0;
        }
        else if (innercomboUI == 1)
        {
            x = 1;
            shake();
            //playaudio(x);
        }
        else if (innercomboUI == 6)
        {
            x = 2;
            shake();
            //playaudio(x);
        }
        else if (innercomboUI == 12)
        {
            x = 3;
            playerattack.extra = 2f;
            shake();
            //playaudio(x);
        }
        else if (innercomboUI == 18)
        {
            x = 4;
            shake();
        }
        else if (innercomboUI == 24)
        {
            x = 5;
            playerattack.extra = 3f;
            shake();
            //playaudio(x + 1);
        }

    }

    private void shake()
    {
        LeanTween.moveX(combotextObject, 230f, 0.2f);
        LeanTween.moveX(combotextObject, 262f, 0.1f).setEase(tween).setDelay(0.2f);
    }

    /*    public void restoreAplha()
        {
            combonum.CrossFadeAlpha(1f, 0f, false);

            switch (innercomboUI)
            {
                case 1:
                case 6: 
                case 12: 
                case 18: 
                case 24:
                    {
                        combotextObject.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(1f, 0f, false);
                        Invoke("removeAplhaText", 1);
                    }
                break;
            }
        }

        public void removeAplhaCombo()
        {
            combonum.CrossFadeAlpha(0f, 0.5f, false);
        }

        public void removeAplhaText()
        {
            combotextObject.GetComponent<TextMeshProUGUI>().CrossFadeAlpha(0f, 0.5f, false);

        }*/
}
