using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silhouette : MonoBehaviour
{
    [SerializeField] private SilhouetteGame silhouetteGame;
    public SpriteRenderer spriteRender;
    public SpriteMask spriteMask;
    public int selectedIndex;


    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SpriteSilhouette>().selectedIndex == selectedIndex)
        {
            collision.GetComponent<SpriteSilhouette>().isCompleted = true;
            collision.GetComponent<DragObject>().resetPos = false;
            collision.GetComponent<SpriteSilhouette>().MoveToNewPos(transform.position, 0.5f);
            EffectManager.effectManager.PlayEffect(Effects.stars, transform);
            AudioManager.audioManager.PlayEffect(AudioEffectType.correct);
            silhouetteGame.CheckAllSilhouettes();

        }
    }
}
