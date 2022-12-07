using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Movement
{
    public GameObject[] textures;

    private void Awake()
    {
        //pick-up texture
        int texture_index = Random.Range(0, textures.Length);
        textures[texture_index].SetActive(true);
    }
}
