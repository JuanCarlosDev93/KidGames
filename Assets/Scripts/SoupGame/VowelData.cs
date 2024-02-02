using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class VowelDataSource
{
    public string vowelName;
    public Sprite vowelSprt;
    public AudioClip vowelSound;
}
public class VowelData : MonoBehaviour
{
    public VowelDataSource[] vowelsDataSource;
}
