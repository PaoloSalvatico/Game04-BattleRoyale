using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public float followSpeed = .6f;


    void Update()
    {
        if (target == null) return;
        transform.position = Vector3.Lerp(transform.position, target.position, followSpeed);
    }
}
