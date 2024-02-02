using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VowelsController : MonoBehaviour
{
    [SerializeField] private SoupGame soupGame;
    [SerializeField] private Animator dishAnim;
    [SerializeField] private ParticleSystem dishVFX;
    [SerializeField] private AudioClip vowelStar;
    [SerializeField] private AudioClip swoosh1;
    [SerializeField] private AudioClip swoosh2;
    [SerializeField] private AudioClip swoosh3;
    private Animator vowelsAnim;
    [SerializeField] private VowelSoup[] vowels = new VowelSoup[3];    


    private void Start()
    {
        vowelsAnim = GetComponent<Animator>();
    }

    public void PlayIntroVowels()
    {
        vowelsAnim.Play("VowelsIntro");
    }
    public void SetVowelsTransform()
    {
        foreach (VowelSoup vowel in vowels)
        {
            vowel.SetInitialTransform();
        }
    }
    public void PlayVowelCorrect(string animName)
    {
        vowelsAnim.Play(animName);
    }

    public void VowelInDish()
    {
        dishAnim.Play("DishVowelIn");
        dishVFX.Play();
    }

    public void PlayVowelAudio()
    {
        soupGame.PlayLetter();
    }

    public void PlaySwooshOne()
    {
        AudioManager.audioManager.PlayOneShotAS(swoosh1);
    }
    public void PlaySwooshTwo()
    {
        AudioManager.audioManager.PlayOneShotAS(swoosh2);
    }
    public void PlaySwooshThree()
    {
        AudioManager.audioManager.PlayOneShotAS(swoosh3);
    }
    public void PlayVowelStarAudio()
    {
        AudioManager.audioManager.PlayOneShotAS(vowelStar);
    }

}
