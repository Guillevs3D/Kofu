using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class LifePlayer : MonoBehaviour
{
    //Variables Lobales Publiacas
    [Header("Configuration Life")]
    public float currentLife = 100;
    public float maxLife = 100;
    public bool isAlive = true;
    [Header("Pocimas")]
    public int countPocimas = 2;
    public float lifeCountPotion = 30;
    public GameObject particleHealt;
    public GameObject tmpPocima;
    [Header("Paneles")]
    public GameObject panelGameOver;
    [Header("Scripts")]
    public Slider slideHealt;
    public MoveCharacter player;
    public ControlPlayer controlPlayer;


    [Header("Good Mode")]
    public bool goodMode;

    // Start is called before the first frame update
    void Start()
    {
        slideHealt.value = currentLife;
        tmpPocima.GetComponent<TextMeshProUGUI>().text = countPocimas.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Potion"))
        {
            UsePocima();
        }
    }
        //Metodo de daño al player
        public void DamagePlayer(int damageAmount)
        {
            currentLife -= damageAmount;
            slideHealt.value = currentLife; //El valor de la vida se traspasa al slider
            //Sonido de daño
            GameManager.instance.audioControl.damage1.Play();
            if(currentLife <= 0)
            {
                player.anima.SetTrigger("Dead Player");
                isAlive = false;
                controlPlayer.enabled = false;
                panelGameOver.SetActive(true);
            }
        }
    public void UsePocima()
    {
        if (currentLife < 100)
        {
            countPocimas -= 1;
            currentLife += lifeCountPotion;
            slideHealt.value = currentLife;
            Refresh();
            GameManager.instance.audioControl.healtChurch.Play();
        }
        if (currentLife > 100)
        {
            currentLife = 100;
        }
    }

    public void Refresh()
    {
        tmpPocima.GetComponent<TextMeshProUGUI>().text = countPocimas.ToString();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "HealtZoneChurch" && currentLife < 100)
        {
            currentLife = 100;
            slideHealt.value = currentLife;
            Instantiate(particleHealt, transform.position, transform.rotation);
            GameManager.instance.audioControl.healtChurch.Play();
        }
    }
}
