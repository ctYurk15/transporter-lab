using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int wavesCount = 10;
    public int winDelay = 5;
    public GameObject spawner;
    public GameObject deathModal;
    public GameObject winModal;
    public GameObject menuModal;

    public Ship ship;

    private void Start()
    {
        //how much time it takes to spawn given amount of waves
        /*float time = wavesCount * spawner.GetComponent<Spawner>().interval;
        InvokeRepeating(nameof(endLevel), time, time);*/
        Time.timeScale = 0f;
    }

    private void endLevel()
    {
        spawner.SetActive(false);
        CancelInvoke(nameof(endLevel));

        InvokeRepeating(nameof(winLevel), winDelay, winDelay);
    }

    private void winLevel()
    {
        CancelInvoke(nameof(winLevel));

        clearAsteroids();
        Time.timeScale = 0f;

        winModal.SetActive(true);
    }

    public void startLevelWithSkin(int skin_number)
    {
        ship.skin = skin_number;
        startLevel();
    }

    public void startLevel()
    {
        Time.timeScale = 1f;
        ship.Restore();

        deathModal.SetActive(false);
        winModal.SetActive(false);
        menuModal.SetActive(false); 

        //how much time it takes to spawn given amount of waves
        float time = wavesCount * spawner.GetComponent<Spawner>().interval;
        InvokeRepeating(nameof(endLevel), time, time);
        spawner.SetActive(true);
    }

    public void Death()
    {
        CancelInvoke(nameof(endLevel));
        clearAsteroids();
        clearBlasts();

        Time.timeScale = 0f;
        deathModal.SetActive(true);
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
}
