using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassRoomShape : MonoBehaviour
{
    [SerializeField] private ClassRoomGame classRoom;
    public ButtonShape buttonShape;
    [SerializeField] private Cat cat;
    [SerializeField] private GameObject star;
    [SerializeField] private GameObject starWin;
    [SerializeField] private Vector2 starWinInitPos;

    [Header("Audio")]
    [SerializeField] private AudioClip starAC;
    [SerializeField] private AudioClip swooshAC;
    [SerializeField] private AudioClip popAC;
    [Header("Effects")]
    [SerializeField] private ParticleSystem starPS;
    [Header("State")]
    public bool shapeCompleted;


    void Start()
    {
        starWinInitPos = starWin.transform.position;
    }

    public void CompleteShape()
    {
        //ResetPoints();
        //ResetTotalElement();
        shapeCompleted = true;
        buttonShape.shapeCompleted = true;
        StarWinAnim();
    }
    public void StarWinAnim()
    {
        AudioManager.audioManager.PlayAudio(starAC, AudioManager.audioManager.effectsAS);
        starWin.SetActive(true);
        starPS.Play();
        starWin.transform.DOScale(new Vector2(1, 1), 1f).OnComplete(() => MoveStarWin());
    }
    void MoveStarWin()
    {
        AudioManager.audioManager.PlayAudio(swooshAC, AudioManager.audioManager.effectsAS);
        starWin.transform.DOScale(new Vector2(0.5f, 0.5f), 1f);
        starWin.transform.DOMove(star.transform.position, 0.5f).OnComplete(() => ActiveStar());
    }
    void ActiveStar()
    {
        AudioManager.audioManager.PlayAudio(popAC, AudioManager.audioManager.effectsAS);
        starWin.SetActive(false);
        starWin.transform.position = starWinInitPos;
        starWin.transform.localScale = Vector2.zero;
        star.SetActive(true);
        star.transform.DOPunchScale(new Vector2(1f, 1f), 0.8f, 1);
        cat.HideAllShapes();
        classRoom.EnablePanelButtons(true);
        buttonShape.GetComponent<Button>().interactable = false;
        classRoom.CheckAllShapes();
    }
}
