using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    //Publicas
    public GameObject inventory;
    public GameObject slotHolder;
    public UIController uiController;

    //Privadas
    private bool inventoryEnabled;
    private int allSlots;
    private int enabledSlots;
    private GameObject[] slots;

    void Start()
    {
        allSlots = slotHolder.transform.childCount;
        slots = new GameObject [allSlots];
        for (int i = 0; i < allSlots; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryEnabled = !inventoryEnabled;
            uiController.fullInvetory.SetActive(false);
        }
        if (inventoryEnabled == true)
        {
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }
    }
  
    public bool AddItem(GameObject itemObject, Sprite itemIcon, MineralTipo type)
    {
        for (int i=0; i<allSlots; i++)
        {
            if (slots[i].GetComponent<Slot>().type == MineralTipo.Nada && itemObject.GetComponent<Item>().pickedUp == false)
            {
                itemObject.GetComponent<Item>().pickedUp = true;

                slots[i].GetComponent<Slot>().item = itemObject;
                slots[i].GetComponent<Slot>().icon = itemIcon;
                slots[i].GetComponent<Slot>().type = type;

                itemObject.transform.parent = slots[i].transform;
                itemObject.SetActive(false);

                slots[i].GetComponent<Slot>().UpdateSlot();
                
                return true;
            }
        }

        return false;
    }
    public int GetMaterial(MineralTipo mineral)
    {
        int count = 0;
        for (int i = 0; i < allSlots; i++)
        {
            if (slots[i].GetComponent<Slot>().type == mineral)
            {
                slots[i].GetComponent<Slot>().Empty();
                count++;
            }
        }
        return count;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            GameObject itemPickedUp = other.gameObject;
            Item item = itemPickedUp.GetComponent<Item>();
            AddItem(itemPickedUp, item.icon, item.tipo);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            uiController.fullInvetory.SetActive(true);
            Invoke("Desaparece", 3);
        }
    }
    private void Desaparece()
    {
            uiController.fullInvetory.SetActive(false);
    }
}

