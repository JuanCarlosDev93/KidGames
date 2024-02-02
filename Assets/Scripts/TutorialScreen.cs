using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialScreen : MonoBehaviour
{
    [SerializeField] private FeedHippoGame feedHippoGame;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject hand;
    [SerializeField] private Transform initialPos;
    [SerializeField] private Transform endPos;

    [SerializeField] private float handSpeed = 1f;

    [Header("Fade Settings")]
    [SerializeField] private float fadeSpeed;

    private void Start()
    {
        hand.transform.position = initialPos.position;
        hand.transform.DOMove(endPos.position, handSpeed).SetLoops(-1,LoopType.Restart);
    }

    public void FadeCG(int endValue)
    {
        canvasGroup.DOFade(endValue,fadeSpeed);
    }
    public void CloseTut(int endValue)
    {
        canvasGroup.DOFade(endValue, fadeSpeed).OnComplete(()=> gameObject.SetActive(false));
        feedHippoGame.cancelActions = false;
    }
}
