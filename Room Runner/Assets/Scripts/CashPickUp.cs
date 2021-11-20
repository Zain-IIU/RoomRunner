using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using DG.Tweening;

public class CashPickUp : MonoBehaviour
{
    public static CashPickUp instance;

    [SerializeField]
    TextMeshProUGUI cashText;
    [SerializeField]
    GameObject pickUPVFX;
    [SerializeField]
    int incremnetinCash;
    int curCash;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        curCash = 0;
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Cash"))
        {
            cashText.transform.DOScale(Vector3.one * 1.25f, 0.25f).SetEase(Ease.InOutSine).OnComplete(()=>
            {
                cashText.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.InOutSine);
            });
            curCash += incremnetinCash;
            cashText.text = curCash.ToString();
            other.gameObject.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InOutQuart);
            if(pickUPVFX)
            pickUPVFX.SetActive(true);
            GatesManager.instance.ChangeGateApperance();
        }

    }

    public void DecrementCash(int amount)
    {
        curCash -= amount;
        cashText.text = curCash.ToString();
        cashText.transform.DOScale(Vector3.one * 1.25f, 0.25f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            cashText.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.InOutSine);
        });
        GatesManager.instance.ChangeGateApperance();
    }
     public int getCurCash()
    {
        return curCash;
    }
}