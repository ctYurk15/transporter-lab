using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int wavesCount = 10;
    public GameObject spawner;
    
    private void Start()
    {
        //how much time it takes to spawn given amount of waves
        float time = wavesCount * spawner.GetComponent<Spawner>().interval;
        InvokeRepeating(nameof(endLevel), time, time);
    }

    private void endLevel()
    {
        spawner.SetActive(false);
        CancelInvoke(nameof(endLevel));
    }
}
