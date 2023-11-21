using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static CameraScript instance;
    public float followSpeed;
    public Vector3 offset;
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
            transform.position = new Vector2(target.position.x, 0);
        }
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -0.56f, 145.0f),0);
    }

    public void StopFollowing()
    {
        isFollowing = false;
    }

    public void ResumeFollowing()
    {
        isFollowing = true;
    }
}