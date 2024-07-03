using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigNumbersGame : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Pig pig;
    [SerializeField] private int actualCoin;
    [SerializeField] private Coin[] coins = new Coin[9];


    private void Start()
    {
        actualCoin = 0;
        coins[0].isSelected = true;
    }
    public void StartGame()
    {
        StartCoroutine(IntroCoins());
    }

    public void NextCoin()
    {
        if (actualCoin < coins.Length-1)
        {
            coins[actualCoin].isSelected = false;
            actualCoin++;
            coins[actualCoin].isSelected = true;
        }
        else
        {
            gameManager.ActiveWinScreen();
        }
    }

    IEnumerator IntroCoins()
    {
        yield return new WaitForSeconds(1f);
        pig.Intro();
        yield return new WaitForSeconds(1f);
        foreach (Coin coin in coins)
        {
            yield return new WaitForSeconds(0.2f);
            coin.Intro();
        }
    }
}
