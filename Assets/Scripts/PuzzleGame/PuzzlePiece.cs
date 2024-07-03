using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PuzzlePiece : MonoBehaviour
{
    public SpriteRenderer spriteRender;
    public SpriteRenderer spriteRenderLine;
    public Vector3 initialPos;
    public Vector2 initialScale;
    public int selectedIndex;
    [SerializeField] private AudioClip clickAC;
    public bool isCompleted;

    void Start()
    {
        initialScale = transform.localScale;
    }

    public void MoveToNewPos(Vector3 newPos, float speed)
    {
        transform.DOMove(newPos, speed);
    }
    public void NewScale(Vector2 newScale, float speed)
    {
        transform.DOScale(newScale, speed);
    }

    public void ActiveCollider()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
        AudioManager.audioManager.PlayAudio(clickAC, AudioManager.audioManager.effectsAS);
        //spriteSilhouette.GetComponent<BoxCollider2D>().offset = new Vector2((S.x / 2), 0);

    }
}
