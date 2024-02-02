using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMouseNumber : MonoBehaviour
{
    [SerializeField] private ElementDrawNumber elementDraw;

    private void OnMouseExit()
    {
        elementDraw.ResetElement();
    }
}
