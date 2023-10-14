using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float followSpeed;
    public Transform target;

    private bool isFollowing = true;

    void FixedUpdate()
    {
        if (isFollowing && target != null)
        {
            transform.position = new Vector3(target.position.x, 0, target.position.z);
        }
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