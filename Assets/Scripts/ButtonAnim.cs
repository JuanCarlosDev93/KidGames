using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonAnim : MonoBehaviour
{
    [SerializeField] private Vector3 newSize;

    public void PunchScale()
    {
        transform.DOPunchScale(newSize,0.2f,1);
    }
    public void Pressed()
    {
        transform.DOLocalMoveY(-0.17f, 0.2f);
    }

}
