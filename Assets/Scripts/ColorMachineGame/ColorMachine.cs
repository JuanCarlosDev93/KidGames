using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Colors
{
    Yellow = 1,
    Blue = 2,
    Red = 3    
}

public class ColorMachine : MonoBehaviour
{
    [Header("Main Objects")]
    [SerializeField] private GameObject winScreen;
    [SerializeField] private PlaylistGames playlist;
    [SerializeField] private Faucet faucet;
    [SerializeField] private ParticleSystem colorBallVFX = default;
    [SerializeField] private Transform panel;
    [SerializeField] private Mold mold;
    [SerializeField] private Button restartBtn;
    [SerializeField] private Transform moldUp;
    [SerializeField] private Transform moldDown;
    [SerializeField] private Transform backLight;
    [SerializeField] private Image background;
    [SerializeField] private SpriteRenderer ballSprite;
    [SerializeField] private SpriteRenderer baseBall1;
    [SerializeField] private SpriteRenderer baseBall2;
    [SerializeField] private SpriteRenderer plus;
    [SerializeField] private SpriteRenderer equal;
    [SerializeField] private Transform initialPos1;
    [SerializeField] private Transform initialPos2;
    [SerializeField] private Transform finalPosBall;
    [SerializeField] private Transform finalPos1;
    [SerializeField] private Transform finalPos2;
     private Vector3 moldUpInitialPos;
     private Vector3 moldDownInitialPos;
    [SerializeField] private ColorButton[] colorButtons;

    [Header("Final Colors")]
    [SerializeField] Color32 green;
    [SerializeField] Color32 orange;
    [SerializeField] Color32 purple;

    [Header("Audio")]    
    [SerializeField] private AudioClip swoosh;
    [SerializeField] private AudioClip greenName;
    [SerializeField] private AudioClip orangeName;
    [SerializeField] private AudioClip purpleName;
    [SerializeField] private AudioClip[] tutorials;
    [SerializeField] private AudioClip[] finalVoices;
    [SerializeField] private AudioClip selectedColorName;
    [SerializeField] private AudioClip pop;

    [Header("Game State")]
    public Colors color1 = Colors.Yellow;
    public Colors color2 = Colors.Yellow;
    public Color32 baseColor1;
    public Color32 baseColor2;
    Color tempColor = Color.white;
    public AudioClip firstColorAudio;
    public AudioClip secondColorAudio;
    public bool cancelActions = false;
    bool firstColor = false;

    private void Start()
    {
        moldDownInitialPos = moldDown.transform.position;
        moldUpInitialPos = moldUp.transform.position;
        InitializeGame();
    }
    void InitializeGame()
    {
        //StartCoroutine(PlayVoice(tutorials[0], 1.5f));
        Invoke(nameof(MoveFaucetDown), 0.5f);
        Invoke(nameof(MovePanelUp), 1f);
    }
    public void CombineColors(Colors colorToSet, AudioClip colorName, Color baseColor)
    {
        if (!firstColor)
        {
            firstColor = true;
            color1 = colorToSet;
            firstColorAudio = colorName;
            baseColor1 = baseColor;
            baseBall1.color = baseColor1;
            MoveObject(baseBall1.gameObject.transform, initialPos1);
            //StartCoroutine(PlayVoice(tutorials[1], 0.1f));
        }
        else
        {
            SecondScene();
            var mainCB = colorBallVFX.main;
            color2 = colorToSet;
            secondColorAudio = colorName;
            baseColor2 = baseColor;
            baseBall2.color = baseColor2;
            MoveObject(baseBall2.gameObject.transform, initialPos2);
            switch ((int)color1 + (int)color2)
            {
                case 3:
                    Debug.Log("Green");
                    ballSprite.color = green;
                    selectedColorName = greenName;
                    tempColor = green;
                    mainCB.startColor = tempColor;
                    break;
                case 4:
                    Debug.Log("Orange");
                    ballSprite.color = orange;
                    selectedColorName = orangeName;
                    tempColor = orange;
                    mainCB.startColor = tempColor;
                    break;
                case 5:
                    Debug.Log("Purple");
                    ballSprite.color = purple;
                    selectedColorName = purpleName;
                    tempColor = purple;
                    mainCB.startColor = tempColor;
                    break;

            }
            firstColor = false;
        }
    }

    void SecondScene()
    {
        faucet.transform.DOMoveY(15, 2).OnComplete(() => MoldClose());
        panel.DOMoveY(-15, 2);
    }
    void MoveObject(Transform object2Move, Transform endPos)
    {
        object2Move.DOMove(endPos.position, 0.5f);
    }
    void MoveFaucetDown()
    {        
        AudioManager.audioManager.PlayOneShotAS(swoosh);
        faucet.transform.DOMoveY(5.30f, 0.5f);
    }
    void MovePanelUp()
    {        
        AudioManager.audioManager.PlayOneShotAS(swoosh);
        panel.DOMoveY(-4.12f,0.5f);
    }
    
