using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Food : MonoBehaviour
{
    [SerializeField] private FeedHippoGame feedHippoGame = default;

    [SerializeField] private GameObject box = default;
    [SerializeField] private GameObject globe = default;

    [SerializeField] private Transform tempPos;
    [SerializeField] private Transform foodBallsTrans;

    public Text TextUI;

    public bool selected = false;
    public bool canScaleBox = true;
    public bool resetPos = true;
    public bool isDragged = false;

    private Vector2 initialPos;
    private Vector3 initialScale;
    [SerializeField] Vector3 scale;

    [SerializeField] float scaleSpeed; 
    
    [SerializeField] private AudioClip swoosh;
    [SerializeField] private AudioClip numberSound;

    private int initialSiblingIndex;

    private void Start()
    {
        initialSiblingIndex = transform.GetSiblingIndex();
        initialPos = transform.position;
        initialScale = transform.localScale;
    }    
    public void OnDrag()
    {
        if (!feedHippoGame.cancelActions)
        {          
#if UNITY_EDITOR
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#else
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position); 
#endif
            isDragged = true;
            GetComponent<RectTransform>().position = mousePos;
            //ScaleBox();
        }
    }
    public void OnEndDrag()
    {
        if (resetPos && isDragged)
        {
            ResetBallPos();
        }
    }
    public void SetInitialPos()
    {
        transform.position = initialPos;
    }
    public void SetInitialScale()
    {
        transform.localScale = initialScale;
    }
    public void SetInitialSilbling()
    {
        transform.SetParent(foodBallsTrans);
        transform.SetSiblingIndex(initialSiblingIndex);
    }
    private void ResetBallPos()
    { 
        AudioManager.audioManager.PlayOneShotAS(swoosh);
        GetComponent<RectTransform>().DOMove(initialPos, 0.2f).OnComplete(() => EnableCollider());
    }
    public void PlayVoice()
    {
        AudioManager.audioManager.PlayOneShotVoice(numberSound);

    }
    void EnableCollider()
    {
        isDragged = false;
        transform.SetParent(foodBallsTrans);
        transform.SetSiblingIndex(initialSiblingIndex);
        //box.transform.DOPunchScale(new Vector3(0.15f, -0.05f, 0f), 0.5f, 2).OnComplete(()=> ActiveActions());
        globe.SetActive(true);
        if (!GetComponent<Collider2D>().isActiveAndEnabled)
        {
            GetComponent<Collider2D>().enabled = true;
        }        
    }
    void ActiveActions()
    {
        feedHippoGame.cancelActions = false;
        box.transform.localScale = Vector3.one;
    }
    public void ScaleBox()
    {
        if (canScaleBox)
        {
            canScaleBox = false;
            box.transform.DOKill();
            box.transform.DOPunchScale(new Vector3(-0.05f, 0.15f, 0), 0.5f, 2);
            transform.SetParent(tempPos);
        }
    }
    public void OnPointerDown()
    {
        if (!feedHippoGame.cancelActions)
        {
            globe.SetActive(false);
            transform.DOScale(scale, scaleSpeed);    
        }
    }
    public void OnPointerUp()
    {
        if (resetPos && isDragged)
        {
            transform.DOScale(Vector3.one, scaleSpeed);
            canScaleBox = true;
        }
    }
}
