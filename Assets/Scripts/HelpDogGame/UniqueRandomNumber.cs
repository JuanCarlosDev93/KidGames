using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueRandomNumber : MonoBehaviour
{
    public static UniqueRandomNumber uniqueRandomNumber;
    //public List<int> numbers = new List<int>();

    private void Awake()
    {
        uniqueRandomNumber = this;
    }

    public void GenerateRandomNumber(int initial,int range, List<int> tempList)
    {
        int randomNumb = Random.Range(initial, range);
       
        while (tempList.Contains(randomNumb))
        {
            randomNumb = Random.Range(initial, range);
        }
        tempList.Add(randomNumb);
    }

    public void CleanNumberList(List<int> tempList)
    {
        tempList.Clear();
    }
}
