using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;

public class Parental : MonoBehaviour
{
    [SerializeField] private Button closeBtn;
    [SerializeField] private TextMeshProUGUI[] numbersTxt;
    [SerializeField] private int actualButton = 0;

    [SerializeField] private List<int> tempNumbList;
    [SerializeField] private ButtonParental[] buttons;
    [SerializeField] private UnityEvent onCompleteParental;


    void InitializeParental()
    {
        GenerateNumbers();
        actualButton = 0;
    }

    private string NumbToName(int number)
    {
        string numbName = default;
        switch (number)
        {
            case 0:
                numbName = "Cero";
                break;
            case 1:
                numbName = "Uno";
                break;
            case 2:
                numbName = "Dos";
                break;
            case 3:
                numbName = "Tres";
                break;
            case 4:
                numbName = "Cuatro";
                break;
            case 5:
                numbName = "Cinco";
                break;
            case 6:
                numbName = "Seis";
                break;
            case 7:
                numbName = "Siete";
                break;
            case 8:
                numbName = "Ocho";
                break;
            case 9:
                numbName = "Nueve";
                break;
            case 10:
                numbName = "Diez";
                break;
            default:
                numbName = "Cero";
                break;
        }
        
        return numbName;
    }

    void GenerateNumbers()
    {
        for (int i = 0; i < 3; i++)
        {
            UniqueRandomNumber.uniqueRandomNumber.GenerateRandomNumber(0, buttons.Length, tempNumbList);
            numbersTxt[i].text = NumbToName(buttons[tempNumbList[i]].number);
        }

        buttons[tempNumbList[actualButton]].isCorrect = true;
    }
    
    public void NextButton()
    {
        actualButton++;
        if (actualButton < tempNumbList.Capacity-1)
        {            
            buttons[tempNumbList[actualButton]].isCorrect = true;
        }
        else
        {
            actualButton = 0;
            UniqueRandomNumber.uniqueRandomNumber.CleanNumberList(tempNumbList);
            onCompleteParental?.Invoke();
        }        
    }

    public void OpenParental()
    {        
        gameObject.SetActive(true);
        InitializeParental();
        transform.GetComponent<CanvasGroup>().DOFade(1, 0.4f);
    }
    public void CloseParental()
    {
        transform.GetComponent<CanvasGroup>().DOFade(0,0.4f).OnComplete(()=> ResetParental());
    }
    public void CloseParentalBtn()
    {
        actualButton = 0;
        UniqueRandomNumber.uniqueRandomNumber.CleanNumberList(tempNumbList);
        transform.GetComponent<CanvasGroup>().DOFade(0, 0.4f).OnComplete(() => ResetParental());
    }
    public void ResetParental()
    {
        closeBtn.interactable = true;
        gameObject.SetActive(false);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].buttonC.image.color = buttons[i].defaultColor;
        }
    }

}
