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
    RectTransform TwitchFollowersPanel;
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
    [SerializeField]
    Image playerAvatar;
    public bool hasStarted;
    [SerializeField]
    TextMeshProUGUI followersText;
    [SerializeField]
    RectTransform LosePanel;
    [SerializeField]
    RectTransform WinPanel;
    [SerializeField]
    Image followersCount;

    [SerializeField]
    RectTransform tweeningFollowersText_InGame;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        followersCount.fillAmount = 0f;
        followersCount.color = Color.white;
        score = 0;
        hasStarted = false;
        tweeningFollowersText_InGame.gameObject.SetActive(false);
    }
    private void Update()
    {
        if(!hasStarted && Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            ShowFollowersMeter();
            StartGame();
        }

     
    }


    public void IncreaseFollowersAmount()
    {
        TweenFollowersTextinGame();
        followersCount.DOFillAmount(followersCount.fillAmount + 0.2f, 0.25f);
        //followersCount.fillAmount += 0.2f;
        if(followersCount.fillAmount>=.1f && followersCount.fillAmount<0.5f)
            followersCount.color = Color.red;
        if (followersCount.fillAmount >= .5f && followersCount.fillAmount < 0.7f)
            followersCount.color = Color.blue;
        if (followersCount.fillAmount >= .7f && followersCount.fillAmount <1f)
            followersCount.color = Color.green;
    }

    private void TweenFollowersTextinGame()
    {
        tweeningFollowersText_InGame.gameObject.SetActive(true);
        tweeningFollowersText_InGame.DOAnchorPosY(100f, tweeningTime).OnComplete(() =>
       {
           tweeningFollowersText_InGame.gameObject.SetActive(false);
           tweeningFollowersText_InGame.DOAnchorPos(Vector2.zero, 0);
       });
    }
    public void ShowAvatar()
    {
        playerAvatar.DOFillAmount(1, 0.5f);
    }
    public void TweenAvatarandMultiplierEffect()
    {
        TwitchFollowersPanel.DOAnchorPos(new Vector2(0, 0), tweeningTime).SetEase(easeType);
    }
    public void StartGame()
    {
        mainMenuPanel.DOScale(Vector2.zero, tweeningTime / 2).SetEase(easeType);
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
        RectTransform diamondImage = Instantiate(scoreCash, diamondOriginPos);
        diamondImage.DOMove(scoreCash.position, tweeningTime).OnComplete(() =>
        {

            diamondImage.gameObject.SetActive(false);
        });
    }
    public void ShowLosePanel()
    {
        LosePanel.DOScale(Vector3.one, tweeningTime);
    }
    public void ShowWinPanel()
    {
        WinPanel.DOScale(Vector3.one, tweeningTime);
    }

    public void ResetAvatarPos()
    {
        playerAvatar.GetComponent<RectTransform>().DOAnchorPosX(-400f, 0.25f);
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
    public void ShowFollowersMeter()
    {
        followersCount.rectTransform.DOScaleX(1, 0.25f);
    }
    public void HideFollowersMeter()
    {
        followersCount.rectTransform.DOScaleX(0, 0.25f);
    }
    int followers = 1;
    public void CashMultiplier()
    {
        followers++;
        followersText.text = followers.ToString();
        followersText.transform.DOScale(Vector2.one * 1.2f, tweeningTime/2).SetEase(easeType).OnComplete(() =>
        {
            followersText.transform.DOScale(Vector2.one, tweeningTime/2);
        });
    }

}
