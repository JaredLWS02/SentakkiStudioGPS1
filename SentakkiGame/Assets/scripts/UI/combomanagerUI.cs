using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class combomanagerUI : MonoBehaviour
{
    public static combomanagerUI instance;
    public int innercomboUI;
    public int x;
    [SerializeField] private playerattack playerattack;
    [SerializeField] private TextMeshProUGUI combonum;
    [SerializeField] private GameObject combotextObject;

    [SerializeField] private List<Sprite> combotypes;
    [SerializeField] public LeanTweenType tween;
    [SerializeField] private combosfxscriptableobject sfx;
    [SerializeField] private AudioSource comboAudioSource;
    private bool invoked;
    public float comboCounter;
    // Start is called before the first frame update
    void Start()
    {
        comboCounter = 0;
        instance = this;
        removeAplhaCombo();
        combotextObject.GetComponent<Image>().CrossFadeAlpha(0, 0.3f, false);
    }

    // Update is called once per frame
    void Update()
    {
        if(innercomboUI > comboCounter)
        {
            comboCounter = innercomboUI;
        }
        combonum.text = ("x " + innercomboUI);
        combotextObject.GetComponent<Image>().sprite = combotypes[x];

        if (x == 0 && combonum.alpha >= 1)
        {
            removeAplhaCombo();
            RemoveAplhaText();
        }
    }

    public void checkcombostatus()
    {
        switch (innercomboUI)
        {
            case 0:
                {
                    x = 0;
                    shake();
                    restoreAplha();
                }
                break;
            case 1:
                {
                    x = 1;
                    shake();
                    restoreAplha();
                }
                break;
            case 6:
                {
                    x = 2;
                    shake();
                    restoreAplha();
                }
                break;
            case 12:
                {
                    x = 3;
                    playerattack.extra = 1f;
                    shake();
                    restoreAplha();
                }
                break;
            case 18:
                {
                    x = 4;
                    shake();
                    restoreAplha();
                }
                break;
            case 24:
                {
                    x = 5;
                    playerattack.extra = 2f;
                    shake();
                    restoreAplha();
                }
                break;
            default:
                {
                    if (innercomboUI >= 24)
                    {
                        shake();
                        combotextObject.GetComponent<Image>().CrossFadeAlpha(1, 0, false);
                    }
                }
                break;

        }


    }

    private void shake()
    {
        LeanTween.moveX(combotextObject, combotextObject.transform.position.x - 30f, 0.2f);
        LeanTween.moveX(combotextObject, combotextObject.transform.position.x, 0.2f).setEase(tween).setDelay(0.2f);
    }

    public void restoreAplha()
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
                    combotextObject.GetComponent<Image>().CrossFadeAlpha(1, 0, false);
                    if (!invoked)
                    {
                        Invoke("RemoveAplhaText", 2);
                        invoked = true;
                    }
                }
                break;
        }
    }

    public void removeAplhaCombo()
    {
        combonum.CrossFadeAlpha(0f, 0.3f, false);
    }

    public void RemoveAplhaText()
    {
        combotextObject.GetComponent<Image>().CrossFadeAlpha(0, 0.3f, false);
        invoked = false;
    }
}
