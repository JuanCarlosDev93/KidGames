using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerConfig : MonoBehaviour
{

    [SerializeField] private AudioClip bgMusic;


    void Start()
    {
        AudioManager.audioManager.SetAudioManagerConfig(bgMusic);
    }
}
