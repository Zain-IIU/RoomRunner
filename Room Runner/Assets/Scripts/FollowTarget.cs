using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] Vector3 offset;
   
    private void LateUpdate()
    {
        if(target)
        {
            transform.position = target.position + offset;
        }
      

    }
}
