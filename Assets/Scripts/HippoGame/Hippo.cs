using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hippo : MonoBehaviour
{
    [SerializeField] private FeedHippoGame feedHippoGame;
    [SerializeField] private Image hippoImage;
    [SerializeField] private Sprite hippoIdle;
    private Animator anim;
    private Collider2D col;
    [SerializeField] private GameObject globe;
    public GameObject raycastBlocker;
    [SerializeField] private ParticleSystem stars;
    [SerializeField] private ParticleSystem hearts;

    [Header("Audio")]    
    [SerializeField] private AudioClip correct;
    [SerializeField] private AudioClip incorrect;
    [SerializeField] private AudioClip openMouth;
    [SerializeField] private AudioClip closeMouth;
    [SerializeField] private AudioClip chew;
    [SerializeField] private AudioClip swallow;



    private void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();             
    }
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Food>().selected)
        {
            col.enabled = false;
            other.GetComponent<Food>().resetPos = false;
            AudioManager.audioManager.PlayOneShotAS(correct);
            stars.Play();
            other.GetComponent<Food>().PlayVoice();            
            other.gameObject.SetActive(false);
            other.GetComponent<Food>().SetInitialSilbling();
            StartEating();
            feedHippoGame.CheckAllFood();
            Debug.Log("Correct: " + other.gameObject.name);
        }
        else
        {
            other.GetComponent<Collider2D>().enabled = false;
            AudioManager.audioManager.PlayEffect(AudioEffectType.incorrect);
            Debug.Log("Incorrect: " + other.gameObject.name);
        }
    }

    public void OpenMouth()
    {
        if (!feedHippoGame.cancelActions)
        {
            anim.SetBool("OpenMouth", true);
        }
    }
    public void CloseMouth()
    {
        if (!feedHippoGame.cancelActions)
        {
            anim.SetBool("OpenMouth", false);
            anim.SetTrigger("CloseMouth");
        }
    }
    public void StartEating()
    {        
        col.enabled = false;
        raycastBlocker.SetActive(true);
        feedHippoGame.cancelActions = true;
        anim.SetBool("OpenMouth", false);
        anim.SetBool("Eating", true);
    }
    public void FinishEating()
    {
        if (anim.GetBool("Happy") == false)
        {
            col.enabled = true;
            raycastBlocker.SetActive(false);
            anim.SetBool("OpenMouth", false);
            anim.SetBool("Eating", false);
            globe.SetActive(true);
            feedHippoGame.cancelActions = false;
        }
        
    }

    public void PlayOpenMouth()
    {
        AudioManager.audioManager.PlayOneShotAS(openMouth);
    }
    public void PlayCloseMouth()
    {
        AudioManager.audioManager.PlayOneShotAS(closeMouth);
    }
    public void PlayChew()
    {
        AudioManager.audioManager.PlayOneShotAS(chew);
    }
    public void PlaySwallow()
    {
        AudioManager.audioManager.PlayOneShotAS(swallow);
    }
    public void SetHappyHippo()
    {
        anim.SetBool("Happy", true);
        hearts.Play();
    }
    public void SetHippoInitial()
    {
        hippoImage.sprite = hippoIdle;
        anim.SetBool("Happy", false);
        anim.SetBool("OpenMouth", false);
        anim.SetBool("Eating", false);
        hearts.Stop();
        col.enabled = true;
    }
    public void ActiveCollider()
    {
        col.enabled = true;
    }
}
