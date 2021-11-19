using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    int RandomIndex = 0;

    public void PickUpItemByFollowers(Transform item)
    {
        item.transform.parent = followers[RandomIndex].transform;
        item.DOLocalMove(new Vector3(0,1.5f,0), 0.5f);
        item.DOScale(Vector3.one* 1.5f, 0.75f);
        RandomIndex++;
    }
}
