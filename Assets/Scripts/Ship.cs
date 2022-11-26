using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : HP
{
    public float shipMargin = 0.6f;
    public float shipSpeed = 0.5f;
    public float blastDamage = 5;
    public float blastSpeed = 25;

    public GameObject[] blastPoints;
    public GameObject shipBlast;

    private float topEdge;
    private float bottomEdge;

    void Start()
    {
        bottomEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).y + shipMargin;
        topEdge = -1 * bottomEdge;
    }

    void Update()
    {
        //move
        if(Input.GetKey(KeyCode.UpArrow) && transform.position.y <= topEdge)
        {
            transform.position += new Vector3(0, shipSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && transform.position.y >= bottomEdge)
        {
            transform.position += new Vector3(0, -1 * shipSpeed * Time.deltaTime);
        }

        //shoot
        if(Input.GetKeyDown(KeyCode.Space))
        {
            foreach(GameObject blastPoint in blastPoints)
            {
                GameObject newShipBlast = Instantiate(shipBlast, blastPoint.transform.position, Quaternion.identity);
                newShipBlast.GetComponent<ShipBlast>().damage = blastDamage;
                newShipBlast.GetComponent<ShipBlast>().speed = blastSpeed;
            }
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
        Time.timeScale = 0f;
    }
}
