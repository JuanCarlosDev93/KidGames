using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class RewardedButton : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI timerTxt;
    private string defaultTxt = "Ver anuncio";

    private void Start()
    {
        AdmobAds.admobAds.adTimer.onTimerEnd.AddListener(DefaultText);
    }
    private void Update()
    {
        if (AdmobAds.admobAds.adTimer.enableTimer)
        {          
            timerTxt.text = TimeSpan.FromSeconds(AdmobAds.admobAds.adTimer.RemainingTime()).ToString(@"m\:ss");            
        }        
    }
    public void ShowRewarded()
    {
        //AdmobAds.admobAds.ShowRewardedAd();
    }
    void DefaultText()
    {
        timerTxt.text = defaultTxt;
    }
}
