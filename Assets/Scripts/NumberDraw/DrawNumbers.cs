using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DrawNumbers : MonoBehaviour
{
    [SerializeField] private DrawLineNumber drawLine;
    [SerializeField] private Button restartBtn;
    public SplineWalker hand;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject tutorialScreen;
    [SerializeField] private Transform leftPos;
    [SerializeField] private Transform centerPos;
    [SerializeField] private Transform rightPos;
    [SerializeField] private ElementDrawNumber[] elementsDraw;
    [SerializeField] private Image backgroundImg;
    [SerializeField] private ParticleSystem starsPS;
    [SerializeField] private Sprite[] backgrounds;
    [SerializeField] private GameObject[] itemsToHide;
    

    public int elementsQuantity;
    public int elementsCounter;
    public bool activeHand;

    [Header("Audio")]    
    public AudioClip swooshAC;
    public AudioClip boingAC;
    [SerializeField] private AudioSource voice;
    [SerializeField] private AudioClip[] voices;
    [SerializeField] private AudioClip[] elementNames;
    [SerializeField] private AudioClip[] nextElements;
    [SerializeField] private AudioClip elementToDraw;
    [SerializeField] private string sceneToLoad;

    private void Start()
    {
        elementsQuantity = elementsDraw.Length;
        elementsDraw[elementsCounter].gameObject.SetActive(true);

        //StartCoroutine(PlayTutorial(1));
        Invoke(nameof(IntroAnim), 1f);
        StartCoroutine(PlayElementName(1));
    }
    public void NextElement()
    {
        //StartCoroutine(PlayElementName(0));
        elementsDraw[elementsCounter].transform.DOPunchScale(new Vector2(0.3f, 0.3f), 0.5f, 2)
            .OnComplete(() => MoveActualLetter());
        //Invoke(nameof(IntroAnim), 1f);
    }
    void MoveActualLetter()
    {
        elementsDraw[elementsCounter].transform.DOMove(leftPos.position, 0.5f)
            .OnComplete(() => ShowNextLetter());
    }
    
    void ShowNextLetter()
    {
        elementsDraw[elementsCounter].gameObject.SetActive(false);
        elementsCounter++;
        elementsDraw[elementsCounter].gameObject.SetActive(true);
        backgroundImg.sprite = backgrounds[elementsCounter];
        Invoke(nameof(IntroAnim), 1f);
    }

    public void CheckAllElements()
    {
        if (elementsCounter < elementsQuantity - 1)
        {
            NextElement();
        }
        else
        {
            elementsDraw[elementsCounter].transform.DOPunchScale(new Vector2(0.3f, 0.3f), 0.5f, 2);
            Invoke(nameof(ShowWinScreen), 1);
        }
    }
    void ShowWinScreen()
    {
        AudioManager.audioManager.PlayAudio(AudioManager.audioManager.winSFX, AudioManager.audioManager.winAS);
        winScreen.SetActive(true);
        winScreen.GetComponent<CanvasGroup>().DOFade(1, 1).OnComplete(()=> PlaylistGames.playlist.StartTimer());
        //restartBtn.interactable = true;
    }
    private void IntroAnim()
    {
        AudioManager.audioManager.PlayEffect(AudioEffectType.swoosh);        
        elementsDraw[elementsCounter].initialPos.gameObject.SetActive(false);
        elementsDraw[elementsCounter].transform.DOScale(1, 0.59f);
        //StartCoroutine(PlayElementName(0));
        elementsDraw[elementsCounter].transform.DOMove(centerPos.position, 0.6f).OnComplete(() => ElementPunchScale());
    }
    private void ElementPunchScale()
    {
        AudioManager.audioManager.PlayAudio(boingAC, AudioManager.audioManager.effectsAS);        
        elementsDraw[elementsCounter].transform.DOPunchScale(new Vector2(0.3f, 0.3f), 0.5f, 2).OnComplete(() => InitElement());
    }
    void InitElement()
    {
        elementsDraw[elementsCounter].decorator.SetActive(true);
        //hand.gameObject.SetActive(true);
    }
    public void ResetGame()
    {

       foreach (ElementDrawNumber element in elementsDraw)
       {
           element.gameObject.SetActive(false);
           element.transform.position = rightPos.position;
           element.ResetTotalElement();
       }
       elementsCounter = 0;
       backgroundImg.sprite = backgrounds[elementsCounter];
       activeHand = true;
       elementsDraw[elementsCounter].gameObject.SetActive(true);
       drawLine.canDraw = true;
       drawLine.lastPointReached = false;
       winScreen.SetActive(false);
       Invoke(nameof(RestartWinScreen), 2);
       HideItems();
       StartCoroutine(PlayElementName(1f));
       Invoke(nameof(IntroAnim), 2f);
    }
    void RestartWinScreen()
    {
        restartBtn.interactable = true;
        winScreen.GetComponent<CanvasGroup>().alpha = 0;
    }

    void ActiveTutorial()
    {
        tutorialScreen.SetActive(true);
        tutorialScreen.GetComponent<CanvasGroup>().DOFade(1, 1);
    }
    void DisableTutorial()
    {
        tutorialScreen.GetComponent<CanvasGroup>().DOFade(0, 0.5f).OnComplete(() => tutorialScreen.SetActive(false));
    }

    IEnumerator PlayTutorial(float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.audioManager.PlayOneShotVoice(voices[0]);        
        yield return new WaitForSeconds(voices[0].length + 0.01f);
        ActiveTutorial();
        AudioManager.audioManager.PlayOneShotVoice(voices[1]);
        yield return new WaitForSeconds(voices[1].length + 0.01f);
        StartCoroutine(PlayElementName(0));
        Invoke(nameof(IntroAnim), 2f);
        DisableTutorial();
    }
    IEnumerator PlayElementName(float delay)
    {
        //int randomNum = 0;
        //randomNum = Random.Range(0, nextElements.Length);
        yield return new WaitForSeconds(delay);
        //AudioManager.audioManager.PlayOneShotVoice(nextElements[randomNum]);
        //yield return new WaitForSeconds(nextElements[randomNum].length);
        //AudioManager.audioManager.PlayOneShotVoice(elementToDraw);
        //yield return new WaitForSeconds(elementToDraw.length);
        AudioManager.audioManager.PlayOneShotVoice(elementNames[elementsCounter]);       
    }
    void HideItems()
    {
        foreach (GameObject item in itemsToHide)
        {
            item.SetActive(false);
        }
    }

    public void RestartScene()
    {
        LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name));
    }
}
