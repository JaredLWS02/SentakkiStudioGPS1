using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static CameraScript instance;
    public List<GameObject> barrier;
    public float followSpeed;
    public Vector2 offset;
    public Transform target;

    private bool isFollowing = true;
    private void Start()
    {
        instance = this;
    }
    void FixedUpdate()
    {
        if (isFollowing && target != null)
        {
            transform.position = new Vector2(target.position.x, 0) + offset;
        }
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -0.56f, 350.0f),0);
    }

    public void StopFollowing()
    {
        foreach (GameObject o in barrier)
        {
            o.SetActive(true);
        }
        isFollowing = false;
    }

    public void ResumeFollowing()
    {
        foreach (GameObject o in barrier)
        {
            o.SetActive(false);
        }
        isFollowing = true;
    }
}