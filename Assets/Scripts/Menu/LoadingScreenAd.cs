using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;

public class LoadingScreenAd : MonoBehaviour
{
    public static LoadingScreenAd instance;
    [SerializeField] private CanvasGroup bg;
    [SerializeField] private RectTransform bannerText;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private int adDelayTime;
    private Vector2 bannerInitPos;
    private float timer;
    [SerializeField] private int seconds;
    [SerializeField] private bool startTimer;
    public bool adScreenOpened;
    
    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
        bannerInitPos = bannerText.anchoredPosition;
        timer = adDelayTime;
        text.text = "Anuncio en:  " + timer.ToString();
    }
    private void Update()
    {
        BannerTimer();
    }
    public void Intro()
    {
        gameObject.SetActive(true);
        bg.blocksRaycasts = true;
        //bg.DOFade(1,1f).OnComplete(()=> ShowBannerText());
        bg.DOFade(1,1f).OnComplete(()=> startTimer = true);
    }
    void ShowBannerText()
    {
        bannerText.DOAnchorPosX(20f, 0.7f).OnComplete(()=> startTimer = true);
    }
    void BannerTimer()
    {
        if (startTimer)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                seconds = (int)(timer % 60);
                //text.text = "Anuncio en:  " + seconds.ToString();
            }
            else
            {
                startTimer = false;
                bg.blocksRaycasts = false;
                //bannerText.anchoredPosition = bannerInitPos;
                adScreenOpened = true;
                AdmobAds.admobAds.ShowInterstitialDirectly();
            }
            
        }
        
    } 
    public void ResetScreen()
    {
        adScreenOpened = false;
        gameObject.SetActive(false);
        bg.alpha = 0;
        seconds = 0;
        timer = adDelayTime;
        text.text = "Anuncio en:  " + timer.ToString();
        bannerText.anchoredPosition = bannerInitPos;
    }
}
