using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] Vector3 offset;
   

    Vector3 curOffset;

    private void Start()
    {
        curOffset = offset;
       
    }

    private void LateUpdate()
    {
        if (target )
        {
            transform.position = target.position + offset;
        }
    }

}
