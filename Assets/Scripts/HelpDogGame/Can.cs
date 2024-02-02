using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Can : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform[] newParents;
    [SerializeField] private int actualParent = 0;
    [SerializeField] private GameObject tempElement;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Spike>().isSelected)
        {
            tempElement = collision.gameObject;
            tempElement.GetComponent<Collider2D>().enabled = false;
            tempElement.GetComponent<ObjectHandler>().resetPos = false;
            AudioManager.audioManager.PlayEffect(AudioEffectType.correct);
            EffectManager.effectManager.PlayEffect(Effects.stars, tempElement.transform);
            AudioManager.audioManager.PlayNumber(int.Parse(tempElement.GetComponent<Spike>().textM.text));
            MoveElement();
            
        }
        else
        {
            print("Not Selected");
        }
       
    }
    private void MoveElement()
    {
        tempElement.transform.DOMove(newParents[actualParent].position, 0.2f).OnComplete(() => ResetElement());
    }
    private void ResetElement()
    {
        tempElement.transform.SetParent(newParents[actualParent]);
        actualParent++;
        tempElement.transform.rotation = Quaternion.Euler(Vector3.zero);
        gameManager.NextElement();
    }
}
