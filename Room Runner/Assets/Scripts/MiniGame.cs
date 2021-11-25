using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

using Cinemachine;

public class MiniGame : MonoBehaviour
{
    public static MiniGame instance;


    [SerializeField]
    RectTransform screenOfMonitor;

    [SerializeField]
    RectTransform dotInScreen;

    [SerializeField]
    Image healthBar;

    [SerializeField]
    CinemachineVirtualCamera winCamera;


    bool hasLost;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    public void StartScreen()
    {
        screenOfMonitor.DOScale(Vector2.one* 0.02f, 0.5f);
    }

    private void Update()
    {
        if (healthBar.fillAmount <= 0 && !hasLost)
        {
            hasLost = true;
            dotInScreen.DOScale(Vector2.zero, 0.15f);
            winCamera.m_Priority = 15;
            UIManager.instance.ResetAvatarPos();
            GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetTrigger("Win");
        }
    }


    public void StartMiniGameTimer()
    {
        healthBar.DOFillAmount(0, 2f);
    }
    public void PlayTheGame()
    {
        DOTween.KillAll();
        healthBar.fillAmount = 1f;
        StartMiniGameTimer();
        UIManager.instance.CashMultiplier();

            dotInScreen.DOScale(Vector2.zero, 0.15f).OnComplete(() =>
            {
                dotInScreen.DOAnchorPos(new Vector3(Random.Range(-23f, 23f), Random.Range(-6f, 6f), 0.25f), 0.25f).OnComplete(() =>
                {
                    dotInScreen.DOScale(Vector2.one, 0.15f);
                });
            });
        
    }
}
