using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    public static PersistentData instance;
    public float menuLastPos = 0;
    
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
}
