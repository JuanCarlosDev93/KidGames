using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitioner : MonoBehaviour
{
    [SerializeField] private string sceneToTransitionTo;
    [SerializeField] private bool useInterAd;
    [SerializeField] private bool useVoice;
    [SerializeField] private AudioClip sceneToGo;
    private Button buttonCmpnt;

    private void Start()
    {
        buttonCmpnt = GetComponent<Button>();

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            useInterAd = false;
        }
        else if (GameObject.Find("AdmobAds") == null)
        {
            useInterAd = false;
        }
    }
    public void Transition()
    {
        if (Input.touchCount >= 2)
        {
            return;
        }
        AudioManager.audioManager.StopAudioVoice();
        if (useVoice)
        {            
            AudioManager.audioManager.PlayOneShotVoice(sceneToGo);
        }
        //AudioManager.audioManager.PlayOneShotAS(AudioManager.audioManager.buttonAC);

        if (!AdmobAds.admobAds.noAds)
        {
            if (useInterAd)
            {
                AdmobAds.admobAds.sceneToTransition = sceneToTransitionTo;
                AdmobAds.admobAds.ShowInterstitialDelayed();
            }
            else
            {
                LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(sceneToTransitionTo));
            }

        }
        else
        {
            LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(sceneToTransitionTo));
        }

    }

    public void DelayedSceneTransition()
    {
        StartCoroutine(DelayTransition(1.3f));
    }

    IEnumerator DelayTransition(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Transition();
    }
}