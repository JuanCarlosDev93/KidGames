using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;

public class Globe : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Transform outroPos;
    [SerializeField] private SpriteRenderer globeSprtR;
    [SerializeField] private SpriteRenderer shineSprtR;
    [SerializeField] private Collider2D globeColl;
    [SerializeField] private ParticleSystem bubblePS;
    [SerializeField] private ParticleSystem bubbleExplodePS;
    [SerializeField] private int globeResistance;
    [SerializeField] private float globeSpeed;
    public AudioClip colorNameAC;
    [SerializeField] private AudioClip bubbleTrailAC;

    public bool canMove = false;
    public UnityEvent onClickGlobe;
    public UnityEvent onCompleteOutro;

    void Update()
    {
        if (canMove)
        {
            transform.position += new Vector3(0, globeSpeed, 0)*Time.deltaTime;
        }
    }

    void OnEnable()
    {
        GlobeIntroAnim();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        canMove = false;
        globeColl.enabled = false;
        AudioManager.audioManager.PlayEffect(AudioEffectType.pop);
        AudioManager.audioManager.PlayOneShotVoiceNew(colorNameAC);
        AudioManager.audioManager.PlayOneShotAS(bubbleTrailAC);
        bubbleExplodePS.Play();
        
        onClickGlobe?.Invoke(); 
        transform.DOPunchScale(new Vector2(0.2f,0.2f),0.2f,10).OnComplete(()=> GlobeOutroAnim());
    }

    public void SetGlobe(Color gColor, AudioClip gNameAC,float gSpeed, float gDuration, int gResistance, Transform endPos)
    {
        globeSprtR.color = gColor;
        colorNameAC = gNameAC;
        globeSpeed = gSpeed;
        globeResistance = gResistance;
        outroPos = endPos;
        globeColl.enabled = true;
        GetComponent<ObjectDuration>().objectDuration = gDuration;
    }
    public void InitiateGlobe()
    {

    }
    void GlobeIntroAnim()
    {        
        transform.DOScale(new Vector2(1,1),0.3f);
        transform.DOMoveY(-2f,0.3f).OnComplete(()=> GlobeMoveUp());
    }
    void GlobeMoveUp()
    {
        canMove = true;
    }

    void GlobeOutroAnim()
    {
        bubblePS.Play();
        transform.DOMoveY(20, 1.5f).OnComplete(()=> onCompleteOutro?.Invoke());
    }
    void FadeOutGlobe()
    {
        globeSprtR.DOFade(0,1);
        shineSprtR.DOFade(0,1);
    }

    void ResetGlobe()
    {
        
    }

    private void OnDisable()
    {
        bubblePS.Stop();
    }
}
