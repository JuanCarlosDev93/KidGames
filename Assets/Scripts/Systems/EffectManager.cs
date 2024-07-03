using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Effects
{
    stars,
    hearts,
    bubbleExplode
}

public class EffectManager : MonoBehaviour
{
    public static EffectManager effectManager;

    [SerializeField] private ParticleSystem stars;
    [SerializeField] private ParticleSystem hearts;
    [SerializeField] private ParticleSystem bubbleExplode;
    [SerializeField] private ParticleSystem custom;


    [HideInInspector]
    public Effects effects = Effects.stars;


    private void Awake()
    {
        effectManager = this;

    }

    public void PlayEffect(Effects customEffect, Transform vfxPos)
    {
        switch (customEffect)
        {
            case Effects.stars:
                stars.transform.position = vfxPos.position;
                stars.Play();
                break;
            case Effects.hearts:
                hearts.transform.position = vfxPos.position;
                hearts.Play();
                break;
            case Effects.bubbleExplode:
                bubbleExplode.transform.position = vfxPos.position;
                bubbleExplode.gameObject.SetActive(true);
                bubbleExplode.Play();
                break;
        }
    }
    public void PlayCustomEffect(ParticleSystem customEffect, Transform vfxPos)
    {
        customEffect.transform.position = vfxPos.position;
        customEffect.Play();        
    }
}