    void MoldClose()
    {
        //StartCoroutine(PlayVoice(tutorials[2], 0.3f));
        mold.GetComponent<Animator>().SetBool("MoldOpen", true);
        //mold.GetComponent<Animator>().Play("MoldClose");
    }
    public void BacklightEnter()
    {
        backLight.gameObject.SetActive(true);
        backLight.transform.DOScale(Vector3.one,1.5f).OnComplete(()=> BackLightRotation());
    }
    public void BacklightExit()
    {        
        backLight.transform.DOScale(Vector3.zero, 1.5f).OnComplete(()=> backLight.gameObject.SetActive(false));
    }
    void BackLightRotation()
    {
        backLight.DORotate(new Vector3(0,0,90),5).SetLoops(-1).SetEase(Ease.Linear);
    }
    void SetFinalScene()
    {
        BacklightExit();
        plus.DOFade(1,0.5f);
        equal.DOFade(1, 0.5f);
        MoveObject(baseBall1.gameObject.transform, finalPos1);
        MoveObject(baseBall2.gameObject.transform, finalPos2);
        MoveObject(ballSprite.gameObject.transform, finalPosBall);

        StartCoroutine(PlayCreatedColorTutorial());

    }
    public IEnumerator PlayCreatedColorName()
    {        
        //AudioManager.audioManager.PlayOneShotVoice(tutorials[3]);
        //yield return new WaitForSeconds(tutorials[3].length+0.1f);
        AudioManager.audioManager.PlayOneShotVoice(selectedColorName);
        //background.DOColor(ballSprite.color,1);
        yield return new WaitForSeconds(selectedColorName.length);
        ballSprite.transform.DOPunchScale(new Vector3(0.3f,0.3f,1),0.5f,6);
        AudioManager.audioManager.PlayEffect(AudioEffectType.star);
        EffectManager.effectManager.PlayEffect(Effects.stars, transform);
        //SetFinalScene();
        Invoke(nameof(ShowWinScreen), 2.5f);

    }
    public IEnumerator PlayCreatedColorTutorial()
    {
        //Combinar        
        AudioManager.audioManager.PlayOneShotVoice(finalVoices[0]);
        yield return new WaitForSeconds(finalVoices[0].length - 0.35f);
        //El Color
        AudioManager.audioManager.PlayOneShotVoice(finalVoices[1]);
        yield return new WaitForSeconds(finalVoices[1].length - 0.35f);
        //Nombre del primer Color
        AudioManager.audioManager.PlayOneShotVoice(firstColorAudio);
        baseBall1.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 1), 0.7f, 1);
        yield return new WaitForSeconds(firstColorAudio.length - 0.35f);
        //Mas
        AudioManager.audioManager.PlayOneShotVoice(finalVoices[2]);
        plus.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 1), 0.7f, 1);
        yield return new WaitForSeconds(finalVoices[2].length - 0.35f);
        //El Color
        AudioManager.audioManager.PlayOneShotVoice(finalVoices[1]);
        yield return new WaitForSeconds(finalVoices[1].length - 0.35f);        
        //Nombre del segundo Color
        AudioManager.audioManager.PlayOneShotVoice(secondColorAudio);
        baseBall2.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 1), 0.7f, 1);
        yield return new WaitForSeconds(secondColorAudio.length - 0.35f);
        //Es Igual
        AudioManager.audioManager.PlayOneShotVoice(finalVoices[3]);
        equal.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 1), 0.7f, 1);
        yield return new WaitForSeconds(finalVoices[3].length);
        //Color Final
        AudioManager.audioManager.PlayOneShotVoice(selectedColorName);
        ballSprite.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 1), 0.7f, 1);
        yield return new WaitForSeconds(selectedColorName.length);
        AudioManager.audioManager.PlayOneShotAS(AudioManager.audioManager.winSFX);
        Invoke(nameof(ShowWinScreen), 0.5f);
    }
    IEnumerator PlayVoice(AudioClip audio, float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.audioManager.PlayOneShotVoice(audio);

    }
    void ShowWinScreen()
    {
        AudioManager.audioManager.PlayOneShotAS(AudioManager.audioManager.winSFX);
        winScreen.SetActive(true);
        //winScreen.GetComponent<CanvasGroup>().DOFade(1, 1);
        winScreen.GetComponent<CanvasGroup>().DOFade(1, 1).OnComplete(() => playlist.startTimer = true);
        restartBtn.interactable = true;
    }

    public void ResetGame()
    {
        LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name));        
    }
}
