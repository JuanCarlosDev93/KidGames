using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void OnRestartBtn()
    {
        AudioManager.audioManager.PlayOneShotAS(AudioManager.audioManager.popSFX);
        ResetGame();
    }
    void ResetGame()
    {
        LoadingScreen.Instance.Show(SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name));
    }
}
