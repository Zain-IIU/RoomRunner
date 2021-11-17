using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAccessories : MonoBehaviour
{
    [SerializeField]
    GameObject HeadSet;
    [SerializeField]
    GameObject pickUP_VFX;

    [SerializeField]
    GameObject[] followers;

    int index = 0;
    public void EnableHeadset()
    {
        HeadSet.SetActive(true);
        followers[index++].SetActive(true);
        if(pickUP_VFX)
        pickUP_VFX.SetActive(true);
    }
}
