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
    [SerializeField]
    Animator animator;
    [SerializeField]
    GameObject VFX;
    int curCash;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        curCash = 0;
    }

    private void Update()
    {
        Vector3 v = Camera.main.transform.position - transform.position;
        v.x = v.z = 0.0f;
        cashText.transform.parent.LookAt(Camera.main.transform.position - v);

        cashText.transform.parent.Rotate(0, 180, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Cash"))
        {
            UIManager.instance.coinPickUpEffect(incremnetinCash);
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
        if(other.gameObject.CompareTag("Obstacle"))
        {
            other.gameObject.transform.DOScale(0, 0.25f);
            DecrementCash(50);
            animator.SetTrigger("Walk_Sad");
            VFX.SetActive(true);
        }

    }

    public void DecrementCash(int amount)
    {
        UIManager.instance.DecrementCashEffect(amount);
        curCash -= amount;
        if(curCash<=1)
        {
            GetComponent<PlayerController>().LosePlayer();
        }
        else
        {
            cashText.text = curCash.ToString();
            cashText.transform.DOScale(Vector3.one * 1.25f, 0.25f).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                cashText.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.InOutSine);
            });
            GatesManager.instance.ChangeGateApperance();
        }
       
    }
     public int getCurCash()
    {
        return curCash;
    }
}
