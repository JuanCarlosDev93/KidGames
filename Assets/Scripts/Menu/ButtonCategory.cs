using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCategory : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private AudioClip categoryNameAC;

    private void Start()
    {
        anim = GetComponent<Animator>();   
    }
    public void OpenCloseMenu(bool openMenu)
    {
        anim.SetBool("OpenMenu", openMenu);
    }
    public void PlayAudioEffect()
    {
        AudioManager.audioManager.PlayOneShotAS(AudioManager.audioManager.swooshSFX);
    }
    public void PlayCategoryName()
    {
        AudioManager.audioManager.PlayOneShotVoice(categoryNameAC);
    }    

}
