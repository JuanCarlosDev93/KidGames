using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Cat : MonoBehaviour
{
    [SerializeField] private ClassRoomGame classRoom;
    Animator anim;

    [SerializeField] private GameObject catStand;
    [SerializeField] private GameObject catDraw;

    [SerializeField] private Image circle;
    [SerializeField] private Image square;
    [SerializeField] private Image triangle;

    [SerializeField] private GameObject circleActive;
    [SerializeField] private GameObject squareActive;
    [SerializeField] private GameObject triangleActive;  

    public GameObject selectedShape;

    [Header("Audio")]
    [SerializeField] private AudioSource puffEffect;    

    [Header("Effects")]
    [SerializeField] private ParticleSystem smoke;
    

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void ActiveSelectedShape()
    {
        selectedShape.SetActive(true);
        selectedShape.GetComponent<Image>().DOFade(1, 1.5f).OnComplete(() => WinShape());
    }
    void WinShape()
    {
        AudioManager.audioManager.PlayOneShotVoice(selectedShape.GetComponent<ClassRoomShape>().buttonShape.shapeName);        
        selectedShape.transform.DOPunchScale(new Vector2(0.01f, 0.01f), 1, 1).OnComplete(()=> selectedShape.GetComponent<ClassRoomShape>().CompleteShape());
    }
    public void HandleDraw()
    {
        catStand.SetActive(false);
        catDraw.SetActive(true);
        
    }
    public void HandleIdle()
    {
        classRoom.cancelActions = false;
        catStand.SetActive(true);
        catDraw.SetActive(false);
        anim.Play("IdleCat");
    }
    public void ActiveCircle()
    {
        circleActive.SetActive(true);
        circleActive.GetComponent<Image>().DOFade(1,1.5f).OnComplete(()=> WinShape());
    }
    
    public void ActiveSquare()
    {
        squareActive.SetActive(true);
    }
    public void ActiveTriangle()
    {
        triangleActive.SetActive(true);
    }
    public void HideAllShapes()
    {
        if (selectedShape.gameObject.activeInHierarchy)
        {
            selectedShape.SetActive(false);
            smoke.Play();
            puffEffect.Play();
        }

        /*if (triangleActive.gameObject.activeInHierarchy || squareActive.gameObject.activeInHierarchy || circleActive.gameObject.activeInHierarchy)
        {
            triangleActive.SetActive(false);
            squareActive.SetActive(false);
            circleActive.SetActive(false);
            smoke.Play();
            puffEffect.Play();
        }*/        
    }

    public IEnumerator ActivateShape()
    {
        yield return new WaitForSeconds(3);
        HideAllShapes();
        classRoom.ElementsMoveOut();
    }    

}
