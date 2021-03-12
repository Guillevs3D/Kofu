using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    public MineralTipo tipo;
    public Sprite icon;

    [HideInInspector]
    public bool pickedUp;

    //Actualiza los iconos de los items sprite
    private void Start()
    {
        icon = GetComponent<SpriteRenderer>().sprite;
    }
}
