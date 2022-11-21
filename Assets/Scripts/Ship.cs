using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float shipMargin = 0.6f;
    public float shipSpeed = 0.5f;

    private float topEdge;
    private float bottomEdge;

    void Start()
    {
        bottomEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).y + shipMargin;
        topEdge = -1 * bottomEdge;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow) && transform.position.y <= topEdge)
        {
            transform.position += new Vector3(0, shipSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && transform.position.y >= bottomEdge)
        {
            transform.position += new Vector3(0, -1 * shipSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            Debug.Log("Damaged ship!");
        }
    }
}
