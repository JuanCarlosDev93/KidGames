using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faucet : MonoBehaviour
{
    Animator anim;
    [SerializeField] private ParticleSystem liquidVFX = default;
    public AnimationClip faucetSpray;
    [SerializeField] private AudioClip bubbles;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayFaucetAnim()
    {
        anim.Play("FaucetSpray");
    }
    public void PlayLiquid()
    {
        liquidVFX.Play();
    }
    public void StopLiquid()
    {
        liquidVFX.Stop();
    }
    public void PlayFaucetSound()
    {
        AudioManager.audioManager.effectsAS.loop = true;
        AudioManager.audioManager.PlayAudio(bubbles, AudioManager.audioManager.effectsAS);
    }
    public void StopFaucetSound()
    {
        AudioManager.audioManager.effectsAS.loop = false;
        AudioManager.audioManager.StopAudio(AudioManager.audioManager.effectsAS);
    }
}
