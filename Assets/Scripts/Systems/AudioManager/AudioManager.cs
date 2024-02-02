using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AudioEffectType
{   
    win,
    correct,
    incorrect,
    swoosh,
    pop,
    star
}

public class AudioManager : MonoBehaviour
{

    public static AudioManager audioManager;
    public delegate void FinalEvent();
    public FinalEvent FinalAudioEvent;

    [Header("General AudioSources")]
    [SerializeField] private AudioSource bgMusicAS;
    public AudioSource voiceAS;
    public AudioSource winAS;
    public AudioSource effectsAS;

    [Header("General AudioClips")]
    public AudioClip winSFX;
    public AudioClip correctSFX;
    public AudioClip incorrectSFX;
    public AudioClip popSFX;
    public AudioClip swooshSFX;
    public AudioClip starSFX;
    public AudioClip buttonAC;


    [Header("Vowel AudioClips")]
    [SerializeField] private AudioClip[] vowels;

    [Header("Number AudioClips")]
    [SerializeField] private AudioClip[] numbers;

    [HideInInspector] public AudioEffectType audioEffectType;

    [Header("Volume Controls")]
    [Range(0f, 1f)]
    [SerializeField] private float musicVolume = 0.1f;
    [Range(0.5f, 1f)]
    [SerializeField] private float voiceVolume = 1f;
    [Range(0.5f, 1f)]
    [SerializeField] private float sfxVolume = 0.5f;



    void Awake()
    {
        if(audioManager != null)
        {
            Destroy(this.gameObject);
        }
        audioManager = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        bgMusicAS.volume = musicVolume;
        voiceAS.volume = voiceVolume;
        winAS.volume = musicVolume;
        effectsAS.volume = sfxVolume;
    }
    //Vocales
    public void StopAudioVoice()
    {
        bgMusicAS.Stop();
        voiceAS.Stop();
        winAS.Stop();
        effectsAS.Stop();
    }
    public void PlayVowel(string vowel)
    {
        switch (vowel)
        {
            case "a":
                PlayOneShotVoice(vowels[0]);
                break;
            case "e":
                PlayOneShotVoice(vowels[1]);
                break;
            case "i":
                PlayOneShotVoice(vowels[2]);
                break;
            case "o":
                PlayOneShotVoice(vowels[3]);
                break;
            case "u":
                PlayOneShotVoice(vowels[4]);
                break;
        }
    }
    //Numeros
    public void PlayNumber(int number)
    {
        switch (number)
        {
            case 0:
                PlayOneShotVoice(numbers[0]);

                break;
            case 1:
                PlayOneShotVoice(numbers[1]);
                break;
            case 2:
                PlayOneShotVoice(numbers[2]);
                break;
            case 3:
                PlayOneShotVoice(numbers[3]);
                break;
            case 4:
                PlayOneShotVoice(numbers[4]);
                break;
            case 5:
                PlayOneShotVoice(numbers[5]);
                break;
            case 6:
                PlayOneShotVoice(numbers[6]);
                break;
            case 7:
                PlayOneShotVoice(numbers[7]);
                break;
            case 8:
                PlayOneShotVoice(numbers[8]);
                break;
            case 9:
                PlayOneShotVoice(numbers[9]);
                break;
            case 10:
                PlayOneShotVoice(numbers[10]);
                break;
        }
    }
    public void PlayEffect(AudioEffectType audioType)
    {
        switch (audioType)
        {
            case AudioEffectType.correct :
                PlayOneShotAS(correctSFX);
                break;
            case AudioEffectType.incorrect:
                PlayOneShotAS(incorrectSFX);
                break;
            case AudioEffectType.swoosh:
                PlayOneShotAS(swooshSFX);
                break;
            case AudioEffectType.pop:
                PlayOneShotAS(popSFX);
                break;
            case AudioEffectType.star:
                PlayOneShotAS(starSFX);
                break;
        }
    }
    public void PlayOneShotAS(AudioClip aClip)
    {        
        effectsAS.PlayOneShot(aClip);
    }
    public void PlayOneShotVoice(AudioClip voiceClip)
    {
        //voiceAS.PlayOneShot(voiceClip);
        voiceAS.clip = voiceClip;
        voiceAS.Play();
    }
    public void PlayOneShotVoiceNew(AudioClip voiceClip)
    {
        voiceAS.PlayOneShot(voiceClip);        
    }
    public void PlayAudio(AudioClip audioClip, AudioSource audioSource)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    public IEnumerator PlayAudioWithEvent(AudioClip audioClip, bool useVoiceAS,float initialDelay, FinalEvent FinalAudioEvnt)
    {
        yield return new WaitForSeconds(initialDelay);
        if (useVoiceAS)
        {
            PlayOneShotVoice(audioClip);
        }
        else
        {
            PlayOneShotAS(audioClip);
        }

        yield return new WaitForSeconds(audioClip.length);
        FinalAudioEvent = FinalAudioEvnt;
        FinalAudioEvent();
    }
    public void StopAudio(AudioSource audioSource)
    {        
        audioSource.Stop();
    }
    public void PauseAudio(AudioSource audioSource)
    {
        audioSource.Pause();
    }

    public void SetAudioManagerConfig(AudioClip bgMusic)
    {
        bgMusicAS.clip = bgMusic;
        bgMusicAS.Play();
    }
}
