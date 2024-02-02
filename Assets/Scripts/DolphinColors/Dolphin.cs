using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dolphin : MonoBehaviour
{


    public void PlayHide()
    {
        GetComponent<Animator>().Play("DolphinHided");
    }

    
}
