using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform persCoin;
    [SerializeField] private Transform[] newParents;
    [SerializeField] private GameObject tempElement;
    //[SerializeField] private int actualParent = 0;
    [SerializeField] private AudioClip coinInPig;
    [SerializeField] private AudioClip correct;


    public void Intro()
    {
        AudioManager.audioManager.PlayEffect(AudioEffectType.swoosh);
        transform.DOMoveX(4.2f, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Coin>().isSelected)
        {
            tempElement = collision.gameObject;
            tempElement.GetComponent<Collider2D>().enabled = false;
            tempElement.GetComponent<DragObject>().resetPos = false;
            AudioManager.audioManager.PlayEffect(AudioEffectType.correct);
            EffectManager.effectManager.PlayEffect(Effects.stars, tempElement.transform);
            AudioManager.audioManager.PlayNumber(int.Parse(tempElement.GetComponent<Coin>().textM.text));
            tempElement.GetComponent<Coin>().Outro();
        }
        else
        {
            print("Not Selected");
        }

    }
    public void MovePersCoin()
    {
        AudioManager.audioManager.PlayEffect(AudioEffectType.swoosh);
        persCoin.DOMoveY(-1.0f, 0.5f).OnComplete(() => CoinInPig());
    }
    private void CoinInPig()
    {
        AudioManager.audioManager.PlayAudio(coinInPig, AudioManager.audioManager.effectsAS);
        //AudioManager.audioManager.PlayAudio(correct,AudioManager.audioManager.effectsAS);
        transform.DOPunchScale(new Vector2(0.2f, -0.2f), 0.3f, 1).OnComplete(() => ResetElement());
    }
    private void ResetElement()
    {      
        
        gameManager.NextElement();
        persCoin.transform.position = new Vector3(persCoin.transform.position.x, 8f);
    }
   
}
