using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DolphinColorsGame : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    public GameObject uICounter;
    [SerializeField] private int remainingGlobes;
    [SerializeField] private int globesCounter;
    [SerializeField] private GlobeSpawner spawner;
    [SerializeField] private PlayableDirector dolphinPlayable;
    //[Header("Audio")]
    //[SerializeField] private AudioClip[] tutorialVoice;

    public void StartTutorial()
    {
        StartGame();
        //AudioManager.audioManager.PlayOneShotVoice(tutorialVoice[3]);
        gameManager.ShowTutorial();
    }
    public void StartGame()
    {
        spawner.StartSpawn();
    }
    public void GlobeCounter()
    {
        if (globesCounter < remainingGlobes)
        {
            globesCounter++;
        }
        else
        {
            globesCounter = 0;
            spawner.activeSpawner = false;
            Invoke(nameof(ShowWinScreen), 1.5f);
        }
    }

    void ShowWinScreen()
    {
        gameManager.WinScreen();
    }
    
}
