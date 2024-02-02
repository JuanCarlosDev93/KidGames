using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButton : MonoBehaviour
{
    [SerializeField] private ColorMachine colorMachine = default;
    [SerializeField] private Faucet faucet;
    [SerializeField] private ParticleSystem liquidVFX = default;
    [SerializeField] private Colors colorType = Colors.Yellow;
    Color tempColor = Color.white;    
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip colorName;
    [SerializeField] private bool buttonSelected;


    public void SetLiquidEffect()
    {
        if (!colorMachine.cancelActions && !buttonSelected)
        {
            buttonSelected = true;
            StartCoroutine(LiquidEffectColor(1.35f));
        }
    }
    IEnumerator LiquidEffectColor(float delay)
    {
        colorMachine.cancelActions = true;
        switch (colorType)
        {
            case Colors.Yellow:
                tempColor = new Color32(255,209,0,255);
                break;
            case Colors.Blue:
                tempColor = new Color32(0, 99, 255, 255);
                break;
            case Colors.Red:
                tempColor = new Color32(255, 17, 0, 255);
                break;

        }
        var main = liquidVFX.main;
        main.startColor = tempColor;
        faucet.PlayFaucetAnim();
        yield return new WaitForSeconds(faucet.faucetSpray.length+0.02f);
        colorMachine.cancelActions = false;
        colorMachine.CombineColors(colorType, colorName, tempColor);
    }
    public void PlayButtonClick()
    {
        AudioManager.audioManager.PlayOneShotAS(buttonClick);
    }
    public void PlayColorName()
    {
        AudioManager.audioManager.PlayOneShotVoice(colorName);
    }
    public void ResetButton()
    {
        buttonSelected = false;
    }
}
