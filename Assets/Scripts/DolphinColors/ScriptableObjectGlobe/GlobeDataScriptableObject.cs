using UnityEngine;


[CreateAssetMenu(fileName = "GlobeData", menuName = "ScriptableObjects/GlobeDataScriptableObject", order = 1)]
public class GlobeDataScriptableObject : ScriptableObject
{
    public string colorName;
    public Color objectColor;
    public AudioClip colorAC;


}
