using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class ClassRoomGame : MonoBehaviour
{
    [Header("Main Objects")]
    [SerializeField] private RectTransform character;
    [SerializeField] private Animator charAnim;
    [SerializeField] private RectTransform board;
    [SerializeField] private RectTransform leftPanel;
    [SerializeField] private RectTransform table;
    //[SerializeField] private CanvasGroup tutorialScreen;
    [SerializeField] private CanvasGroup winScreen;

    [SerializeField] private Button btnCircle;
    [SerializeField] private Button btnTriangle;
    [SerializeField] private Button btnSquare;

    //[SerializeField] private GameObject tutCircle;
    //[SerializeField] private GameObject tutTriangle;
    //[SerializeField] private GameObject tutSquare;
    
    [SerializeField] private ClassRoomShape[] shapesList;



    public GameObject shapeToEnable;
    [SerializeField] private Transform hand;


    [Header("Audio")]  
    [SerializeField] private AudioClip circleName;
    [SerializeField] private AudioClip squareName;
    [SerializeField] private AudioClip triangleName;
    //[SerializeField] private AudioClip[] voices;

    
    public bool cancelActions = true;
    private float initialCharPosX;
    private Vector2 initialBoardPos;
    private float initialPanelPosX;
    private float initialTablePosX;


    private void Start()
    {
        initialCharPosX = character.anchoredPosition.x;
        initialBoardPos = board.anchoredPosition;
        initialPanelPosX = leftPanel.anchoredPosition.x;
        initialTablePosX = table.anchoredPosition.x;

        StartCoroutine(PlayTutorial(1f));
    }    
    IEnumerator PlayTutorial(float delay)
    {
        charAnim.Play("HiCat");
        yield return new WaitForSeconds(delay);
        //AudioManager.audioManager.PlayOneShotVoice(voices[0]);        
        //yield return new WaitForSeconds(voices[0].length + 0.1f);
        //AudioManager.audioManager.PlayOneShotVoice(voices[1]);        
        hand.gameObject.SetActive(true);
        cancelActions = false;
        EnablePanelButtons(true);
    }
    public void EnablePanelButtons(bool enable)
    {
        btnCircle.interactable = enable;
        btnTriangle.interactable = enable;
        btnSquare.interactable = enable;
    }
    public void ElementsMoveOut()
    {
        //AudioManager.audioManager.PlayOneShotVoice(voices[3]);        
        character.DOAnchorPosX(1800,1f).OnComplete(()=> MoveScaleBoard());
        leftPanel.DOAnchorPosX(-800, 1f);
        table.DOAnchorPosX(-1800, 1f);
    }
    void MoveScaleBoard()
    {        
        board.DOScale(new Vector2(1.2f,1.2f),1f);
        board.DOAnchorPos(Vector2.zero, 1f).OnComplete(()=> EnableShape());
    }

    void EnableShape()
    {        
        shapeToEnable.SetActive(true);
        //shapeToEnable.GetComponent<CanvasGroup>().DOFade(3, 1f).OnComplete(()=> ActiveTutorial());
        shapeToEnable.GetComponent<CanvasGroup>().DOFade(3, 1f).OnComplete(()=> StartCoroutine(CloseTutorial()));
    }
    /*public void ActiveTutorial()
    {
        tutorialScreen.gameObject.SetActive(true);
        tutorialScreen.DOFade(1,1f).OnComplete(()=> StartCoroutine(DelayedTutorial()));     
    }*/
    /*IEnumerator DelayedTutorial()
    {
        AudioManager.audioManager.PlayOneShotVoice(voices[4]);       
        yield return new WaitForSeconds(voices[4].length);
        tutorialScreen.DOFade(0, 1f).OnComplete(()=> StartCoroutine(CloseTutorial()));
    }*/
    IEnumerator CloseTutorial()
    {
        //tutCircle.SetActive(false);
        //tutTriangle.SetActive(false);
        //tutSquare.SetActive(false);
        //tutorialScreen.gameObject.SetActive(false);
        //AudioManager.audioManager.PlayOneShotVoice(voices[2]);

        //yield return new WaitForSeconds(voices[2].length+0.05f);
        AudioManager.audioManager.PlayOneShotVoice(shapeToEnable.GetComponent<ElementDrawShape>().buttonShape.shapeName);
        shapeToEnable.transform.DOPunchScale(new Vector2(0.2f,0.2f),0.6f,1);
        yield return new WaitForSeconds(shapeToEnable.GetComponent<ElementDrawShape>().buttonShape.shapeName.length);        
        shapeToEnable.GetComponent<ElementDrawShape>().decortator.SetActive(true);
    }
    public void HideShape()
    {        
        shapeToEnable.GetComponent<CanvasGroup>().DOFade(0, 1f).OnComplete(() => StartCoroutine(ShapeNameTrans()));
    }
    IEnumerator ShapeNameTrans()
    {
        //AudioManager.audioManager.PlayOneShotVoice(voices[7]);

        //yield return new WaitForSeconds(voices[7].length);
        AudioManager.audioManager.PlayOneShotVoice(shapeToEnable.GetComponent<ElementDrawShape>().buttonShape.shapeName);
        shapeToEnable.transform.DOPunchScale(new Vector2(0.2f, 0.2f), 0.6f, 1);
        yield return new WaitForSeconds(shapeToEnable.GetComponent<ElementDrawShape>().buttonShape.shapeName.length);
        ResetElements();
    }
    public void ResetElements()
    {        
        shapeToEnable.SetActive(false);
        character.DOAnchorPosX(initialCharPosX, 1f);
        leftPanel.DOAnchorPosX(initialPanelPosX, 1f).OnComplete(()=> shapeToEnable.GetComponent<ElementDrawShape>().CompleteShape());
        table.DOAnchorPosX(initialTablePosX, 1f);
        board.DOScale(Vector2.one, 1f);
        board.DOAnchorPos(initialBoardPos, 1f);
        CheckAllShapes();
        
    }
    public void CheckAllShapes()
    {
        for (int i = 0; i < shapesList.Length; ++i)
        {
            if (shapesList[i].shapeCompleted == false)
            {
                //AudioManager.audioManager.PlayOneShotVoice(voices[5]);
                return;
            }
        }
        StartCoroutine(PlayWinScreen());
    }
    IEnumerator PlayWinScreen()
    {
        yield return new WaitForSeconds(1.5f);
        //AudioManager.audioManager.PlayOneShotVoice(voices[6]);

        //yield return new WaitForSeconds(1.5f);
        winScreen.gameObject.SetActive(true);
        winScreen.DOFade(1, 1f).OnComplete(()=> PlaylistGames.playlist.StartTimer());
        AudioManager.audioManager.PlayAudio(AudioManager.audioManager.winSFX, AudioManager.audioManager.winAS);

    }
    public void RestartScene() 
    {
        LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name));
    }
}
