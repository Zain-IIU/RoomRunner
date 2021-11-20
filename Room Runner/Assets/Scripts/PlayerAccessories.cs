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
    Transform gamingChair;
    [SerializeField]
    GameObject[] followers;
    [SerializeField]
    List<GameObject> itemsPicked;

    [SerializeField]
    Transform[] positionofEachObject;
    int index = 0;

    private void Start()
    {
        itemsPicked = new List<GameObject>();
    }
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
        itemsPicked.Add(item.gameObject);
        Debug.Log(itemsPicked[RandomIndex].name);
        item.transform.parent = followers[RandomIndex].transform;
        item.DOLocalMove(new Vector3(0,1.5f,0), 0.5f);
        item.DOScale(Vector3.one* 1.5f, 0.75f);
        RandomIndex++;
    }

    public void RearrangeItems()
    {
        for(int i=0;i<itemsPicked.Count;i++)
        {
            followers[i].GetComponent<Animator>().SetTrigger("Stop");
            for(int j=0;j<positionofEachObject.Length;j++)
            {
                if( itemsPicked[i].name==positionofEachObject[j].name)
                {
                    itemsPicked[i].transform.DOLocalRotate(Vector3.zero, 0.25f);
                    itemsPicked[i].transform.parent = positionofEachObject[j];
                    itemsPicked[i].transform.DOLocalMove(Vector3.zero, 1f);
                    itemsPicked[i].transform.DOScale(Vector3.one, 0.5f);
                }
            }
            if(i==itemsPicked.Count-1)
            {
                gamingChair.DOLocalMove(new Vector3(0, 0, -1), 2.5f).SetEase(Ease.InBack);
            }
        }

    }
}
