using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using TMPro;


public class Gate : MonoBehaviour
{
    [SerializeField]
    GameObject planeActive;

    [SerializeField]
    GameObject planeInActive;

    [SerializeField]
    TextMeshProUGUI priceText;

    [SerializeField]
    int priceTag;

    public bool isActive;

    public int getPrice() { return priceTag; }
    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        planeActive.SetActive(isActive);
        planeInActive.SetActive(!isActive);
        priceText.text = priceTag.ToString();

        GatesManager.instance.onCashPickUp += SetGateAvailability;
    }

    private void SetGateAvailability()
    {
        if(CashPickUp.instance.getCurCash()>=priceTag)
        {
            isActive = true;
            planeActive.SetActive(isActive);
            planeInActive.SetActive(!isActive);
        }
        else
        {
            isActive = false;
            planeActive.SetActive(isActive);
            planeInActive.SetActive(!isActive);
        }
    }

   
}
