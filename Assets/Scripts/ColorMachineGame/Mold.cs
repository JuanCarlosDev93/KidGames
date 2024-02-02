using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mold : MonoBehaviour
{
    [SerializeField] private ColorMachine colorMachine = default;
    [SerializeField] private GameObject ball;
    [SerializeField] private AudioClip swoosh;
    [SerializeField] private AudioClip pop;
    [SerializeField] private AudioClip machine;
    [SerializeField] private AudioClip machineOpen;



    public void ActiveBall()
    {
        ball.SetActive(true);
    }
    public void PlayColorName()
    {
        StartCoroutine(colorMachine.PlayCreatedColorName());
    }
    public void BackLightEnter()
    {
        colorMachine.BacklightEnter();
    }
    public void PlaySwoosh()
    {
        AudioManager.audioManager.PlayOneShotAS(swoosh);
    }
    public void PlayPop()
    {
        AudioManager.audioManager.PlayOneShotAS(pop);
    }
    public void PlayMachineSound()
    {
        AudioManager.audioManager.PlayOneShotAS(machine);
    }
    public void PlayTryOpen()
    {
        AudioManager.audioManager.PlayOneShotAS(machineOpen);
    }

}
