using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UI;

public class ElementDrawShape : MonoBehaviour
{
    [SerializeField] private ClassRoomGame classRoomGame;
    [SerializeField] private DrawShape drawGame;
    [SerializeField] private DrawLineShape drawLine;
    [SerializeField] private Collider2D checkMouseColl;
    [SerializeField] private Transform localLineParent;
    public ButtonShape buttonShape;
    [SerializeField] private GameObject star; 
    [SerializeField] private GameObject starWin;
    [SerializeField] private Vector2 starWinInitPos;
    [SerializeField] private bool firstPointTouched;
    [SerializeField] private bool midPointTouched;
    [SerializeField] private int stepsCount;
    [SerializeField] private GameObject firsPoint;
    [SerializeField] private GameObject midPoint;
    [SerializeField] private GameObject lastPoint;
    public GameObject decortator;
    [SerializeField] private ParticleSystem starPS;

    [Header("Audio")]
    [SerializeField] private AudioSource effectsAS;
    [SerializeField] private AudioClip starAC;
    [SerializeField] private AudioClip swooshAC;
    [SerializeField] private AudioClip popAC;

    [SerializeField] private GameObject[] steps;
    [SerializeField] private BezierSpline[] handPaths;

    [SerializeField] private UnityEvent onCompleteShape;

    public bool shapeCompleted;

    private void OnEnable()
    {
        //stepsCount = steps.Length;
        drawLine.lineParent = localLineParent;
        drawGame.hand.spline = handPaths[stepsCount];
    }

    private void Start()
    {
        //checkMouseColl = GetComponent<Collider2D>();
        starWinInitPos = starWin.transform.position;
    }
    public void FirstPoint()
    {
        firstPointTouched = true;
        drawLine.canDraw = true;
    }
    public void MiddlePoint()
    {
        midPointTouched = true;
    }
    public void LastPoint()
    {
        if (firstPointTouched && midPointTouched)
        {
            AudioManager.audioManager.PlayEffect(AudioEffectType.correct);
            firstPointTouched = false;
            midPointTouched = false;
            drawLine.lastPointReached = true;
            drawLine.canDraw = false;
            drawGame.activeHand = false;
            shapeCompleted = true;
            //ResetTotalElement();
            //ResetPoints();
            onCompleteShape?.Invoke();
            
            //NextStep();
        }
        //else
        //{
        //    ResetPoints();
        //}
    }
    public void SinglePoint()
    {
        AudioManager.audioManager.PlayEffect(AudioEffectType.correct);
        firstPointTouched = false;
        midPointTouched = false;
        drawLine.lastPointReached = true;
        drawLine.canDraw = false;
        drawGame.activeHand = false;
        NextStep();
    }

    private void NextStep()
    {
        if (stepsCount < steps.Length - 1)
        {
            Debug.Log("Next Step");
            steps[stepsCount].SetActive(false);
            stepsCount++;
            steps[stepsCount].SetActive(true);
            drawGame.hand.spline = handPaths[stepsCount];
            drawGame.hand.gameObject.SetActive(true);
            drawGame.hand.ResetWalkerPos();
            drawGame.activeHand = true;
        }
        //else
        //{
        //    steps[stepsCount].SetActive(false);
        //    checkMouseColl.enabled = false;
        //    //drawGame.CheckAllElements();
        //}
    }
    public void ResetElement()
    {
        firstPointTouched = false;
        midPointTouched = false;
        drawLine.canDraw = false;
    }
    public void ResetPoints()
    {
        firstPointTouched = false;
        midPointTouched = false;
        drawLine.lastPointReached = false;
        drawLine.canDraw = true;
        drawGame.activeHand = true;
        firsPoint.SetActive(true);
        midPoint.SetActive(false);
        lastPoint.SetActive(false);
    }
    public void ResetTotalElement()
    {
        ResetElement();
        int childs = localLineParent.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.Destroy(localLineParent.GetChild(i).gameObject);
        }
        stepsCount = 0;
        steps[stepsCount].SetActive(true);
    }
    public void CompleteShape()
    {
        ResetPoints();
        ResetTotalElement();
        buttonShape.shapeCompleted = true;
        StarWinAnim();
    }
    public void StarWinAnim()
    {
        AudioManager.audioManager.PlayAudio(starAC, AudioManager.audioManager.effectsAS);        
        starWin.SetActive(true);
        starPS.Play();
        starWin.transform.DOScale(new Vector2(1,1), 1f).OnComplete(()=> MoveStarWin());
    }
    void MoveStarWin()
    {
        AudioManager.audioManager.PlayAudio(swooshAC, AudioManager.audioManager.effectsAS);        
        starWin.transform.DOScale(new Vector2(0.5f,0.5f), 1f);
        starWin.transform.DOMove(star.transform.position, 0.5f).OnComplete(() => ActiveStar());
    }
    void ActiveStar()
    {
        AudioManager.audioManager.PlayAudio(popAC, AudioManager.audioManager.effectsAS);        
        starWin.transform.localScale = Vector2.zero;
        starWin.transform.position = starWinInitPos;        
        starWin.SetActive(false);
        star.SetActive(true);
        star.transform.DOPunchScale(new Vector2(1f,1f),0.8f,1);
        classRoomGame.EnablePanelButtons(true);
        buttonShape.GetComponent<Button>().interactable = false;
    }
}
