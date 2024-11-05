using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Easel : MonoBehaviour
{
    [SerializeField] private PaintingGame paintingGame;
    [SerializeField] private AudioClip canvasInEaselSFX;

    public void SwooshEffect()
    {
        AudioManager.audioManager.PlayEffect(AudioEffectType.swoosh);
    }
    public void CanvasInEaselEffect()
    {
        AudioManager.audioManager.PlayAudio(canvasInEaselSFX, AudioManager.audioManager.effectsAS);
    }
    public void ShowPainting()
    {
        paintingGame.ShowPainting();
    }

}
