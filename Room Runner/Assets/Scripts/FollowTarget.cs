using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] Vector3 offset;
    [SerializeField]
    float amount;

    Vector3 curOffset;

    private void Start()
    {
        curOffset = offset;
    }

    private void LateUpdate()
    {
        if (target)
        {
            transform.position = target.position + offset;
        } 
    }

}
