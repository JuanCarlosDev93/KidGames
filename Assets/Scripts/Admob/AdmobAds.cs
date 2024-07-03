using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class AdmobAds : MonoBehaviour
{
    public static AdmobAds admobAds;
    public AdTimer adTimer;
    private InterstitialAd interstitial;
    [Header("Menu Ads")]
    [SerializeField] private bool delayInterAds;
    [SerializeField] private int delay;
    public int delayCount;
    [Header("Playlist Ads")]
    public bool delayInterAdsPlaylist;
    [SerializeField] private int delayPlaylist;
    public int delayPlaylistCount;

    public bool interAdLoaded;
    public bool noAds;
    public string sceneToTransition;


#if UNITY_ANDROID
    string adUnitId = "ca-app-pub-1214160927205088/6022647322";

#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

    void Start()
    {
        admobAds = this;
        DontDestroyOnLoad(this.gameObject);
        delayCount = delay;
        // Initialize the Google Mobile Ads SDK.
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            noAds = true;
            return;
        }
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
            LoadInterstitialAd();
        });
        
    }
    
    private void LoadInterstitialAd()
    {
        if(interstitial != null)
        {
            DestroyAd();
        }

        AdRequest adRequest = new AdRequest();
        List<string> deviceIds = new List<string>();
        //deviceIds.Add("f4bfc750f1504134966401cdcdeaf7dc");
        deviceIds.Add("B894F846BE4248BE04A5784EC7DDF4C4");
        /*RequestConfiguration requestConfiguration = new RequestConfiguration.Builder()
        .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.True)
        .SetTestDeviceIds(deviceIds)
        .build();*/

        RequestConfiguration requestConfiguration = new RequestConfiguration
        {
            TagForChildDirectedTreatment = TagForChildDirectedTreatment.True,
            TestDeviceIds = deviceIds
        };        
        MobileAds.SetRequestConfiguration(requestConfiguration);
        
        InterstitialAd.Load(adUnitId, adRequest,(InterstitialAd ad, LoadAdError error)=>
        {
            // If the operation failed with a reason.
            if (error != null)
            {
                Debug.LogError("Interstitial ad failed to load an ad with error : " + error);
                LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(sceneToTransition));
                return;
            }
            // If the operation failed for unknown reasons.
            // This is an unexpected error, please report this bug if it happens.
            if (ad == null)
            {
                Debug.LogError("Unexpected error: Interstitial load event fired with null ad and null error.");
                LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(sceneToTransition));
                return;
            }

            // The operation completed successfully.
            Debug.Log("Interstitial ad loaded with response : " + ad.GetResponseInfo());
            interstitial = ad;

            // Register to ad events to extend functionality.
            RegisterEventHandlers(ad);

            // Inform the UI that the ad is ready.
            //AdLoadedStatus?.SetActive(true);
        });

    }

    public void ShowInterstitialDelayed()
    {
        if (!noAds)
        {
            if (delayInterAds)
            {
                if (delayCount > 0)
                {
                    delayCount--;
                    LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(sceneToTransition));
                }
                else
                {
                    delayCount = delay;
                    if (interstitial.CanShowAd())
                    {
                        interAdLoaded = true;
                        LoadingScreenAd.instance.Intro();
                    }
                    else
                    {
                        LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(sceneToTransition));
                    }
                }
            }
        }
        else
        {
            LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(sceneToTransition));
        }
    }
    public void ShowInterstitialDelayedPlaylist()
    {
        if (!noAds)
        {
            if (delayInterAdsPlaylist)
            {
                if (delayPlaylistCount > 0)
                {
                    delayPlaylistCount--;
                    LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(sceneToTransition));
                }
                else
                {
                    delayPlaylistCount = delayPlaylist;
                    if (interstitial.CanShowAd())
                    {
                        interAdLoaded = true;
                        LoadingScreenAd.instance.Intro();
                    }
                    else
                    {
                        LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(sceneToTransition));
                    }
                }
            }
        }
        else
        {
            LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(sceneToTransition));
        }

    }
    public void ShowInterstitialDirectly()
    {
        if (interstitial != null && interstitial.CanShowAd())
        {
            interstitial.Show();
            Debug.Log("Showing interstitial ad.");
        }
        else
        {
            LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(sceneToTransition));
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }       

    public void DestroyAd()
    {
        if (interstitial != null)
        {
            interstitial.Destroy();
            interstitial = null;
            Debug.Log("Destroying interstitial ad.");
            
        }

        // Inform the UI that the ad is not ready.
        //AdLoadedStatus?.SetActive(false);
    }
    
    public void LogResponseInfo()
    {
        if (interstitial != null)
        {
            var responseInfo = interstitial.GetResponseInfo();
            UnityEngine.Debug.Log(responseInfo);
        }
    }

    private void RegisterEventHandlers(InterstitialAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            LoadInterstitialAd();
            LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(sceneToTransition));
            Debug.Log("Interstitial ad full screen content closed.");            

        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content with error : "
                + error);
            LoadInterstitialAd();
            LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(sceneToTransition));
        };
    }
    private static string NameFromIndex(int BuildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }
    private int SceneIndexFromName(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string testedScreen = NameFromIndex(i);
            //print("sceneIndexFromName: i: " + i + " sceneName = " + testedScreen);
            if (testedScreen == sceneName)
                return i;
        }
        return -1;
    }

}
