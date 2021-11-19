using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;


public class GatesManager : MonoBehaviour
{
    public event Action onCashPickUp;



    public static GatesManager instance;
    private void Awake()
    {
        instance = this;
    }
    public void ChangeGateApperance()
    {
        onCashPickUp?.Invoke();
    }
}
