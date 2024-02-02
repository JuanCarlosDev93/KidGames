using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerealBox : MonoBehaviour
{
    [SerializeField] private VowelsController vowelsController;
    [SerializeField] private ParticleSystem cerealVFX;
    [SerializeField] private AudioClip openBox;
    [SerializeField] private AudioClip openBox2;
    [SerializeField] private AudioClip shakeSides;
    [SerializeField] private AudioClip shakeUD;

    public void PlayIntroVowels()
    {
        vowelsController.PlayIntroVowels();
        cerealVFX.Play();
    }
    public void PlaySwoosh()
    {
        AudioManager.audioManager.PlayEffect(AudioEffectType.swoosh);
    }
    public void PlayOpenBoxAudio()
    {
        AudioManager.audioManager.PlayOneShotAS(openBox);
        AudioManager.audioManager.PlayOneShotAS(openBox2);
    }
    public void PlayShakeSides()
    {
        AudioManager.audioManager.PlayOneShotAS(shakeSides);
    }
    public void PlayShakeUD()
    {
        AudioManager.audioManager.PlayOneShotAS(shakeUD);
    }
}
