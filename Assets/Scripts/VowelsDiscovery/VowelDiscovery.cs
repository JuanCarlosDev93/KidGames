using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VowelDiscovery : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    public SyllablePlayer syllablePlayer;
    public Octopus octopus;
    public VowelElement selectedVowel;
    [SerializeField] private VowelElement[] vowelsElements;
    [SerializeField] private VowelElement[] vowels;

    private void Start()
    {
        //gameManager.ShowTutorial();
        octopus.PlayIntroAnim();
    }
    public void CheckElements()
    {
        foreach (VowelElement vowel in vowelsElements)
        {
            if (!vowel.isCompleted)
            {
                return;
            }
        }
        HideElements();
        gameManager.ActiveWinScreen();
    }

    public void ResetElements()
    {
        foreach (VowelElement vowel in vowelsElements)
        {
            vowel.ResetElement();
        }
    }
    public void ShowElements()
    {
        selectedVowel.gameObject.SetActive(true);
        foreach (VowelElement vowel in vowelsElements)
        {
            if (!vowel.isCompleted)
            {
                vowel.gameObject.SetActive(true);
            }
        }
    }
    public void HideElements()
    {
        selectedVowel.gameObject.SetActive(false);
        foreach (VowelElement vowel in vowelsElements)
        {
            if (!vowel.isCompleted)
            {
                vowel.gameObject.SetActive(false);
            }
        }        
    }

    public void MoveElementToNewPos()
    {
        foreach (VowelElement vowel in vowelsElements)
        {
            vowel.MoveToNewPos();
        }
    }

    public void ShowVowelsIntro()
    {
        foreach (VowelElement vowel in vowels)
        {
            vowel.ShowVowel();
        }
    }
}
