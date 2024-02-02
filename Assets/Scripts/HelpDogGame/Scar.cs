using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scar : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private SpriteRenderer scarSpriteRend;
    public Sprite bandageSprt;
    public TextMesh textM;
    public bool isSelected = false;
    [SerializeField] private GameObject tempElement;


    private void Start()
    {
        scarSpriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isSelected && collision.GetComponent<Spike>().isSelected)
        {
            textM.gameObject.SetActive(false);

            GetComponent<Collider2D>().enabled = false;
            tempElement = collision.gameObject;
            tempElement.GetComponent<Collider2D>().enabled = false;
            tempElement.GetComponent<ObjectHandler>().resetPos = false;
            tempElement.gameObject.SetActive(false);
            AudioManager.audioManager.PlayEffect(AudioEffectType.correct);
            EffectManager.effectManager.PlayEffect(Effects.stars, tempElement.transform);
            AudioManager.audioManager.PlayNumber(int.Parse(tempElement.GetComponent<Spike>().textM.text));
            ChangeSprite();
            gameManager.NextElement();
        }
        else
        {
            print("Not Selected");
        }
    }

    void ChangeSprite()
    {
        scarSpriteRend.sprite = bandageSprt;
        scarSpriteRend.color = Color.white;
    }
}
