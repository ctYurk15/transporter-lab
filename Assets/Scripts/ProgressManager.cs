using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    public Text coinsText;
    public int[] levelsCoins;
    public int[] shipPrices;

    public Button[] shipping_levelSelectButtons;
    public Button[] collecting_levelSelectButtons;

    public Button[] shipSelectButtons;
    public Button[] shipBuyButtons;

    public static int levels_count = 3;
    public static int ships_count = 3;

    private void Start()
    {
        /*PlayerPrefs.SetString("TransporterShipsOwning0", "1");
        PlayerPrefs.SetString("TransporterShipsOwning0", "0");
        PlayerPrefs.SetString("TransporterShipsOwning0", "0");
        PlayerPrefs.SetInt("TransporterCoins", 13);*/

        updateUI();
    }

    public void levelCompleted(int level_number, bool shipping_level)
    {
        //coins
        int coins = PlayerPrefs.GetInt("TransporterCoins");
        coins += levelsCoins[level_number];
        PlayerPrefs.SetInt("TransporterCoins", coins);

        //levels
        string save_cell_name = "TransporterCompletedLevel" + level_number + "_" 
            + (shipping_level ? "shipping" : "collecting");
        PlayerPrefs.SetString(save_cell_name, "1");

        updateUI();
    }

    public void updateUI()
    {
        int coins = PlayerPrefs.GetInt("TransporterCoins");
        coinsText.text = "Coins: "+coins;

        //ships
        string[] owning_ships = getShipsAvailable();
        for(int i = 0; i < ships_count; i++)
        {
            shipSelectButtons[i].interactable = owning_ships[i] == "1";
            shipBuyButtons[i].interactable = owning_ships[i] == "" && coins >= shipPrices[i];
            shipBuyButtons[i].GetComponentInChildren<Text>().text = owning_ships[i] == "1" ? "Already have" : "Price: " + shipPrices[i];
        }

        //levels
        string[][] levels_available = getLevelsAvailable();
        for (int i = 1; i < levels_count; i++) //1st level should always be available
        {
            //level available only if shipping & collecting parts of previous level is completed
            bool level_available = levels_available[i - 1][0] == "1" && levels_available[i - 1][1] == "1";
            shipping_levelSelectButtons[i].interactable = collecting_levelSelectButtons[i].interactable = level_available;
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
        string[] ships_owning = new string[ships_count];

        for (int i = 0; i < ships_count; i++)
        {
            ships_owning[i] = PlayerPrefs.GetString("TransporterShipsOwning"+i);
        }

        if (ships_owning[0] == "") ships_owning[0] = "1";

        return ships_owning;
    }

    private string[][] getLevelsAvailable()
    {
        string[][] levels_available = new string[levels_count][];

        for(int i = 0; i < levels_count; i++)
        {
            levels_available[i] = new string[] {
                PlayerPrefs.GetString("TransporterCompletedLevel"+i+"_shipping"),
                PlayerPrefs.GetString("TransporterCompletedLevel"+i+"_collecting"),
            };

            Debug.Log(PlayerPrefs.GetString("TransporterCompletedLevel" + i + "_shipping") + PlayerPrefs.GetString("TransporterCompletedLevel" + i + "_collecting"));
        }

        return levels_available;
    }
}
