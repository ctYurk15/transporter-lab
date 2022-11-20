using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] positions;
    public GameObject asteroidPrefab;
    public int spawnChance = 3;

    public float interval = 1;


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
                GameObject asteroid = Instantiate(asteroidPrefab, positions[i].transform.position, Quaternion.identity);
            }
        }
    }
}
