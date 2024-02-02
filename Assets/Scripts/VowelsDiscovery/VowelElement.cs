using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class VowelElement : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private VowelDiscovery vowelDiscovery;
    [SerializeField] private TouchObject touchObject;
    private Transform elementTransform;
    [SerializeField] private string vowel;
    [SerializeField] Transform centerPos;
    [SerializeField] Transform newPos;
    private int wordsQuantity;
    [SerializeField] GameObject bubble;
    [SerializeField] VowelElementData vowelData;
    [SerializeField] VowelWordData wordData;
    [SerializeField] Sprite initialSprite;
    [SerializeField] public SpriteRenderer elementSprite;
    public TextMeshPro elementName;
    [SerializeField] VowelElement[] otherVowels;
    [SerializeField] VowelElement[] vowelElements;

    [SerializeField] Vector2 scale;
    [SerializeField] Vector2 punchScale;
    [SerializeField] float actionSpeed;
    public bool isOther;
    public bool isCentered;
    public bool isCompleted;

    [SerializeField] private UnityEvent onTouchUp;


    private void Start()
    {
        wordsQuantity = vowelElements.Length;
        elementTransform = transform;
        initialSprite = GetComponent<SpriteRenderer>().sprite;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.audioManager.PlayEffect(AudioEffectType.pop);
        EffectManager.effectManager.PlayEffect(Effects.bubbleExplode, this.transform);

        if (isOther)
        {
            isOther = false;            
            isCompleted = true;
            vowelDiscovery.HideElements();
            MoveElementToCenter();
            bubble.SetActive(false);
        }
        else
        {
            AudioManager.audioManager.PlayVowel(vowel);
            transform.parent = null;
            if (!isCentered)
            {
                isCentered = true;
                vowelDiscovery.selectedVowel = this;              
                MoveToCenter();
                SetWords();
                HideOtherElements();
            }
            else
            {
                touchObject.ScalePunch(punchScale);
                vowelDiscovery.MoveElementToNewPos();
            }
            vowelDiscovery.octopus.PlayHideAnim();

        }

    }

    void SetWords()
    {
        for (int i = 0; i < wordsQuantity;i++)
        {
            vowelElements[i].wordData = vowelData.vowelWordDatas[i];
            vowelElements[i].elementSprite.sprite = vowelData.vowelWordDatas[i].elementImg;
            vowelElements[i].elementName.text = vowelData.vowelWordDatas[i].elementName;
            vowelElements[i].isOther = true;
        }
    }

    void PlaySyllable()
    {
        vowelDiscovery.syllablePlayer.gameObject.SetActive(true);
        vowelDiscovery.syllablePlayer.SetSyllableData(wordData);
        vowelDiscovery.syllablePlayer.PlaySyllable();
    }
    public void ResetElement()
    {
        if (!isCompleted)
        {            
            gameObject.SetActive(true);
        }
        
    }
    private void MoveToCenter()
    {
        elementTransform.DOMove(centerPos.position, 0.5f).OnComplete(()=> ShowVowelElements());
        elementTransform.DOScale(new Vector2(1.5f, 1.5f), 0.5f);      
    }
    private void MoveElementToCenter()
    {
        elementTransform.DOMove(centerPos.position, 0.5f).OnComplete(() => ActiveSyllablePLayer());
        elementTransform.DOScale(new Vector2(1.5f, 1.5f), 0.5f);
    }
    public void MoveToNewPos()
    {
        elementTransform.DOMove(newPos.position, 0.5f);
    }
    private void HideOtherElements()
    {
        foreach (VowelElement other in otherVowels)
        {
            other.gameObject.SetActive(false);
        }
    }
    private void ShowVowelElements()
    {
        foreach (VowelElement other in vowelElements)
        {
            other.gameObject.SetActive(true);
        }
    }
    private void ActiveSyllablePLayer()
    {
        gameObject.SetActive(false);
        PlaySyllable();
    }

    public void ShowVowel()
    {
        gameObject.SetActive(true);
        transform.DOScale(1,1.5f);
    }

}
