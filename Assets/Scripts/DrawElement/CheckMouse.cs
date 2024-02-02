using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMouse : MonoBehaviour
{
    [SerializeField] private ElementDraw elementDraw;

    private void OnMouseExit()
    {
        elementDraw.ResetElement();
    }

}
