using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityStandardAssets.CrossPlatformInput;



public class SellItems : MonoBehaviour
{
    [Header("Config Shop")]
    public int conversionCobre = 1;
    public int conversionHierro = 5;
    public int conversionGemaVerde = 9;
    public int conversionGemaAguamarina = 10;
    public int conversionGemaMorada = 15;
    [Header("Particle")]
    public GameObject particleCoin;
    public GameObject spawnCoin;
    [Header("Config UI")]
    public TextMeshProUGUI tmpGold;
    public UIController uiController;
    public Inventory inventory;
    private int moneyPlayer = 0;

    bool triggered;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (triggered && CrossPlatformInputManager.GetButtonDown("Interac"))
        {
          Sell();         
           print("sell!!!!");

        }
    }
   

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            uiController.canvasShop.SetActive(true);
            uiController.sell.SetActive(true);
            triggered = true;

        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            uiController.canvasShop.SetActive(false);
            uiController.sell.SetActive(false);
            triggered = false;
        }
    }
    public void Sell()
    {        
        int prevMoney = moneyPlayer;        
        moneyPlayer += inventory.GetMaterial(MineralTipo.Cobre) * conversionCobre;
        moneyPlayer += inventory.GetMaterial(MineralTipo.Hierro) * conversionHierro;
        moneyPlayer += inventory.GetMaterial(MineralTipo.GemaVerde) * conversionGemaVerde;
        moneyPlayer += inventory.GetMaterial(MineralTipo.GemaAguamarina) * conversionGemaAguamarina;
        moneyPlayer += inventory.GetMaterial(MineralTipo.GemaMorada) * conversionGemaMorada;
        if (moneyPlayer > prevMoney)
        {
            tmpGold.text = moneyPlayer.ToString();
            GameObject part = Instantiate(particleCoin, spawnCoin.transform.position, transform.rotation);
            part.GetComponent<ParticleSystem>().Emit(moneyPlayer - prevMoney);
            //sonido caja registradora
            GameManager.instance.audioControl.sellItems.Play();
        }
    }
}
