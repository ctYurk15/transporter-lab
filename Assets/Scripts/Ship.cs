using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : HP
{
    public float shipMargin = 0.6f;
    public float shipSpeed = 0.5f;
    public float blastDamage = 5;
    public float blastSpeed = 25; 
    public float initialHealth = 10; 

    public GameObject[] blastPoints;
    public Skin[] skins;
    public int skin = 1;

    public GameObject shipBlast;
    public GameManager gameManager;

    public Text hpText;

    private float topEdge;
    private float bottomEdge;
    private Vector3 initialPosition;
    private Skin selected_skin;

    void Start()
    {
        bottomEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).y + shipMargin;
        topEdge = -1 * bottomEdge;

        initialPosition = transform.position;
        selectSkin();
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
            foreach(GameObject blastPoint in selected_skin.blastPoints)
            {
                GameObject newShipBlast = Instantiate(shipBlast, blastPoint.transform.position, Quaternion.identity);
                newShipBlast.GetComponent<ShipBlast>().damage = blastDamage;
                newShipBlast.GetComponent<ShipBlast>().speed = blastSpeed;
            }
        }
    }

    protected override void Death()
    {
        gameObject.SetActive(false);
        gameManager.Death();
    }

    protected override void UpdateHealth(float new_hp)
    {
        hpText.text = "HP: " + new_hp;
    }

    public void Restore()
    {
        health = initialHealth;
        UpdateHealth(health);

        transform.position = initialPosition;

        gameObject.SetActive(true);
    }

    public void selectSkin()
    {
        selected_skin = skins[skin];
        selected_skin.gameObject.SetActive(true);

        health = selected_skin.hp;
        initialHealth = selected_skin.hp;
        shipSpeed = selected_skin.speed;
        UpdateHealth(health);
    }
}
