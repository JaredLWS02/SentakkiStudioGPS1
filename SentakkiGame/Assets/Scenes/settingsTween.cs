using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingsTween : MonoBehaviour
{
    public void tweenMove()
    {
        LeanTween.moveLocal(gameObject,new Vector3(-363,-8),0.3f);
        LeanTween.scale(gameObject, new Vector3(2.22f, 2.22f, 2.22f), 0.3f);
    }
    public void tweenMoveBack()
    {
        LeanTween.scale(gameObject, Vector3.zero, 0.3f);
        LeanTween.moveLocal(gameObject, new Vector3(339, -8), 0.01f).setDelay(0.3f);
    }
}
