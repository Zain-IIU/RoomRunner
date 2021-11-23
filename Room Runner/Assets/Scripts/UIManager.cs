using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;



public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField]
    RectTransform InGamePanel;
    [SerializeField]
    RectTransform mainMenuPanel;
    [SerializeField]
    float tweeningTime;
    [SerializeField]
    Ease easeType;
    [SerializeField]
    RectTransform scoreCash;
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    RectTransform diamondOriginPos;
    int score;

    public bool hasStarted;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        hasStarted = false;
    }
    private void Update()
    {
        if(!hasStarted && Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            StartGame();
        }
    }

    public void StartGame()
    {
        mainMenuPanel.DOScale(Vector2.zero, tweeningTime / 2).SetEase(easeType).OnComplete(() =>
        {
            InGamePanel.DOScale(Vector2.one, tweeningTime / 2);
        });
    }
  
    [ContextMenu("PickUp Effect")]
    public void coinPickUpEffect(int amounttoIncrease)
    {
        scoreCash.DOScale(Vector2.one * 1.2f, tweeningTime / 2).SetEase(easeType).OnComplete(() =>
        {
            scoreCash.DOScale(Vector2.one, tweeningTime);
        });
        score += amounttoIncrease;
        scoreText.text = score.ToString();
        RectTransform diamondImage = Instantiate(scoreCash,diamondOriginPos);
        diamondImage.DOMove(scoreCash.position, tweeningTime).OnComplete(() =>
        {

            diamondImage.gameObject.SetActive(false);
        });
    }

    public void DecrementCashEffect(int amount)
    {
        score -= amount;
        scoreText.text = score.ToString();
        scoreCash.DOScale(Vector2.one * 1.2f, tweeningTime / 2).SetEase(easeType).OnComplete(() =>
        {
            scoreCash.DOScale(Vector2.one, tweeningTime);
        });
    }

}
