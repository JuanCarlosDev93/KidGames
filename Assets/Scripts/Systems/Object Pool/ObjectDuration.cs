using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDuration : MonoBehaviour
{
    private bool enableTimer;
    private float timer;
    public float objectDuration;

    private void OnEnable()
    {
        enableTimer = true;
    }

    private void Update()
    {
        if (enableTimer)
        {
            if (timer < objectDuration)
            {
                timer += Time.deltaTime;
            }
            else
            {
                enableTimer = false;
                gameObject.SetActive(false);
                timer = 0;
            }
        }
        
    }


}
