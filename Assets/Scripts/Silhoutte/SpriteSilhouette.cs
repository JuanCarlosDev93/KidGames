using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpriteSilhouette : MonoBehaviour
{
    public SpriteRenderer spriteRender;
    public Vector3 initialPos;
    public int selectedIndex;
    [SerializeField] private AudioClip clickAC;
    public bool isCompleted;

    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();        
    }

    public void MoveToNewPos(Vector3 newPos, float speed)
    {
        transform.DOMove(newPos,speed);
    }

    public void ActiveCollider()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
        Vector2 S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        gameObject.GetComponent<BoxCollider2D>().size = S;
        AudioManager.audioManager.PlayAudio(clickAC, AudioManager.audioManager.effectsAS);
        //spriteSilhouette.GetComponent<BoxCollider2D>().offset = new Vector2((S.x / 2), 0);

    }
}
