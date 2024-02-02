using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class SyllablePlayer : MonoBehaviour
{
    [SerializeField] private VowelDiscovery vowelDiscovery;
    public Transform playerInitialPos;
    [SerializeField] GameObject hand;
    [SerializeField] Transform playerCenterPos;
    [SerializeField] TextMeshPro oneSyllable;
    [SerializeField] TextMeshPro twoSyllable;
    [SerializeField] TextMeshPro threeSyllable;
    [SerializeField] TextMeshPro fourSyllable;
    [SerializeField] SyllableText syllableTextObj;
    [SerializeField] private Transform syllablePos;
    [SerializeField] private Transform syllableTxtEndPos;
    [SerializeField] private SpriteRenderer syllablePlayerSprite;
    [SerializeField] private List<TextMeshPro> syllablesTxt;
    [SerializeField] private List<AudioClip> syllablesAC;
    private AudioClip wordClip;
    TextMeshPro tempSyllableText;
    [SerializeField] int wordLength;

    public void SetSyllableData(VowelWordData wordData)
    {
        wordLength = wordData.nameSyllable.Length;
        wordClip = wordData.nameAC;
        syllablePlayerSprite.sprite = wordData.elementImg;
        for (int i = 0; i < wordData.nameSyllable.Length; i++)
        {
            syllablesAC.Add(wordData.syllablesAC[i]);
            tempSyllableText = Instantiate(SyllableTextFit(wordData.nameSyllable[i]), syllablePos);
            tempSyllableText.text = wordData.nameSyllable[i];
            tempSyllableText.gameObject.SetActive(true);
            syllablesTxt.Add(tempSyllableText);
        }
    }

    private TextMeshPro SyllableTextFit(string syllable)
    {
        if (syllable.Length==1)
        {
            return oneSyllable;

        }
        if (syllable.Length == 2)
        {
            return twoSyllable;

        }
        if (syllable.Length == 3)
        {
            return threeSyllable;

        }
        if (syllable.Length == 4)
        {
            return fourSyllable;

        }
        return oneSyllable;
    }
    public void PlaySyllable()
    {
        syllablePos.DOMoveY(syllableTxtEndPos.position.y,0.5f).OnComplete(()=> StartCoroutine(PlaySequeceAudio()));           
    }

    IEnumerator PlaySequeceAudio()
    {
        
        AudioManager.audioManager.PlayOneShotVoice(wordClip);
        yield return new WaitForSeconds(wordClip.length + 0.02f);
        hand.transform.position = syllablesTxt[0].transform.position;
        hand.SetActive(true);
        for (int i = 0; i < wordLength; i++)
        {
            //syllablesTxt[i].elementNameTMP.transform.DOPunchScale(new Vector2(1.3f,1.3f),0.3f,1);
            syllablesTxt[i].color = Color.red;
            hand.transform.DOMoveX(syllablesTxt[i].transform.position.x,0.5f);
            AudioManager.audioManager.PlayOneShotVoice(syllablesAC[i]);
            yield return new WaitForSeconds(syllablesAC[i].length + 0.02f);
        }
        hand.SetActive(false);
        AudioManager.audioManager.PlayOneShotVoice(wordClip);
        yield return new WaitForSeconds(wordClip.length + 0.02f);
        syllablesAC.Clear();
        vowelDiscovery.ShowElements();
        vowelDiscovery.CheckElements();
        gameObject.SetActive(false);
        RemovechildrenText();
        
        ResetPlayer();
    }

    public void RemovechildrenText()
    {
        foreach (TextMeshPro syllableChild in syllablesTxt)
        {
            Destroy(syllableChild.gameObject);
        }
        syllablesTxt.Clear();
    }

    private void ResetPlayer()
    {
        syllablePos.position = playerInitialPos.position;
    }

}
