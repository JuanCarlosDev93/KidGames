using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : MonoBehaviour
{
    Animator octoAnim;
    [SerializeField] VowelDiscovery vowelDiscovery;

    // Start is called before the first frame update
    void Start()
    {
        octoAnim = GetComponent<Animator>();
        //PlayIntroAnim();
    }

    public void PlayIntroAnim()
    {
        octoAnim.Play("Octo_Intro");
    }

    public void PlayHideAnim()
    {
        octoAnim.Play("Octo_Hide");
    }

    public void ShowVowels()
    {
        vowelDiscovery.ShowVowelsIntro();
    }
}
