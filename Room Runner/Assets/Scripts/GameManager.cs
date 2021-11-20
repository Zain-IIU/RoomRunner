using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public event Action onGameStarted;

    private void Awake()
    {
        instance = this;
    }

    public bool hasStarted;
    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !hasStarted)
        {
            hasStarted = true;
            onGameStarted?.Invoke();
        }
    }
}
