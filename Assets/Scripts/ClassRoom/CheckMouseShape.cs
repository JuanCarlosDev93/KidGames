using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMouseShape : MonoBehaviour
{
    [SerializeField] private ElementDrawShape elementDraw;

    private void OnMouseExit()
    {
        elementDraw.ResetElement();
        elementDraw.ResetPoints();
    }
}
