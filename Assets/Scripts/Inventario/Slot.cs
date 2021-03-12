using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public GameObject item;
    public int ID;
    public MineralTipo type = MineralTipo.Nada;
    public string description;
    public Sprite icon;
    public Transform slotIconGameObject;
    Sprite prevImage;

    private void Start()
    {
        slotIconGameObject = transform.GetChild(0);
        prevImage = slotIconGameObject.GetComponent<Image>().sprite;
    }
    public void UpdateSlot()
    {
        slotIconGameObject.GetComponent<Image>().sprite = icon;
    }

    internal void Empty()
    {
        slotIconGameObject.GetComponent<Image>().sprite = prevImage;
        type = MineralTipo.Nada;
    }
}
