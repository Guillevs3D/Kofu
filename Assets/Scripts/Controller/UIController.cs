using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityStandardAssets.CrossPlatformInput;

public class UIController : MonoBehaviour
{
    //Variable Gloabales
    [Header("Paneles")]
    public GameObject sell;
    public GameObject buyBullet;
    public GameObject buyPotion;
    public GameObject canvasShop;
    public GameObject pausePanel;
    public GameObject fullInvetory;

    // Start is called before the first frame update
    void Start()
    {
        sell.SetActive(false);
        buyPotion.SetActive(false);
        buyBullet.SetActive(false);
        canvasShop.SetActive(false);
        pausePanel.SetActive(false);
        fullInvetory.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        PanelGameOver();
    }
    //Activar desactivar el panel de Game Over
    public void PanelGameOver()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(!pausePanel.activeSelf);
        }
    }
}
