using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AdTimer : MonoBehaviour
{

    public bool enableTimer;
    [SerializeField] private int timeToBackAds;
    [SerializeField] private float timer;
    public UnityEvent onTimerEnd;


    // Start is called before the first frame update
    void Start()
    {
        timer = timeToBackAds;
    }

    // Update is called once per frame
    void Update()
    {
        if (enableTimer)
        {            
            if (timer > 0)
            {
                float minutes = Mathf.Floor(timer / 60);
                timer -= Time.deltaTime;
            }
            else
            {                
                enableTimer = false;             
                timer = timeToBackAds;
                AdmobAds.admobAds.noAds = false;
                onTimerEnd?.Invoke();
            }
        }
    }

    public int RemainingTime()
    {
        return (int) timer;
    } 
}
