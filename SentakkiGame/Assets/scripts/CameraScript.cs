using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float followSpeed = 2f;
    public Transform target;

    private bool isFollowing = true;

    void Update()
    {
        if (isFollowing && target != null)
        {
            Vector3 newPosition = new Vector3(target.position.x, target.position.y, -2f);
            transform.position = Vector3.Slerp(transform.position, newPosition, followSpeed * Time.deltaTime);
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