using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int winDelay = 5;
    public Spawner spawner;
    public Sky sky;
    public ProgressManager progressManager;

    public GameObject deathModal;
    public GameObject winModal;
    public GameObject menuModal;

    public Ship ship;

    public Text hpText;
    public Text skinText;
    public Text levelText;
    public Text levelTypeText;
    public Text crystalsText;

    private int selected_skin = 0;
    private int selected_level = 0;
    private bool shipping_level = true;

    private int wavesCount = 10;

    private void Start()
    {
        Time.timeScale = 0f;
    }

    private void endLevel()
    {
        spawner.gameObject.SetActive(false);
        CancelInvoke(nameof(endLevel));

        InvokeRepeating(nameof(winLevel), winDelay, winDelay);
    }

    private void winLevel()
    {
        CancelInvoke(nameof(winLevel));

        clearAsteroids();
        Time.timeScale = 0f;

        hpText.gameObject.SetActive(false);
        crystalsText.gameObject.SetActive(false);

        winModal.SetActive(true);
        progressManager.levelCompleted(selected_level);
    }

    public void checkCrystals(int crystals)
    {
        int crystals_left = spawner.levelsCrystals[selected_level] - crystals;
        crystalsText.text = "Crystals left: " + (crystals_left > 0 ? crystals_left.ToString() : "collected all");

        if (!shipping_level && crystals >= spawner.levelsCrystals[selected_level])
        {
            endLevel();
        }
    }

    public void selectSkin(int skin_number)
    {
        selected_skin = skin_number;
        skinText.text = "Skin: " + (selected_skin + 1);
    }

    public void selectShippingLevel(int level_number)
    {
        shipping_level = true;
        selected_level = level_number;

        levelText.text = "Level: " + (selected_level + 1);
        levelTypeText.text = "Type: shipping";
    }

    public void selectCollectingLevel(int level_number)
    {
        shipping_level = false;
        selected_level = level_number;

        levelText.text = "Level: " + (selected_level + 1);
        levelTypeText.text = "Type: collecting";
    }

    public void startLevel()
    {
        ship.skin = selected_skin;
        spawner.level = selected_level;
        spawner.isDelivery = shipping_level;
        sky.setMaterial(selected_level);

        crystalsText.gameObject.SetActive(!shipping_level);
        crystalsText.text = "Crystals left: " + spawner.levelsCrystals[selected_level];

        wavesCount = spawner.levelsWaves[selected_level];

        Time.timeScale = 1f;
        ship.Restore();

        deathModal.SetActive(false);
        winModal.SetActive(false);
        menuModal.SetActive(false);

        spawner.gameObject.SetActive(true);
        hpText.gameObject.SetActive(true);

        //how much time it takes to spawn given amount of waves
        if (shipping_level)
        {
            float time = wavesCount * spawner.interval;
            InvokeRepeating(nameof(endLevel), time, time);
        }
    }

    public void Death()
    {
        CancelInvoke(nameof(endLevel));
        clearAsteroids();
        clearBlasts();

        hpText.gameObject.SetActive(false);
        crystalsText.gameObject.SetActive(false);

        Time.timeScale = 0f;
        deathModal.SetActive(true);
    }

    public void Menu()
    {
        deathModal.SetActive(false);
        winModal.SetActive(false);
        menuModal.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void clearAsteroids()
    {
        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();
        for (int i = 0; i < asteroids.Length; i++)
        {
            Destroy(asteroids[i].gameObject);
        }
    }

    private void clearBlasts()
    {
        ShipBlast[] shipBlasts = FindObjectsOfType<ShipBlast>();
        for (int i = 0; i < shipBlasts.Length; i++)
        {
            Destroy(shipBlasts[i].gameObject);
        }
    }

    public bool isShipping()
    {
        return shipping_level;
    }
}
