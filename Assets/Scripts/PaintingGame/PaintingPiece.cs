using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PaintingPiece : MonoBehaviour
{
    PaintingGame paintingGame;

    [SerializeField] private ColorType colorType = ColorType.red;
    [SerializeField] private SpriteShapeRenderer spriteRenderer;
    public bool isCompleted = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteShapeRenderer>();
        paintingGame = FindObjectOfType<PaintingGame>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PaletteColor tempPaletteColor;
        tempPaletteColor = collision.GetComponent<PaletteColor>();

        if (paintingGame.useFreePainting)
        {
            isCompleted = true;
            spriteRenderer.color = tempPaletteColor.color;
            paintingGame.CheckAllPaintingPieces();
        }
        else
        {
            if (tempPaletteColor.colorType == colorType)
            {
                //SetColor();
                isCompleted = true;
                spriteRenderer.color = tempPaletteColor.color;
                //tempPaletteColor.paletteCollider.enabled = false;
                paintingGame.CheckAllPaintingPieces();
                Debug.Log("Correct: " + colorType.ToString());
            }
            else
            {
                Debug.Log("Incorrect: " + colorType.ToString());
            }
        }
        
    }

    public void OnClickPaintingPiece()
    {
        if (paintingGame.useFreePainting)
        {
            isCompleted = true;
            spriteRenderer.color = paintingGame.selectedColor;
            paintingGame.CheckAllPaintingPieces();
        }
        else
        {
            if (paintingGame.selectedColorType == colorType)
            {
                isCompleted = true;
                spriteRenderer.color = paintingGame.selectedColor;
                paintingGame.CheckAllPaintingPieces();
            }
        }
        paintingGame.PlaySplashAudio();
        paintingGame.PlaySplashEffect();

    }
    private void SetColor()
    {
        switch (colorType)
        {
            case ColorType.yellow:
                spriteRenderer.color = Color.yellow;
                break;
            case ColorType.blue:
                spriteRenderer.color = Color.blue;
                break;
            case ColorType.red:
                spriteRenderer.color = Color.red;
                break;
            default:
                break;
        }
    }
}
