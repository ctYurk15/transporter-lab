using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBlast : MonoBehaviour
{
    public float speed = 5;
    public float damage = 5;

    private float rightEdge;

    private void Start()
    {
        //visible edge
        rightEdge = -Camera.main.ScreenToWorldPoint(Vector3.zero).x + 1;
    }

    private void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
        if (transform.position.x > rightEdge)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            collision.gameObject.GetComponent<Asteroid>().Damage(damage);
            Destroy(gameObject);
        }
    }
}
