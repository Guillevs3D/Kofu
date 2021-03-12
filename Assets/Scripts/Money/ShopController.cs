using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ShopController : MonoBehaviour
{
    public enum ObjetcType
    {
        Potion,
        Bullet
    }
    [Header("Shop")]
    public ObjetcType itemShop;
    public int price;
    [Header("Particle")]
    public GameObject particleCoin;
    public GameObject spawnCoin;
    [Header("Config UI")]
    public TextMeshProUGUI tmpGold;
    public UIController uiController;
    public LifePlayer lifePlayer;
    public ControlPlayer player;

    private int moneyPlayer = 0;
    bool triggered;
    void Update()
    {

        if (triggered && CrossPlatformInputManager.GetButtonDown("Interac"))
        {
            Buy();
        }
    }

    private void Buy()
    {
        moneyPlayer = int.Parse(tmpGold.text);
         if (moneyPlayer > price)
        {
            moneyPlayer -= price;
            tmpGold.text = moneyPlayer.ToString();
            GameObject part = Instantiate(particleCoin, spawnCoin.transform.position, transform.rotation);
            part.GetComponent<ParticleSystem>().Emit(price);
            //sonido caja registradora
            GameManager.instance.audioControl.sellItems.Play();

            if (itemShop == ObjetcType.Bullet)
            {
                player.countBullet++;
                player.Refresh();
            }
            else
            {
                lifePlayer.countPocimas++;
                lifePlayer.Refresh();
            }
         }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            uiController.canvasShop.SetActive(true);
            if (itemShop == ObjetcType.Bullet)
                uiController.buyBullet.SetActive(true);
            else
                uiController.buyPotion.SetActive(true);
            triggered = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            uiController.canvasShop.SetActive(false);
            if (itemShop == ObjetcType.Bullet)
                uiController.buyBullet.SetActive(false);
            else
                uiController.buyPotion.SetActive(false);
            triggered = false;
        }
    }
}
