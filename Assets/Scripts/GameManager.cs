using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    [Header("Main Objects")]
    [SerializeField] private GameObject tutorialScreen;
    [SerializeField] private GameObject winScreen;
    [HideInInspector]public AudioClip winGameVoice;
    [SerializeField] private PlaylistGames playlist;


    [Header("Game Settings")]
    [SerializeField] private bool useFinalVoice;
    public bool canInteract = true;

    [Header("Main Events")]
    [SerializeField] private UnityEvent start;
    [SerializeField] private UnityEvent nextElement;
    [SerializeField] private UnityEvent restart;


    private void Start()
    {
        Intro();
    }
    private void Intro()
    {
        start?.Invoke();      
    }
    
    public void NextElement()
    {
        nextElement?.Invoke();
    } 
    
    public void PlayTutorial()
    {        
        tutorialScreen?.SetActive(true);
    }
    public void ShowTutorial()
    {
        tutorialScreen.SetActive(true);
        tutorialScreen.GetComponent<TutorialScreenNew>().OpenTutorial();
    }
    public void WinScreen()
    {
        if (useFinalVoice)
        {
            StartCoroutine(AudioManager.audioManager.PlayAudioWithEvent(winGameVoice,true,0.5f,()=> ActiveWinScreen()));
        }
        else
        {
            ActiveWinScreen();
        }        
    }

    public void ActiveWinScreen()
    {
        AudioManager.audioManager.PlayAudio(AudioManager.audioManager.winSFX, AudioManager.audioManager.winAS);
        winScreen.SetActive(true);
        winScreen.GetComponent<CanvasGroup>().DOFade(1, 1).OnComplete(()=> playlist.startTimer = true);
    }
    public void RestartGame()
    {
        restart?.Invoke();
    }
    public void EnableInteration()
    {
        canInteract = true;
    }
}
