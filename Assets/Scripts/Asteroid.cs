using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject[] textures;
    public float speed = 5;
    public float hp = 10;

    private float leftEdge;

    private void Start()
    {
        //pick-up texture
        int texture_index = Random.Range(0, textures.Length);
        textures[texture_index].SetActive(true);

        //visible edge
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1;
    }

    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }

    public void Damage(float damage_hp)
    {
        hp -= damage_hp;
        if(hp <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
