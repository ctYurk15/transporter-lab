using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{
    public float speed = 1;
    public float hp = 10;
    public float damage = 5;

    public GameObject[] blastPoints;
    public GameObject body;
    public GameObject engines;
    public GameObject guns;

    public bool can_shoot = true;
    public bool can_fly = true;

    public void updateLook(float max_hp, float current_hp)
    {
        int hp_percents = (int)((current_hp * 100) / max_hp);

        if(hp_percents <= 66 && hp_percents > 33)
        {
            can_shoot = false;
            guns.gameObject.SetActive(false);
        }
        else if (hp_percents <= 33 && hp_percents > 0)
        {
            can_fly = false;
            can_shoot = false;

            guns.gameObject.SetActive(false);
            engines.gameObject.SetActive(false);
        }
    }

    public void reEnable()
    {
        can_shoot = true;
        can_fly = true;

        guns.gameObject.SetActive(true);
        engines.gameObject.SetActive(true);
    }
}
