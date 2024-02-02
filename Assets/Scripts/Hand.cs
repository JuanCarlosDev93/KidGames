using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hand : MonoBehaviour
{
    [SerializeField] private Transform initPos;
    [SerializeField] private Transform finalPos;
    [SerializeField] private float speed;


    private void OnEnable()
    {
        MoveHandLoop();
    }
    public void MoveHandLoop()
    {
        transform.DOMove(finalPos.position, speed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InCirc);
    }
    private void OnDisable()
    {
        transform.DOKill();
        transform.position = initPos.position;
    }

}
