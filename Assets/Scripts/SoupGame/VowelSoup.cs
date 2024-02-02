using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class VowelSoup : MonoBehaviour
{

    [SerializeField] private SoupGame soupGame = default;
    [SerializeField] private VowelsController vowelsController;
    //[SerializeField] private GameObject pot = default;
    [SerializeField] private SpriteRenderer vowelSpriteRndr = default;
    [SerializeField] private Transform initialPos;
    [SerializeField] private Vector3 initialScale;    
    [SerializeField] Vector3 scale;
    [SerializeField] float scaleSpeed;
    [SerializeField] private AudioClip vowelSound;    
    [SerializeField] private AudioClip vowelCorrect;    
    [SerializeField] private AudioClip vowelIncorrect;
    [SerializeField] private string animCorrectName;
    public bool canScalePot = true;
    public bool canMove;
    public bool selected = false;

    private int initialSiblingIndex;
    private Color initialColor;

    private void Start()
    {
        //vowelSpriteRndr = GetComponent<SpriteRenderer>();
        //initialSiblingIndex = transform.GetSiblingIndex();
        //initialPos = GetComponent<RectTransform>().anchoredPosition;
        //initialScale = transform.localScale;
        initialColor = GetComponent<SpriteRenderer>().color;
        canMove = true;
    }

    public void OnClickVowel()
    {
        if (selected)
        {
            vowelsController.PlayVowelCorrect(animCorrectName);
            soupGame.playingIntro = false;
            AudioManager.audioManager.PlayOneShotAS(vowelCorrect);
            EffectManager.effectManager.PlayEffect(Effects.stars, transform);
            print("Correct");
        }
        else
        {
            AudioManager.audioManager.PlayOneShotAS(vowelIncorrect);
            print("Incorrect");
        }
    }
    
    public void SetInitialTransform()
    {
        transform.position = initialPos.position;
        transform.localScale = initialScale;
    }  

    public void OnDrag()
    {
        if (!soupGame.cancelActions )
        {
            if (canMove)
            {
#if UNITY_EDITOR
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#else
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position); 
#endif
                GetComponent<RectTransform>().position = mousePos;
                //ScalePot();
            }

        }
    }
    public void OnEndDrag()
    {
        if (!soupGame.cancelActions)
        {
            if (canMove)
            {
                ResetBallPos();

            }
        }
    }
    public void SetVowelData(Sprite vowelSprt, AudioClip vowelSnd)
    {
        vowelSpriteRndr.sprite = vowelSprt;
        vowelSound = vowelSnd;
    }
    public void SetInitialPos()
    {
        //GetComponent<RectTransform>().anchoredPosition = initialPos;
    }
    public void SetInitialScale()
    {
        transform.localScale = initialScale;
    }
    public void SetInitialSilbling()
    {
        //transform.SetParent(vowelTempTrans);
        transform.SetSiblingIndex(initialSiblingIndex);
    }
    private void ResetBallPos()
    {
        AudioManager.audioManager.PlayEffect(AudioEffectType.swoosh);        
        //GetComponent<RectTransform>().DOAnchorPos(initialPos, 0.2f).OnComplete(() => EnableCollider());
    }
    public void PlayVoice()
    {
        AudioManager.audioManager.PlayOneShotVoice(vowelSound);
        
    }
    void EnableCollider()
    {
        //transform.SetParent(vowelTempTrans);
        transform.SetSiblingIndex(initialSiblingIndex);
        //pot.transform.DOPunchScale(new Vector3(0.15f, -0.05f, 0f), 0.5f, 2).OnComplete(() => pot.transform.localScale = Vector3.one);
        
        if (!GetComponent<Collider2D>().isActiveAndEnabled)
        {
            GetComponent<Collider2D>().enabled = true;
        }
    }
    public void SetCollider()
    {        
        if (!GetComponent<Collider2D>().isActiveAndEnabled)
        {
            GetComponent<Collider2D>().enabled = true;
        }
    }
    public void SetInitialColor()
    {
        GetComponent<Image>().color = initialColor;
    }
    public void ScalePot()
    {
        if (canScalePot)
        {
            canScalePot = false;
            //pot.transform.DOPunchScale(new Vector3(-0.05f, 0.15f, 0), 0.5f, 2);
            //transform.SetParent(tempPos);
        }
    }
    public void OnPointerDown()
    {
        if (!soupGame.cancelActions)
        {
            if (canMove)
            {
                transform.DOScale(scale, scaleSpeed);
            }
        }
    }
    public void OnPointerUp()
    {
        if (!soupGame.cancelActions)
        {
            if (canMove)
            {
                transform.DOScale(Vector3.one, scaleSpeed);
                canScalePot = true;
            }
        }
    }
}
