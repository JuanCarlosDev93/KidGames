using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTouch : MonoBehaviour
{
    public void TouchButton()
    {
        AudioManager.audioManager.PlayOneShotAS(AudioManager.audioManager.buttonAC);
    }
    public void TouchButtonStars()
    {
        AudioManager.audioManager.PlayOneShotAS(AudioManager.audioManager.starSFX);
    }
    public void PlayTouchVFX()
    {
        EffectManager.effectManager.PlayEffect(Effects.stars, transform);
    }
}
