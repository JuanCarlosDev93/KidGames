using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PaletteColor : MonoBehaviour
{
    PaintingGame paintingGame;

    public ColorType colorType = ColorType.red;
    public Color32 color;
    public Collider2D paletteCollider;

    private void Start()
    {
        color = GetComponent<Image>().color;
        paletteCollider = GetComponent<Collider2D>();
        paintingGame = FindObjectOfType<PaintingGame>();
    }
    public void OnClickColorPalette()
    {
        paintingGame.selectedColor = color;
        paintingGame.selectedColorType = colorType;
        transform.DOPunchScale(new Vector2(0.5f,0.5f),0.3f,3);
        paintingGame.PlaySplashAudio();
        //paintingGame.PlaySplashEffect();
        //AudioManager.audioManager.PlayEffect(AudioEffectType.pop);
        paintingGame.PaletteTouchAnim();
    }
}
