using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings instance;
    [SerializeField] private int frameRate;
    public bool batterySaveEnabled;
    private const bool defaultBatSaving = false;

    
    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        SetFrameRate();
        //LoadData();
    }

    void SetFrameRate()
    {
        Application.targetFrameRate = frameRate;
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("BatterySaving", BoolToInt(batterySaveEnabled));
        PlayerPrefs.Save();
    }

    void LoadData()
    {
        //Load or create data for battery saving
        if (PlayerPrefs.HasKey("BatterySaving"))
        {
            batterySaveEnabled = IntToBool(PlayerPrefs.GetInt("BatterySaving"));
        }
        else
        {
            batterySaveEnabled = defaultBatSaving;            
        }
        SaveData();
        //Set Framerate
        if (batterySaveEnabled)
        {
            frameRate = 30;
        }
        else
        {
            frameRate = 60;
        }
        SetFrameRate();

    }

    int BoolToInt(bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }

    bool IntToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }
}
