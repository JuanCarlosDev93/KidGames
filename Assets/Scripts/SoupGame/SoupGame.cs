using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SoupGame : MonoBehaviour
{
    [Header("Vowels Data")]
    [SerializeField] private VowelData vowelData;
    [Header("Main Objects")]
    [SerializeField] private GameObject winScreen = default;
    [SerializeField] private Animator vowelsAnim;
    [SerializeField] private SpriteRenderer vowelSpriteRndr;
    [SerializeField] private Transform vowelsParent;
    [SerializeField] private CerealBox cerealBox = default;
    [SerializeField] private Dish dish = default;
    [SerializeField] private Button restartBtn;
    [SerializeField] private List<VowelSoup> vowels;
    [SerializeField] private List<VowelSoup> tempVowels;
    [Header("Audio")]    
    [SerializeField] private AudioClip pop;
    [SerializeField] private AudioClip[] tutorialVoice;
    [Header("Game State")]
    [Range(0, 8)]
    [SerializeField] private int vowelsQuantity;
    [SerializeField] private int vowelGroups = 0;
    private int totalVowelGroups = 5;
    private int totalVowels;
    public bool cancelActions;
    public bool playingIntro = true;
    public bool winCondition = false;
    private int randomNumber;
    private Color tempColor;
    private int randomColor;
    [SerializeField] private Color[] customColors = new Color[4];

    private void Start()
    {
        cancelActions = true;
        totalVowels = vowelsQuantity;
        SetVowels();
        //PlayIntroAnim();
        
    }
    void PlayIntroAnim()
    {   
        PlayLetter();
    }

    public void PlayLetter()
    {
        HighlightsVowelSprite();
        AudioManager.audioManager.PlayOneShotVoice(vowelData.vowelsDataSource[vowelGroups].vowelSound);
        cancelActions = false;
    }

    private void SetVowels()
    {
        SetInitialRandomVowels(vowelGroups);
        for (int i = 0; i < vowelsQuantity; i++)
        {            
            randomNumber = Random.Range(0, vowels.Capacity);        
            while (vowels[randomNumber].selected)
            {
                randomNumber = Random.Range(0, vowels.Capacity);
            }
            vowels[randomNumber].selected = true;
            //vowels[randomNumber].GetComponent<Image>().color = RandomCustomColors();
            vowels[randomNumber].SetVowelData(
                vowelData.vowelsDataSource[vowelGroups].vowelSprt,
                vowelData.vowelsDataSource[vowelGroups].vowelSound);
            //tempVowels.Add(vowels[randomNumber]);
        }
        vowelSpriteRndr.sprite = vowelData.vowelsDataSource[vowelGroups].vowelSprt;
        vowelSpriteRndr.color = RandomCustomColors();
    }
    void SetInitialRandomVowels(int discartedVowel)
    {
        foreach (VowelSoup vowel in vowels)
        {
            randomNumber = Random.Range(0, totalVowelGroups - 1);
            while (randomNumber == discartedVowel)
            {
                randomNumber = Random.Range(0,totalVowelGroups - 1);
            }
            vowel.GetComponent<SpriteRenderer>().sprite = vowelData.vowelsDataSource[randomNumber].vowelSprt;
            vowel.GetComponent<SpriteRenderer>().color = RandomCustomColors();
        }
    }
    Color RandomCustomColors()
    {
        randomColor = Random.Range(0, customColors.Length);
        tempColor = customColors[randomColor];
        return tempColor;
    }
    void ResetVowels()
    {
        foreach (VowelSoup vowel in vowels)
        {
            vowel.selected = false; 
        }
    }

    public void CheckAllVowels()
    {
        totalVowels--;
        if (vowelGroups >= totalVowelGroups-1 && totalVowels <= 0)
        {
            winCondition = true;
            dish.GetComponent<Animator>().Play("DishHided");
            cerealBox.GetComponent<Animator>().Play("BoxHided");
            StartCoroutine(ShowWinScreen(0.5f));
        }
        else
        {            
            if (totalVowels <= 0)
            {
                vowelGroups++;
                cancelActions = true;
                NextVowelGroup();
            }
        }
        
    }

    void NextVowelGroup()
    {
        ResetVowels();
        SetVowels();
    }
    public void ResetGame()
    {
        totalVowels = vowelsQuantity;        
    }
    
    void VowelsScaleDown()
    {
        foreach (VowelSoup vowel in vowels)
        {
            vowel.transform.DOScale(Vector2.zero,0.5f);
        }                     
    }    
    
    void PotIn()
    {        
        if (!winCondition)
        {
            foreach (VowelSoup vowel in vowels)
            {
                vowel.transform.localScale = Vector2.zero;
            }
            ResetGame();
            //pot.GetComponent<RectTransform>().DOAnchorPosX(potInitialPos.x, 1f).OnComplete(() => VowelsScaleUp());
        }

    }
    void TableOut()
    {
        //table.GetComponent<RectTransform>().DOAnchorPosX(-950, 1f).OnComplete(()=> TableIn());
    }
    void TableIn()
    {
        if (!winCondition)
        {
            //table.GetComponent<RectTransform>().DOAnchorPosX(tableInitialPos.x, 1f);
        }
    }
    IEnumerator PlayVoice(AudioClip audio, float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.audioManager.PlayOneShotVoice(audio);   
    }    
    
    void HighlightsVowelSprite()
    {
        vowelSpriteRndr.transform.DOPunchScale(new Vector2(0.5f,0.5f), 0.5f,1).SetLoops(2);
    }
    IEnumerator ShowWinScreen(float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.audioManager.PlayAudio(AudioManager.audioManager.winSFX, AudioManager.audioManager.winAS);
        winScreen.SetActive(true);
        winScreen.GetComponent<CanvasGroup>().DOFade(1, 1).OnComplete(()=> PlaylistGames.playlist.StartTimer());
        restartBtn.interactable = true;
    }
    public void RestartGame()
    {
        LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name));
    }
}
