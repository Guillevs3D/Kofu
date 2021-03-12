using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MineralTipo
{
    Cobre,
    Hierro,
    GemaVerde,
    GemaAguamarina,
    GemaMorada,
    Nada
}

public class Minar : MonoBehaviour
{
    
    private void OnTriggerStay(Collider other)
    {
        switch (other.tag)
        {
            case "Mena":                
            break;            
        }
    }

}
