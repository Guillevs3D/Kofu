using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Ambiente")]
    //Ambiente
    public AudioSource ambientMine;
    public AudioSource ambientVillage;
    //Player
    [Header("Player")]
    public AudioSource walkPlayer;
    public AudioSource attack1;
    public AudioSource damage1;
    [Header("Shoot")]
    public AudioSource shoot;
    public AudioSource reload;

    //Enemigo
    [Header("Enemigo")]
    public AudioSource attackEnemy1;
    public AudioSource damageEnemy;

    //Sonidos del pueblo venta vida
    [Header("Iglesia Tienda")]
    public AudioSource sellItems;
    public AudioSource healtChurch;
}
