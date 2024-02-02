using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Button openSettingsBtn;
    [SerializeField] private Button closeSettingsBtn;
    
    public void OpenMenu()
    {
        settingsMenu.SetActive(true);
        settingsMenu.GetComponent<CanvasGroup>().DOFade(1, 0.5f).OnComplete(() => closeSettingsBtn.interactable = true);
    }
    public void CloseMenu()
    {
        settingsMenu.GetComponent<CanvasGroup>().DOFade(0, 0.5f).OnComplete(() => OnCloseMenu());
    }
    void OnCloseMenu()
    {
        openSettingsBtn.interactable = true;
        settingsMenu.SetActive(false);
    }
}
