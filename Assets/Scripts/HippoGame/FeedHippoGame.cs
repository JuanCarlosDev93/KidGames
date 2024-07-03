using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class FeedHippoGame : MonoBehaviour
{
    [Header("Main Objects")]
    [SerializeField] private GameObject winScreen;
    //[SerializeField] private GameObject tutScreen;
    [SerializeField] private GameObject hippo;
    [SerializeField] private GameObject globe;
    [SerializeField] private Text globeText;
    [SerializeField] private Button restartBtn;


    [SerializeField] private Food[] foodBalls;
    [SerializeField] private int foodBallIndex;

    [Header("Audio")]    
    [SerializeField] private AudioClip pop;
    //[SerializeField] private AudioClip[] tutorialVoice;

    [Header("Game States")]
    public bool cancelActions = false;

    private void Start()
    {
        //cancelActions = true;
        InitializeGame();
        //StartCoroutine(PlayVoice(tutorialVoice[0], 1f));
        //StartCoroutine(PlayTutScreen(tutorialVoice[0].length));        
    }

    void InitializeGame()
    {
        globeText.text = (foodBallIndex + 1).ToString();
        foodBalls[foodBallIndex].gameObject.SetActive(true);
        foodBalls[foodBallIndex].selected = true;
    }

    void NextFoodBall()
    {
        foodBallIndex++;
        globeText.text = (foodBallIndex + 1).ToString();
        foodBalls[foodBallIndex].selected = true;
    }
    
    public void CheckAllFood()
    {
        if (foodBallIndex >= foodBalls.Length-1)
        {
            globe.SetActive(false);
            hippo.GetComponent<Hippo>().SetHappyHippo();
            StartCoroutine(ShowWinScreen(1.5f));
        }
        else
        {
            NextFoodBall();
        }
    }    
    IEnumerator PlayVoice(AudioClip audio, float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.audioManager.PlayOneShotVoice(audio);     
    }
    IEnumerator ShowWinScreen(float delay)
    {
        yield return new WaitForSeconds(delay);
        //StartCoroutine(PlayVoice(tutorialVoice[1], 1f));
        AudioManager.audioManager.PlayOneShotAS(AudioManager.audioManager.winSFX);
        winScreen.SetActive(true);
        winScreen.GetComponent<CanvasGroup>().DOFade(1, 1).OnComplete(()=> PlaylistGames.playlist.startTimer = true);
        restartBtn.interactable = true;

    }
    public void ActiveActions()
    {
        cancelActions = false;
    }
    public void RestartGame()
    {
        LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name));
    }
}
