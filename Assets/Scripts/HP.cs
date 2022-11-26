using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HP : MonoBehaviour
{
    public float health = 10;

    public void Damage(float damage_hp)
    {
        health -= damage_hp;
        if (health <= 0)
        {
            Death();
        }
    }

    protected abstract void Death();
}
