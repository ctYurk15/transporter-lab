using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] positions;
    public GameObject[] asteroidPrefabs;
    public int[] levelsWaves;

    public int spawnChance = 3;
    public float interval = 1;
    public int level = 0;


    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), interval, interval);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        for(int i = 0; i < positions.Length; i++)
        {
            if(Random.Range(0, spawnChance) == 0)
            {
                GameObject asteroid = Instantiate(asteroidPrefabs[level], positions[i].transform.position, Quaternion.identity);
            }
        }
    }
}
