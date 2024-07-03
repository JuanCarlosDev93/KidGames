using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SilhouetteGame : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform lineSliced;
    [SerializeField] private Sprite[] objectSprites;
    [SerializeField] private SpriteSilhouette[] spriteSilhouettes;
    [SerializeField] private Silhouette[] silhouettes;
    [SerializeField] private List<int> randomIndexes = new List<int>();
    [SerializeField] private List<int> randomSprites = new List<int>();
    [SerializeField] private ParticleSystem starVFX;
    [SerializeField] private AudioClip starSFX;
    [SerializeField] private AudioClip starWinSFX;
    private float audioPitch = 1;



    private void Start()
    {
        SetSilhouettes();
        //Invoke(nameof(Intro), 1.5f);
        StartCoroutine(Intro());
    }
    IEnumerator Intro()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0;i < spriteSilhouettes.Length; i++)
        {            
            spriteSilhouettes[i].transform.DOMove(spriteSilhouettes[i].initialPos, 0.3f).OnComplete(()=> spriteSilhouettes[i].ActiveCollider());     
            AudioManager.audioManager.PlayEffect(AudioEffectType.swoosh);
            yield return new WaitForSeconds(0.4f);
        }
    }

    private void SetSilhouettes()
    {
        for (int i = 0; i < spriteSilhouettes.Length; i++)
        {
            UniqueRandomNumber.uniqueRandomNumber.GenerateRandomNumber(0, objectSprites.Length, randomSprites);
            int randomSprite = randomSprites[i];
            spriteSilhouettes[i].spriteRender.sprite = objectSprites[randomSprite];
            spriteSilhouettes[i].initialPos = spriteSilhouettes[i].transform.position;

            UniqueRandomNumber.uniqueRandomNumber.GenerateRandomNumber(0, spriteSilhouettes.Length, randomIndexes);
            int randomIndex = randomIndexes[i];
            spriteSilhouettes[i].selectedIndex = randomIndex;
            silhouettes[randomIndex].spriteRender.sprite = spriteSilhouettes[i].spriteRender.sprite;
            silhouettes[randomIndex].spriteMask.sprite = spriteSilhouettes[i].spriteRender.sprite;
            silhouettes[randomIndex].selectedIndex = spriteSilhouettes[i].selectedIndex;

            spriteSilhouettes[i].transform.position = silhouettes[randomIndex].transform.position;
        }
        
    }
    private void ActiveSilhouetteCollider(int index)
    {
        
        spriteSilhouettes[index].GetComponent<Collider2D>().enabled = true;
        Vector2 S = spriteSilhouettes[index].GetComponent<SpriteRenderer>().sprite.bounds.size;
        spriteSilhouettes[index].GetComponent<BoxCollider2D>().size = S;
        AudioManager.audioManager.PlayEffect(AudioEffectType.pop);
        //spriteSilhouette.GetComponent<BoxCollider2D>().offset = new Vector2((S.x / 2), 0);
        
    }
    public void CheckAllSilhouettes()
    {
        if (SillhouetteCompleted())
        {
            StartCoroutine(WinScreen());
        }
    }
    private bool SillhouetteCompleted()
    {
        for (int i = 0; i < spriteSilhouettes.Length;i++)
        {
            if (!spriteSilhouettes[i].isCompleted)
            {
                return false;
            }
        }
        return true;
    }
    IEnumerator WinScreen()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < spriteSilhouettes.Length; i++)
        {
            spriteSilhouettes[i].transform.DOPunchScale(new Vector2(0.2f, 0.2f), 0.5f, 1);
            EffectManager.effectManager.PlayEffect(Effects.stars, spriteSilhouettes[i].transform);
            AudioManager.audioManager.PlayAudioPitch(starSFX, AudioManager.audioManager.effectsAS, audioPitch);            
            yield return new WaitForSeconds(0.75f);
            audioPitch += 0.2f;
        }
        yield return new WaitForSeconds(0.1f);
        lineSliced.DOPunchScale(new Vector2(0.2f, 0.2f), 0.5f, 1);
        for (int i = 0; i < spriteSilhouettes.Length; i++)
        {
            spriteSilhouettes[i].transform.DOPunchScale(new Vector2(0.2f, 0.2f), 0.5f, 1);            
        }
        AudioManager.audioManager.PlayAudio(starSFX, AudioManager.audioManager.effectsAS);
        EffectManager.effectManager.PlayCustomEffect(starVFX, lineSliced.transform);
        yield return new WaitForSeconds(1.3f);
        AudioManager.audioManager.effectsAS.pitch = 1;
        gameManager.ActiveWinScreen();
    }
}
