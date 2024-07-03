using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PuzzlePieceData", menuName = "ScriptableObjects/PuzzlePieceScriptableObject", order = 1)]
public class PuzzlePieceData : ScriptableObject
{
    public string setName;
    public Sprite[] puzzlePieces;
}
