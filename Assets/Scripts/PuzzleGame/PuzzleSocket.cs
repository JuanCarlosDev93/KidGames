using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSocket : MonoBehaviour
{
    [SerializeField] private PuzzleGame puzzleGame;
    public SpriteRenderer spriteRender;
    public int selectedIndex;


    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PuzzlePiece>().selectedIndex == selectedIndex)
        {
            collision.GetComponent<PuzzlePiece>().isCompleted = true;
            collision.GetComponent<DragObject>().resetPos = false;
            collision.GetComponent<PuzzlePiece>().MoveToNewPos(transform.position, 0.5f);
            collision.GetComponent<PuzzlePiece>().NewScale(collision.GetComponent<PuzzlePiece>().initialScale, 0.5f);
            EffectManager.effectManager.PlayEffect(Effects.stars, transform);
            AudioManager.audioManager.PlayEffect(AudioEffectType.correct);
            puzzleGame.CheckAllPieces();

        }
    }
}
