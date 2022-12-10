using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    public Text coinsText;
    public int[] levelsCoins;
    public int[] shipPrices;

    public Button[] shipSelectButtons;
    public Button[] shipBuyButtons;

    private void Start()
    {
        /*PlayerPrefs.SetString("TransporterShipsOwning0", "1");
        PlayerPrefs.SetString("TransporterShipsOwning0", "0");
        PlayerPrefs.SetString("TransporterShipsOwning0", "0");
        PlayerPrefs.SetInt("TransporterCoins", 13);*/

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

        string[] owning_ships = getShipsAvailable();
        for(int i = 0; i < shipSelectButtons.Length; i++)
        {
            Debug.Log(JsonUtility.ToJson(owning_ships));
            shipSelectButtons[i].interactable = owning_ships[i] == "1";
            shipBuyButtons[i].interactable = owning_ships[i] == "" && coins >= shipPrices[i];
            shipBuyButtons[i].GetComponentInChildren<Text>().text = owning_ships[i] == "1" ? "Already have" : "Price: " + shipPrices[i];
        }
    }

    public void buyShip(int ship_number)
    {
        int coins = PlayerPrefs.GetInt("TransporterCoins");
        string[] owning_ships = getShipsAvailable();

        owning_ships[ship_number] = "1";
        coins -= shipPrices[ship_number];

        PlayerPrefs.SetString("TransporterShipsOwning"+ship_number, owning_ships[ship_number]);
        PlayerPrefs.SetInt("TransporterCoins", coins);

        updateUI();
    }

    private string[] getShipsAvailable()
    {
        string[] ships_owning = {
            PlayerPrefs.GetString("TransporterShipsOwning0"),
            PlayerPrefs.GetString("TransporterShipsOwning1"),
            PlayerPrefs.GetString("TransporterShipsOwning2")
        };

        if (ships_owning[0] == "") ships_owning[0] = "1";

        return ships_owning;
    }
}
