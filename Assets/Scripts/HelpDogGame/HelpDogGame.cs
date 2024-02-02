
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Playables;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class HelpDogGame : MonoBehaviour
{
    [Header("Main Objects")]
    [SerializeField] private GameManager gameManager;

    [SerializeField] private GameObject dog;
    [SerializeField] private GameObject dogSad;
    [SerializeField] private GameObject cactus;
    [SerializeField] private GameObject can;
    [SerializeField] private GameObject medKit;
    [SerializeField] private TextMeshPro numberTxt;
    [SerializeField] private TextMeshPro numberScarTxt;
    [SerializeField] private PlayableDirector dogPlayable;
    [SerializeField] private GameObject crashFX;
    [SerializeField] private Transform heartPos;

    [SerializeField] private Image transitionImage;

    [Header("Objects Data")]
    [SerializeField] private Spike[] spikes;
    [SerializeField] private Spike[] bandages;
    [SerializeField] private Scar[] scars;
    [SerializeField] private List<Spike> randomSpikes = new List<Spike>();
    [SerializeField] private List<Spike> randomBandages = new List<Spike>();
    [SerializeField] private List<Scar> randomScars= new List<Scar>();
    [SerializeField] private List<int> tempNumbers = new List<int>();

    [SerializeField] private int selectedElement = 0;

    [Header("Audio")]
    [SerializeField] private AudioClip[] tutorialVoice;

    [Header("Game State")]
    [SerializeField] private bool firstPart = true;

    private void Start()
    {
        gameManager.winGameVoice = tutorialVoice[tutorialVoice.Length - 1];
        StartGame();
    }

    public void IntroAnim()
    {
        dogPlayable.Play();
    }
    private void StartGame()
    {
        SetSpikes();
    }
    private void PlayCrashFX()
    {
        crashFX.SetActive(true);
    }
    private void SetSpikes()//Unificar con SetScars
    {
        for (int i = 0; i < spikes.Length; i++)
        {
            UniqueRandomNumber.uniqueRandomNumber.GenerateRandomNumber(0, 5, tempNumbers);
            spikes[tempNumbers[i]].textM.text = (i+1).ToString();
            randomSpikes.Add(spikes[tempNumbers[i]]);
        }
        randomSpikes[selectedElement].isSelected = true;
        numberTxt.text = randomSpikes[selectedElement].textM.text;
        tempNumbers.Clear();
    }

    private void SetScarsBands()
    {
        for (int i = 0; i < scars.Length; i++)
        {
            UniqueRandomNumber.uniqueRandomNumber.GenerateRandomNumber(0, 5, tempNumbers);
            bandages[tempNumbers[i]].textM.text = (i + 6).ToString();
            scars[tempNumbers[i]].textM.text = (i + 6).ToString();
            scars[tempNumbers[i]].textM.gameObject.SetActive(true);
            randomBandages.Add(bandages[tempNumbers[i]]);
            randomScars.Add(scars[tempNumbers[i]]);
        }
        randomScars[selectedElement].isSelected = true;
        randomBandages[selectedElement].isSelected = true;
        numberScarTxt.text = randomScars[selectedElement].textM.text;
        tempNumbers.Clear();
    }

    public void NextElement()//Unificar con NextElementScar
    {
        if (firstPart)
        {
            if (selectedElement < spikes.Length - 1)
            {
                randomSpikes[selectedElement].isSelected = false;
                selectedElement++;
                randomSpikes[selectedElement].isSelected = true;
                numberTxt.text = randomSpikes[selectedElement].textM.text;
            }
            else
            {
                //dogSad.GetComponent<Animator>().SetBool("Win", true);
                //gameManager.WinScreen();
                //selectedElement = 0;
                ActiveNextSection();

            }
        }
        
        
    }
    public void NextElementScar()
    {
        if (!firstPart)
        {
            if (selectedElement < scars.Length - 1)
            {
                randomScars[selectedElement].isSelected = false;
                randomBandages[selectedElement].isSelected = false;
                selectedElement++;
                randomScars[selectedElement].isSelected = true;
                randomBandages[selectedElement].isSelected = true;
                numberScarTxt.text = randomScars[selectedElement].textM.text;
            }
            else
            {
                dogSad.GetComponent<Animator>().SetBool("Win", true);
                gameManager.WinScreen();
            }
        }
        

    }

    private void ActiveNextSection()
    {
        firstPart = false;
        selectedElement = 0;
        MoveCanOut();         
    }

    void MoveCanOut()
    {        
        can.transform.DOMoveX(-20,1.5f).OnComplete(()=> MediKitIn());
    }
    void MediKitIn()
    {
        can.SetActive(false);
        SetScarsBands();
        medKit.SetActive(true);
        medKit.transform.DOMoveX(-8, 1f).OnComplete(()=> SetBandages());
    }

    void SetBandages()
    {
        foreach (Scar scar in scars)
        {
            scar.gameObject.SetActive(true);
        }
        foreach (Spike bandage in bandages)
        {
            bandage.GetComponent<ObjectHandler>().SetInitialPos();
        }
    }
    
    
    public void ActiveGame()
    {
        
        FadeTransitionIn();
               
    }

    public void FadeTransitionIn()
    {
        transitionImage.gameObject.SetActive(true);
        transitionImage.DOFade(1, 0.5f).OnComplete(()=> FadeTransitionOut());
    }
    public void FadeTransitionOut()
    {
        cactus.SetActive(false);
        can.SetActive(true);
        dogSad.SetActive(true);
        crashFX.SetActive(false);
        transitionImage.DOFade(0, 0.5f).OnComplete(() => StartNextGame());
        dog.SetActive(false);
    }
    void StartNextGame()
    {
        transitionImage.gameObject.SetActive(false);
        gameManager.EnableInteration();
        //StartCoroutine(AudioManager.audioManager.PlayAudioWithEvent(tutorialVoice[2], true, 0.02f,() => gameManager.EnableInteration()));

    }
    public void ActiveTutorial()
    {
        //AudioManager.audioManager.PlayOneShotVoice(tutorialVoice[3]);
        //gameManager.PlayTutorial();    
        gameManager.ShowTutorial();

    }
    public void PlayHeart()
    {
        EffectManager.effectManager.PlayEffect(Effects.hearts,heartPos);
    }
    public void RestartGame()
    {       
        LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name));
    }
}
