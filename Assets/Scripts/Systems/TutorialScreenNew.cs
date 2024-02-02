using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class TutorialScreenNew : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject hand;
    [SerializeField] private Transform initialPos;
    [SerializeField] private Transform endPos;

    [SerializeField] private float handSpeed = 1f;
    [SerializeField] private bool useGameManager;

    [Header("Fade Settings")]
    [SerializeField] private float fadeSpeed;

    [SerializeField] private bool autoCloseTut;
    private float timer;
    private float tutorialDuration = 3;
    [SerializeField] private UnityEvent onCloseTutorial;
    [SerializeField] private AudioClip tutorialAC;
    [SerializeField] private bool useTutVoice;
    [SerializeField] private bool staticHand;


    private void Update()
    {
        if (autoCloseTut)
        {
            if (timer < tutorialDuration)
            {
                timer += Time.deltaTime;
            }
            else
            {
                autoCloseTut = false;
                CloseTut(0);
                timer = 0;
            }
        }

    }

    private void Start()
    {
        //FadeCG(1);
        hand.transform.position = initialPos.position;
        if (!staticHand)
        {
            hand.transform.DOMove(endPos.position, handSpeed).SetLoops(-1, LoopType.Restart);
        }
    }

    public void FadeCG(int endValue)
    {
        canvasGroup.DOFade(endValue,fadeSpeed);
    }

    public void OpenTutorial()
    {
        GetComponent<CanvasGroup>().DOFade(1, 1)
            .OnComplete(() => FadeOutTut());
    }
    void FadeOutTut()
    {
        if (useTutVoice)
        {
            AudioManager.audioManager.PlayAudio(tutorialAC, AudioManager.audioManager.voiceAS);
        }
        SetAutoClose(tutorialAC.length);
    }
    public void CloseTut(int endValue)
    {
        if (useGameManager)
        {
            gameManager.canInteract = true;
        }        
        canvasGroup.DOFade(endValue, fadeSpeed).OnComplete(()=> OnCloseTut());
        
    }

    private void OnCloseTut()
    {
        onCloseTutorial?.Invoke();
        gameObject.SetActive(false);
    }
    public void SetAutoClose(float timeToClose)
    {
        autoCloseTut = true;
        tutorialDuration = timeToClose;
    }
}
