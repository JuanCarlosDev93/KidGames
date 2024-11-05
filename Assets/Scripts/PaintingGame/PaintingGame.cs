using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PaintingGame : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator easelAnim;

    [SerializeField] private Transform easelCanvas;
    [SerializeField] private Transform colorPalette;

    [SerializeField] private AudioClip splashSFX;
    [SerializeField] private AudioClip starsSFX;

    [SerializeField] private ParticleSystem splashVFX;
    [SerializeField] private ParticleSystem winVFX;

    private PaintingObject selectedPaintingObject;
    [SerializeField] private PaintingObject[] paintingObjects;

    [SerializeField] private List<PaintingPiece> paintingPieces = new List<PaintingPiece>();

    [SerializeField] private int paintingSelected;

    [SerializeField] private Vector2 paletteInitialPos;


    public Color32 selectedColor;
    public ColorType selectedColorType;

    public bool useFreePainting;


    private void Start()
    {
        paintingSelected = Random.Range(0, paintingObjects.Length);
        AddPaintingPieces();
        paletteInitialPos = colorPalette.position;
        colorPalette.position = new Vector2(-20,colorPalette.position.y);
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f);
        easelAnim.SetTrigger("showEasel");
        yield return new WaitForSeconds(1f);
        colorPalette.DOMoveX(paletteInitialPos.x, 0.5f);
        AudioManager.audioManager.PlayEffect(AudioEffectType.swoosh);
    }

    public void CheckAllPaintingPieces()
    {
        if (PaintingCompleted())
        {            
            StartCoroutine(ShowWinScreen());
        }
    }
    private bool PaintingCompleted()
    {
        for (int i = 0; i < paintingPieces.Count; i++)
        {
            if (!paintingPieces[i].isCompleted)
            {
                return false;
            }
        }
        return true;
    }

    private void AddPaintingPieces()
    {
        selectedPaintingObject = Instantiate(paintingObjects[paintingSelected]);
        selectedPaintingObject.transform.SetParent(easelCanvas);
        selectedPaintingObject.transform.position = Vector3.zero;
        for (int i = 0; i < selectedPaintingObject.paintingPieces.Length; i++)
        {
            paintingPieces.Add(selectedPaintingObject.paintingPieces[i]);
        }
    }
    public void ShowPainting()
    {
        
        selectedPaintingObject.gameObject.SetActive(true);
    }
    private IEnumerator ShowWinScreen()
    {        
        yield return new WaitForSeconds(0.5f);
        EffectManager.effectManager.PlayCustomEffect(winVFX,winVFX.transform);
        AudioManager.audioManager.effectsAS.pitch = 1;
        AudioManager.audioManager.PlayAudio(starsSFX,AudioManager.audioManager.effectsAS);
        yield return new WaitForSeconds(1f);      
        gameManager.WinScreen();
    }
    public void PlaySplashAudio()
    {
        AudioManager.audioManager.PlayAudioPitch(splashSFX, AudioManager.audioManager.effectsAS, Random.Range(0.7f,1.3f));
    }
    public void PlaySplashEffect()
    {
        ParticleSystem.MainModule splashMain = splashVFX.main;
        Color color = selectedColor;
        splashMain.startColor = color;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        EffectManager.effectManager.PlayCustomEffectPos(splashVFX, mousePos);
    }
    public void PaletteTouchAnim()
    {
        colorPalette.DOPunchPosition(new Vector3(0, -10f, 0), 0.5f,3);
    }
}
