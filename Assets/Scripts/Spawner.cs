using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] positions;
    public GameObject[] asteroidPrefabs;

    public Crystal crystal;

    public int[] levelsWaves;
    public int[] levelsCrystals;

    public int spawnChance = 3;
    public float interval = 1;

    public int level = 0;
    public bool isDelivery = true;

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
        int crystal_position = Random.Range(0, positions.Length);
        bool spawned_crystal = false;

        if (Random.Range(0, spawnChance) == 0 && !isDelivery)
        {
            spawned_crystal = true;
            Instantiate(crystal, positions[crystal_position].transform.position, Quaternion.identity);
        }

        for (int i = 0; i < positions.Length; i++)
        {
            if(Random.Range(0, spawnChance) == 0 && (isDelivery || (!isDelivery && i != crystal_position && !spawned_crystal)))
            {
                Instantiate(asteroidPrefabs[level], positions[i].transform.position, Quaternion.identity);
            }
        }

        

    }
}
