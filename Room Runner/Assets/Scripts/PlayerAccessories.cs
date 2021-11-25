using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
public class PlayerAccessories : MonoBehaviour
{
    [SerializeField]
    GameObject HeadSet;
    [SerializeField]
    GameObject pickUP_VFX;
    
    [SerializeField]
    Transform gamingChair;
    [SerializeField]
    
    List<GameObject> followerS;
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
        followerS[index++].SetActive(true);
        if(pickUP_VFX)
        pickUP_VFX.SetActive(true);
    }

    int RandomIndex = 0;

    
    public void PickUpItemByFollowers(Transform item)
    {
        itemsPicked.Add(item.gameObject);
        Debug.Log(itemsPicked[RandomIndex].name);
        item.transform.parent = followerS[RandomIndex].transform;
        item.DOLocalMove(new Vector3(0,1.5f,0), 0.5f);
        item.DOScale(Vector3.one* 1.5f, 0.75f);
        RandomIndex++;
    }

    public void RearrangeItems(CinemachineVirtualCameraBase endCamera)
    {
        for(int i=0;i<itemsPicked.Count;i++)
        {
            followerS[i].GetComponent<Animator>().SetTrigger("Stop");
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
                gamingChair.DOLocalMove(new Vector3(0, 0, -1), 2.5f).SetEase(Ease.InBack).OnComplete( ()=>
                {
                    endCamera.transform.DOMoveZ(148f, 1f);
                    MiniGame.instance.StartScreen();
                    UIManager.instance.ShowAvatar();
                    MiniGame.instance.StartMiniGameTimer();
                    UIManager.instance.TweenAvatarandMultiplierEffect();
                });
            }
        }

    }

    public void HideFollowers()
    {
        for (int i = 0; i < followerS.Count; i++)
        {
            followerS[i].transform.DOScale(Vector3.zero, 0.5f);
        }
    }
}
