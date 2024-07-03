using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PuzzleGame : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SpriteRenderer imageRef;
    [SerializeField] private Sprite[] puzzleSprites;
    [SerializeField] private PuzzlePiece[] puzzlePieces;
    [SerializeField] private PuzzleSocket[] puzzleSockets;
    [SerializeField] private Transform[] beginPosistions;
    [SerializeField] private List<int> randomIndexes = new List<int>();
    [SerializeField] private List<int> randomPosIndexes = new List<int>();

    [SerializeField] private AudioClip starWinSFX;
    [SerializeField] private ParticleSystem starVFX;

    Sprite[,] TileSprites;
    [SerializeField] private int NumberOfColumns;
    [SerializeField] private int NumberOfRows;
    [SerializeField] private Sprite SourceSprite;

    private void Start()
    {
        SetPieces();
        StartCoroutine(Intro());
    }
    IEnumerator Intro()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            puzzlePieces[i].transform.DOScale(new Vector2(0.65f,0.65f), 0.3f);
            puzzlePieces[i].transform.DOMove(puzzlePieces[i].initialPos, 0.3f).OnComplete(() => puzzlePieces[i].ActiveCollider());
            AudioManager.audioManager.PlayEffect(AudioEffectType.swoosh);
            yield return new WaitForSeconds(0.4f);
        }
    }

    private void SliceImage()
    {
        int PixelsToUnits = SourceSprite.texture.height / NumberOfRows;
        TileSprites = new Sprite[NumberOfColumns, NumberOfRows];
        for (int i = 0; i < NumberOfColumns; i++)
        {
            for (int y = 0; y < NumberOfRows; y++)
            {
                Rect theArea = new Rect(i * PixelsToUnits, y * PixelsToUnits, PixelsToUnits, PixelsToUnits);
                TileSprites[i, y] = Sprite.Create(SourceSprite.texture, theArea, Vector2.zero, PixelsToUnits);
            }
        }
    }

    private void SetPieces()
    {
        int spriteIndex = Random.Range(0, puzzleSprites.Length);
        imageRef.sprite = puzzleSprites[spriteIndex];

        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            puzzlePieces[i].spriteRender.sprite = imageRef.sprite;

            UniqueRandomNumber.uniqueRandomNumber.GenerateRandomNumber(0, beginPosistions.Length, randomPosIndexes);
            int randomPosIndex = randomPosIndexes[i];
            puzzlePieces[i].initialPos = beginPosistions[randomPosIndex].position;

            UniqueRandomNumber.uniqueRandomNumber.GenerateRandomNumber(0, puzzlePieces.Length, randomIndexes);
            int randomIndex = randomIndexes[i];

            puzzlePieces[i].selectedIndex = i;        
            puzzleSockets[i].selectedIndex = puzzlePieces[i].selectedIndex;

            puzzlePieces[i].transform.position = puzzleSockets[i].transform.position;
        }

    }
    private void ActivePieceCollider(int index)
    {
        puzzlePieces[index].GetComponent<Collider2D>().enabled = true;
        Vector2 S = puzzlePieces[index].GetComponent<SpriteRenderer>().sprite.bounds.size;
        puzzlePieces[index].GetComponent<BoxCollider2D>().size = S;
        AudioManager.audioManager.PlayEffect(AudioEffectType.pop);
        //spriteSilhouette.GetComponent<BoxCollider2D>().offset = new Vector2((S.x / 2), 0);
    }
    public void CheckAllPieces()
    {
        if (PieceCompleted())
        {
            StartCoroutine(WinScreen());
        }
    }
    private bool PieceCompleted()
    {
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            if (!puzzlePieces[i].isCompleted)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator WinScreen()
    {
        imageRef.DOFade(255,0.5f);
        foreach (PuzzlePiece puzzlePiece in puzzlePieces)
        {
            puzzlePiece.spriteRender.DOFade(0, 0.9f);
            puzzlePiece.spriteRenderLine.DOFade(0, 0.9f);
        }
        yield return new WaitForSeconds(1.2f);
        imageRef.transform.parent.DOPunchScale(new Vector2(0.2f,0.2f), 0.5f,1);
        AudioManager.audioManager.PlayAudio(starWinSFX, AudioManager.audioManager.effectsAS);
        EffectManager.effectManager.PlayCustomEffect(starVFX, imageRef.transform);
        yield return new WaitForSeconds(1.3f);
        gameManager.ActiveWinScreen();
    }
}
