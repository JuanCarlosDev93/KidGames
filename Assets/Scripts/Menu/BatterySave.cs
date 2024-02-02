using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BatterySave : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private int frameRate = 60;
    [SerializeField] private bool batterySaveEnabled = false;

    private void Awake()
    {
        if (Settings.instance.batterySaveEnabled)
        {
            frameRate = 30;
            buttonText.text = "Activado";
            Application.targetFrameRate = frameRate;
        }
        else
        {
            frameRate = 60;
            buttonText.text = "Desactivado";
            Application.targetFrameRate = frameRate;
        }
    }

    public void ActiveBatterySave()
    {        
        SetBatterySave();
        Settings.instance.SaveData();
    }

    void SetBatterySave()
    {
        if (batterySaveEnabled == false)
        {
            batterySaveEnabled = true;
            frameRate = 30;
            buttonText.text = "Activado";
            Application.targetFrameRate = frameRate;
        }
        else
        {
            batterySaveEnabled = false;
            frameRate = 60;
            buttonText.text = "Desactivado";
            Application.targetFrameRate = frameRate;
        }
        
    }
}
