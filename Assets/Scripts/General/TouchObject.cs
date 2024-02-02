using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;


public class TouchObject : MonoBehaviour
{   


    void Start()
    {

    }   
    public void MoveObject(Transform newPos, float speed)
    {
        transform.DOMove(newPos.position, speed);
    }
    public void ScaleObject(Vector2 newScale, float speed)
    {
        transform.DOScale(newScale, speed);
    }
    public void ScalePunch(Vector2 newScale)
    {
        transform.DOPunchScale(newScale, 0.5f,1);
    }

}
