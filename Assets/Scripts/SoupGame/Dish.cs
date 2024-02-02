using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dish : MonoBehaviour
{
    [Header("Main Objects")]
    [SerializeField] private SoupGame soupGame = default;
    [SerializeField] private Animator cerealBoxAnim;
    [SerializeField] private Transform[] vowelPositions;
    [HideInInspector] public int posIndex = 0;

    [Header("Audio")]
    [SerializeField] private AudioSource effect;
    [SerializeField] private AudioSource voice;
    [SerializeField] private AudioClip vowelInEffect;
    [SerializeField] private AudioClip vowelInEffect2;
    [SerializeField] private AudioClip dishInSFX;
    [SerializeField] private AudioClip dishVowelIn;

    [Header("Effects")]
    [SerializeField] private ParticleSystem stars;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<VowelSoup>().selected)
        {
            other.enabled = false;
            other.GetComponent<VowelSoup>().selected = false;
            other.GetComponent<VowelSoup>().canMove = false;
            soupGame.cancelActions = true;
            ScaleUpDish();
            AudioManager.audioManager.PlayEffect(AudioEffectType.correct);            
            stars.Play();
            other.GetComponent<VowelSoup>().PlayVoice();
            VowelNewScale(other.transform);
            VowelToOtherPos(other.transform, vowelPositions[posIndex]);
            if (posIndex < vowelPositions.Length-1)
            {
                posIndex++;
            }
            Debug.Log("Correct: " + other.gameObject.name);
        }
        else
        {
            other.GetComponent<Collider2D>().enabled = false;
            AudioManager.audioManager.PlayEffect(AudioEffectType.incorrect);
            Debug.Log("Incorrect: " + other.gameObject.name);
        }
    }

    void ScaleUpDish()
    {
        transform.DOPunchScale(new Vector2(0.2f,0.2f),0.3f, 1);
    }
    void VowelToOtherPos(Transform vowel, Transform newPos)
    {
        vowel.transform.DOMove(newPos.position, 0.5f).OnComplete(()=> SetVowelNewPos(vowel));
    }
    void VowelNewScale(Transform vowel)
    {
        vowel.transform.DOScale(new Vector3(0.7f,0.7f,1), 0.5f);
    }
    void SetVowelNewPos(Transform vowel)
    {
        soupGame.cancelActions = false;
        vowel.SetParent(vowelPositions[posIndex]);
        soupGame.CheckAllVowels();
    }
    public void NextElement()
    {
        if (!soupGame.playingIntro)
        {
           cerealBoxAnim.Play("MoveInBox");
           soupGame.CheckAllVowels();
        }
    }
    public void PlaySwoosh()
    {
        AudioManager.audioManager.PlayEffect(AudioEffectType.swoosh);
        PlayDishIn();

    }
    public void PlayDishIn()
    {
        AudioManager.audioManager.PlayOneShotAS(dishInSFX);
    }

    public void PlayVowelInAudio()
    {
        AudioManager.audioManager.PlayOneShotAS(vowelInEffect);
        AudioManager.audioManager.PlayOneShotAS(vowelInEffect2);
        AudioManager.audioManager.PlayOneShotAS(dishVowelIn);

    }
}
