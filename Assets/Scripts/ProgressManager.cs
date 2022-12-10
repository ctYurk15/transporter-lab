using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    public Text coinsText;
    public int[] levelsCoins;

    private void Start()
    {
        updateUI();
    }

    public void levelCompleted(int level_number)
    {
        int coins = PlayerPrefs.GetInt("TransporterCoins");
        coins += levelsCoins[level_number];
        PlayerPrefs.SetInt("TransporterCoins", coins);
        updateUI();
    }

    public void updateUI()
    {
        int coins = PlayerPrefs.GetInt("TransporterCoins");
        coinsText.text = "Coins: "+coins;
    }
}
