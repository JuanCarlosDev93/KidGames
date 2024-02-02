using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepDraw : MonoBehaviour
{
    public GameObject firstPoint;
    public GameObject middlePoint;
    public GameObject lastPoint;


    public void ResetPoints()
    {
        firstPoint.SetActive(true);
        middlePoint.SetActive(false);
        lastPoint.SetActive(false);
    }


}
