using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDelayedAd : MonoBehaviour
{
    [SerializeField] private bool useDelay;
    [SerializeField] private bool canLoadAd;
    [SerializeField] private int delay;
    [SerializeField] private int delayCount;

    
    private void Start()
    {
        if (GameObject.Find("AdmobAds") != null)
        {
            canLoadAd = true;
            delayCount = delay;
        }
        else
        {
            canLoadAd = false;
        }
    }
    
    public void LoadInterDelayed()
    {
        if (canLoadAd)
        {
            if (useDelay)
            {
                if (delayCount > 0)
                {
                    delayCount--;
                }
                else
                {
                    delayCount = delay;
                    AdmobAds.admobAds.ShowInterstitialDirectly();
                }
            }
            
        }
    }
}
