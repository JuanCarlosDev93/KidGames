using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LocalLoading : MonoBehaviour
{

    private CanvasGroup loadingScrn;
    [SerializeField] private int delayTime;

    private void Start()
    {
        loadingScrn = gameObject.GetComponent<CanvasGroup>();
    }

    public void StartLoading()
    {
        loadingScrn.DOFade(1,1).OnComplete(()=> StartCoroutine(DelayedLoading()));
    }

    IEnumerator DelayedLoading()
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EndLoading()
    {
        loadingScrn.DOFade(1, 1).OnComplete(() => StartCoroutine(DelayedLoading()));
    }
}
