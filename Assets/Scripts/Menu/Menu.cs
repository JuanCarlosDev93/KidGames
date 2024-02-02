using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class Menu : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;    
    [SerializeField] private bool useLastPosMenu;

    private void Start()
    {
        if (useLastPosMenu)
        {
            LoadMenuPos();
        }
    }
    private void LoadMenuPos()
    {
        scrollRect.horizontalNormalizedPosition = PersistentData.instance.menuLastPos;
    }

    public void SaveMenuPos()
    {
       PersistentData.instance.menuLastPos = scrollRect.horizontalNormalizedPosition;
    }

    
}

