using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subscriber : MonoBehaviour
{

    [SerializeField] private PopUp popUp;

    private void Start()
    {
       // AdmobAds.admobAds.onReward.AddListener(popUp.OpenMenu);
    }    
}
