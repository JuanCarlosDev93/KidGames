using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementDrawNumber : MonoBehaviour
{
    [SerializeField] private DrawNumbers drawGame;
    [SerializeField] private DrawLineNumber drawLine;
    [SerializeField] private Collider2D checkMouseColl;
    public GameObject decorator;
    public GameObject star;
    public Transform localLineParent;
    public Transform initialPos;
    [SerializeField] private Vector2 initialScale;
    [SerializeField] private bool firstPointTouched;
    [SerializeField] private bool midPointTouched;
    [SerializeField] private int stepsCount;
    [SerializeField] private GameObject[] steps;
    [SerializeField] private BezierSpline[] handPaths;



    private void OnEnable()
    {
        //stepsCount = steps.Length;

        drawLine.elementDraw = this;
        drawLine.lineParent = localLineParent;
        drawGame.hand.spline = handPaths[stepsCount];
    }

    private void Start()
    {
        //checkMouseColl = GetComponent<Collider2D>();
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
            NextStep();
        }
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
        else
        {
            steps[stepsCount].SetActive(false);
            checkMouseColl.enabled = false;
            drawGame.CheckAllElements();
        }
    }
    private void ResetElementPoints()
    {
        steps[stepsCount].GetComponent<StepDraw>().ResetPoints();
    }
    public void ResetElement()
    {
        firstPointTouched = false;
        midPointTouched = false;
        drawLine.canDraw = false;
        ResetElementPoints();
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
}
