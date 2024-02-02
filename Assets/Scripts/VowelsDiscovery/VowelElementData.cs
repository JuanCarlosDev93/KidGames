using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class VowelWordData
{
    public Sprite elementImg;
    public string elementName;
    public string[] nameSyllable;
    public AudioClip nameAC;
    public AudioClip[] syllablesAC;
}

[CreateAssetMenu(fileName = "VowelData", menuName = "ScriptableObjects/VowelElementData", order = 1)]
public class VowelElementData : ScriptableObject
{
    public VowelWordData[] vowelWordDatas;
}
