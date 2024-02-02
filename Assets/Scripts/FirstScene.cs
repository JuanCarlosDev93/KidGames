using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour
{

    [SerializeField] private string sceneToTransitionTo;
    [SerializeField] private float delay;

    private void Start()
    {
        Invoke(nameof(Transition), delay);
    }
    public void Transition()
    {
        LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(sceneToTransitionTo));
    }
}
