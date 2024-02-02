using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class DrawShape : MonoBehaviour
{
    public DrawLineShape drawLine;
    public SplineWalker hand;
    [SerializeField] private Transform leftPos;
    [SerializeField] private Transform centerPos;
    [SerializeField] private Transform rightPos;
    [SerializeField] private GameObject[] itemsToHide;

    public int elementsQuantity;
    public int elementsCounter;
    public bool activeHand;

    [Header("Audio")]
    public AudioSource correct;
    public AudioSource win;
    [SerializeField] private AudioSource voice;
    [SerializeField] private AudioClip[] voices;
    [SerializeField] private AudioClip[] elementNames;
    [SerializeField] private AudioClip[] nextElements;
    [SerializeField] private AudioClip elementToDraw;

    private void Start()
    {  
        //StartCoroutine(PlayTutorial(1));
    }
    public void NextElement()
    {
        StartCoroutine(PlayElementName(0));
        //Invoke(nameof(IntroAnim), 1f);
    }   
    
    
    public void ResetGame()
    {       
        elementsCounter = 0;
        activeHand = true;
        drawLine.canDraw = true;
        drawLine.lastPointReached = false;
        HideItems();
        StartCoroutine(PlayElementName(1f));
    }    

    IEnumerator PlayTutorial(float delay)
    {
        yield return new WaitForSeconds(delay);
        voice.clip = voices[0];
        voice.Play();
        yield return new WaitForSeconds(voices[0].length + 0.01f);
        voice.clip = voices[1];
        voice.Play();
        yield return new WaitForSeconds(voices[1].length + 0.01f);
        StartCoroutine(PlayElementName(0));
    }
    IEnumerator PlayElementName(float delay)
    {
        int randomNum = 0;
        randomNum = Random.Range(0, nextElements.Length);
        yield return new WaitForSeconds(delay);
        voice.clip = nextElements[randomNum];
        voice.Play();
        yield return new WaitForSeconds(nextElements[randomNum].length);
        voice.clip = elementToDraw;
        voice.Play();
        yield return new WaitForSeconds(elementToDraw.length);
        voice.clip = elementNames[elementsCounter];
        voice.Play();
    }
    void HideItems()
    {
        foreach (GameObject item in itemsToHide)
        {
            item.SetActive(false);
        }
    }
}
