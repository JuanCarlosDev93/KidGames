using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonShape : MonoBehaviour
{
    [SerializeField] private ClassRoomGame classRoom;
    [SerializeField] private GameObject shape;
    [SerializeField] private Cat cat;
    [SerializeField] private GameObject hand;
    
    [SerializeField] private Animator charAnim;
    [SerializeField] private string paramName;
    [SerializeField] private AudioClip correct;
    [Header("Audio")]
    public AudioClip shapeName;
    [Header("State")]
    public bool shapeCompleted;


   
    public void OnTouchShape()
    {
        if (shapeCompleted)
        {
            //Activar voz indicando que se completo la forma.
            return;
        }
        if (!classRoom.cancelActions)
        {
            AudioManager.audioManager.PlayOneShotAS(correct);
            cat.selectedShape = shape;
            classRoom.shapeToEnable = shape;
            classRoom.cancelActions = true;
            //cat.HideAllShapes();
            hand.SetActive(false);
            AudioManager.audioManager.PlayOneShotVoice(shapeName);            
            charAnim.SetTrigger("Trans2Draw");
            charAnim.SetTrigger(paramName);
            //StartCoroutine(cat.ActivateShape());
        }
        
    }
}
