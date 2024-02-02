using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CommonTweens : MonoBehaviour
{

    

    [Header("Move")]
    [SerializeField] private Transform finalPos;
    [SerializeField] private float transitionMoveSpeed;

    [Header("Scale")]
    [SerializeField] private Vector3 finalScale;
    [SerializeField] private float transitionScale;

    [Header("Fade")]
    [SerializeField] private float finalFade;
    [SerializeField] private float transitionFade;


    public void Move()
    {
        transform.DOMove(finalPos.position, transitionMoveSpeed);
    }

    public void ScalePunch()
    {
        transform.DOPunchScale(finalScale, transitionScale);
    }    

    public void Fade()
    {
        transform.GetComponent<SpriteRenderer>().DOFade(finalFade, transitionFade);
    }
}
