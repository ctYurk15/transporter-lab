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
    
    public Skin[] skins;
    public int skin = 1;

    public GameObject shipBlast;
    public GameManager gameManager;

    public AudioSource blastSound;
    public AudioSource deathSound;

    public Text hpText;

    private float topEdge;
    private float bottomEdge;
    private Vector3 initialPosition;
    private Skin selected_skin;

    private int collected_crystals = 0;

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
        if(Input.GetKey(KeyCode.UpArrow) && transform.position.y <= topEdge && selected_skin.can_fly)
        {
            transform.position += new Vector3(0, shipSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && transform.position.y >= bottomEdge && selected_skin.can_fly)
        {
            transform.position += new Vector3(0, -1 * shipSpeed * Time.deltaTime);
        }

        //shoot
        if(Input.GetKeyDown(KeyCode.Space) && selected_skin.can_shoot)
        {
            foreach(GameObject blastPoint in selected_skin.blastPoints)
            {
                GameObject newShipBlast = Instantiate(shipBlast, blastPoint.transform.position, Quaternion.identity);
                newShipBlast.GetComponent<ShipBlast>().damage = blastDamage;
                newShipBlast.GetComponent<ShipBlast>().speed = blastSpeed;
            }
            blastSound.Play();
        }
    }

    protected override void Death()
    {
        selected_skin.gameObject.SetActive(false);
        deathSound.Play();
        gameManager.Death();
    }

    protected override void UpdateHealth(float new_hp)
    {
        hpText.text = "HP: " + new_hp;
        selected_skin.updateLook(initialHealth, new_hp);
    }

    public void Restore()
    {
        collected_crystals = 0;
        health = initialHealth;
        UpdateHealth(health);

        transform.position = initialPosition;

        gameObject.SetActive(true);
        disableSkins();
        selectSkin();
    }

    public void selectSkin()
    {
        selected_skin = skins[skin];
        selected_skin.gameObject.SetActive(true);
        selected_skin.reEnable();

        health = selected_skin.hp;
        initialHealth = selected_skin.hp;
        shipSpeed = selected_skin.speed;
        blastDamage = selected_skin.damage;

        UpdateHealth(health);
    }

    private void disableSkins()
    {
        foreach (Skin _skin in skins)
        {
            _skin.gameObject.SetActive(false);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Crystal")
        {
            collected_crystals++;
            gameManager.checkCrystals(collected_crystals);
            Destroy(collision.gameObject);
        }
    }
}
