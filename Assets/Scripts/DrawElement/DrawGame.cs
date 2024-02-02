using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DrawGame : MonoBehaviour
{
    [SerializeField] private DrawLine drawLine;
    [SerializeField] private LocalLoading localLoadScreen;
    [SerializeField] private Button restartBtn;
    public SplineWalker hand;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject tutorialScreen;
    [SerializeField] private Transform leftPos;
    [SerializeField] private Transform centerPos;
    [SerializeField] private Transform rightPos;
    [SerializeField] private ElementDraw[] elementsDraw;
    [SerializeField] private Image backgroundImg;
    [SerializeField] private ParticleSystem starsPS;
    [SerializeField] private Sprite[] backgrounds;
    [SerializeField] private GameObject[] itemsToHide;

    public int elementsQuantity;
    public int elementsCounter;
    public bool activeHand;

    [Header("Audio")]    
    
    public AudioClip starAC;
    public AudioClip swooshAC;
    public AudioClip boingAC;
    public AudioClip jingleChimeAC;
    [SerializeField] private AudioClip[] voices;
    [SerializeField] private AudioClip[] elementNames;
    [SerializeField] private AudioClip[] nextElements;
    [SerializeField] private AudioClip elementToDraw;

    private void Start()
    {
        elementsQuantity = elementsDraw.Length;
        elementsDraw[elementsCounter].gameObject.SetActive(true);

        //StartCoroutine(PlayTutorial(1));
        //StartCoroutine(PlayElementName(0));
        Invoke(nameof(IntroAnim), 0.5f);
    }
    public void NextElement()
    {
        //StartCoroutine(PlayElementName(0));
        elementsDraw[elementsCounter].transform.DOPunchScale(new Vector2(0.3f, 0.3f), 0.5f, 2)
            .OnComplete(()=> MoveActualLetterNew());   
        //Invoke(nameof(IntroAnim), 1f);
    }
    void MoveActualLetter()
    {

        elementsDraw[elementsCounter].transform.DOMove(leftPos.position, 0.5f)
            .OnComplete(() => ShowNextLetter());
    }
    void MoveActualLetterNew()
    {
        AudioManager.audioManager.PlayOneShotVoice(swooshAC);
        elementsDraw[elementsCounter].localLineParent.gameObject.SetActive(false);
        elementsDraw[elementsCounter].transform.DOScale(0.15f, 0.45f);
        elementsDraw[elementsCounter].transform.DOMove(elementsDraw[elementsCounter].initialPos.position, 0.5f)
            .OnComplete(() => ElementScalePunchBack());
        //.OnComplete(() => ShowNextLetter());
    }
    void ElementScalePunchBack()
    {
        AudioManager.audioManager.PlayOneShotVoice(jingleChimeAC);
        elementsDraw[elementsCounter].transform.DOPunchScale(new Vector2(0.1f, 0.1f), 0.6f, 2).OnComplete(() => ShowStar());
    }
    void ShowStar()
    {
        elementsDraw[elementsCounter].initialPos.gameObject.SetActive(true);
        
        elementsDraw[elementsCounter].gameObject.SetActive(false);
        elementsDraw[elementsCounter].star.SetActive(true);
        starsPS.transform.position = elementsDraw[elementsCounter].star.transform.position;
        starsPS.Play();
        AudioManager.audioManager.PlayOneShotVoice(starAC);
        elementsDraw[elementsCounter].star.transform.DOPunchScale(new Vector2(0.2f,0.2f),2,1).OnComplete(()=> StartCoroutine(ShowNextLetter()));

    }
    IEnumerator ShowNextLetter()
    {
        //elementsDraw[elementsCounter].initialPos.gameObject.SetActive(true);
        //elementsDraw[elementsCounter].gameObject.SetActive(false);
        
        elementsCounter++;
        elementsDraw[elementsCounter].gameObject.SetActive(true);
        //StartCoroutine(PlayElementName(0));
        yield return new WaitForSeconds(0.25f);
        //backgroundImg.sprite = backgrounds[elementsCounter];
        Invoke(nameof(IntroAnim), 0.25f);
    }
    void ShowNextLetterNew()
    {

    }

    public void CheckAllElements()
    {
        if (elementsCounter < elementsQuantity-1)
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
        AudioManager.audioManager.PlayOneShotAS(swooshAC);
        elementsDraw[elementsCounter].initialPos.gameObject.SetActive(false);
        elementsDraw[elementsCounter].transform.DOScale(1,0.59f).OnComplete(()=> StartCoroutine(PlayElementName(0)));
        elementsDraw[elementsCounter].transform.DOMove(centerPos.position, 0.7f).OnComplete(()=> ElementPunchScale());
    }
    private void ElementPunchScale()
    {        
        AudioManager.audioManager.PlayOneShotVoice(boingAC);
        elementsDraw[elementsCounter].transform.DOPunchScale(new Vector2(0.3f,0.3f), 0.5f, 2).OnComplete(()=> InitElement());
    }
    void InitElement()
    {
        elementsDraw[elementsCounter].decorator.SetActive(true);
        //hand.gameObject.SetActive(true);
    }
    public void ResetGame()
    {
        LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name));
        //localLoadScreen.StartLoading();
        //foreach (ElementDraw element in elementsDraw)
        //{
        //    element.gameObject.SetActive(false);
        //    element.transform.position = rightPos.position;           
        //    element.ResetTotalElement();
        //}
        //elementsCounter = 0;
        //backgroundImg.sprite = backgrounds[elementsCounter];
        //activeHand = true;
        //elementsDraw[elementsCounter].gameObject.SetActive(true);        
        //drawLine.canDraw = true;
        //drawLine.lastPointReached = false;        
        //winScreen.SetActive(false);
        //Invoke(nameof(RestartWinScreen), 2);
        //HideItems();
        //StartCoroutine(PlayElementName(1f));
        //Invoke(nameof(IntroAnim), 2f);
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
        tutorialScreen.GetComponent<CanvasGroup>().DOFade(0, 0.5f).OnComplete(()=> tutorialScreen.SetActive(false));
    }

    IEnumerator PlayTutorial(float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.audioManager.PlayOneShotVoiceNew(voices[0]);
        yield return new WaitForSeconds(voices[0].length + 0.01f);
        ActiveTutorial();        
        AudioManager.audioManager.PlayOneShotVoiceNew(voices[1]);
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
        //AudioManager.audioManager.PlayOneShotVoiceNew(nextElements[randomNum]);
        //yield return new WaitForSeconds(nextElements[randomNum].length);        
        //AudioManager.audioManager.PlayOneShotVoiceNew(elementToDraw);
        //yield return new WaitForSeconds(elementToDraw.length);
        AudioManager.audioManager.PlayOneShotVoiceNew(elementNames[elementsCounter]);
    }
    void HideItems()
    {
        foreach (GameObject item in itemsToHide)
        {
            item.SetActive(false);
        }
    }
}
