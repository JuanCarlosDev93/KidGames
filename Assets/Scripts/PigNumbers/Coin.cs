using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    [SerializeField] private Pig pig;
    public TextMeshPro textM;
    [SerializeField] private AudioClip coinIn;
    [SerializeField] private float introSpeedMin;
    [SerializeField] private float introSpeedMax;
    public bool isSelected = false;

    private void Start()
    {
        transform.position = new Vector2(transform.position.x, 8f);
    }
    public void Intro()
    {
        AudioManager.audioManager.PlayEffect(AudioEffectType.swoosh);
        transform.DOMoveY(GetComponent<DragObject>().initialPos.y, RandomSpeed(introSpeedMin, introSpeedMax)).OnComplete(()=> CoinPunchPosition()).SetEase(Ease.Linear);
    }
    private void CoinPunchPosition()
    {
        AudioManager.audioManager.PlayAudio(coinIn,AudioManager.audioManager.effectsAS);
        transform.DOPunchPosition(new Vector2(0, 1.5f), 0.3f, 1).SetEase(Ease.Linear);
    }
    public void Outro()
    {
        AudioManager.audioManager.PlayEffect(AudioEffectType.swoosh);
        transform.DOMoveY(8f, 0.5f).OnComplete(() => pig.MovePersCoin());
    }
    private float RandomSpeed(float min, float max)
    {
        return Random.Range(min,max);
    }
}
