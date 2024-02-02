using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [SerializeField] private GameObject popUpPanel;
    [SerializeField] private Button closeBtn;

    //private void Start()
    //{
    //    AdmobAds.admobAds.onReward.AddListener(OpenMenu);
    //}
    public void OpenMenu()
    {
        popUpPanel.SetActive(true);
        popUpPanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f).OnComplete(() => closeBtn.interactable = true);
    }
    public void CloseMenu()
    {
        popUpPanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f).OnComplete(() => OnCloseMenu());
    }
    void OnCloseMenu()
    {
        popUpPanel.SetActive(false);
    }
}
