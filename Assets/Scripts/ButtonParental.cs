using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonParental : MonoBehaviour
{
    [SerializeField] Parental parental;
    public int number;
    public bool isCorrect;
    public Color defaultColor;
    public Color pressedColor;
    public Button buttonC;

    private void Start()
    {
        buttonC = GetComponent<Button>();
    }

    public void OnClicBtn()
    {
        if (isCorrect)
        {
            isCorrect = false;
            buttonC.image.color = pressedColor;
            parental.NextButton();
        }
    }
}
