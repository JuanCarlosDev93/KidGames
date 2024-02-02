using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaylistGames : MonoBehaviour
{
    public static PlaylistGames playlist;
    [SerializeField] private int totalScenes;
    [SerializeField] TextMeshProUGUI timerTxt;
    private float timer;
    [SerializeField] int totalTime;
    public bool startTimer;
    //[SerializeField] private bool useInterAd;


    private void Start()
    {
        playlist = this;
        timer = totalTime;
        totalScenes = SceneManager.sceneCountInBuildSettings;
    }

    private int GetRandomScene(int totalScene)
    {
        return Random.Range(0, totalScene);
    }

    private void Update()
    {
        LoadSceneTimer();
    }
    
    void LoadSceneTimer()
    {
        if (startTimer)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                int timerInt = (int)(timer % 60);
                timerTxt.text = timerInt.ToString();
            }
            else
            {
                startTimer = false;
                timer = 0;
                timerTxt.text = "0";
                //LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(NextScene()));
                LoadScene();
            }

        }

    }
    private int NextScene()
    {
        int actualScene = SceneManager.GetActiveScene().buildIndex;
        if (actualScene > 1)
        {
            if (actualScene == totalScenes-1)
            {
                actualScene = 2;
            }
            else
            {
                actualScene++;
            }
        }
        else
        {
            actualScene = 2;
        }
        return actualScene;
    }
    public void LoadScene()
    {
        if (AdmobAds.admobAds.delayInterAdsPlaylist)
        {
            AdmobAds.admobAds.sceneToTransition = NameFromIndex(NextScene());
            AdmobAds.admobAds.ShowInterstitialDelayed();
            return;
        }
        LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(NextScene()));

    }
    public void StartTimer()
    {
        startTimer = true;
    }
    public void StopTimer()
    {
        startTimer = false;
    }
    private static string NameFromIndex(int BuildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }

}
