using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{

    [SerializeField] private AudioClip yay;
    [SerializeField] private AudioClip claps;


    void Start()
    {
        AudioManager.audioManager.PlayOneShotAS(yay);
        AudioManager.audioManager.PlayOneShotAS(claps);
    }

}
