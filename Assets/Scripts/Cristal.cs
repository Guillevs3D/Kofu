using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristal : MonoBehaviour
{
    public GameObject mineralPref;
    float force = 10;

    public void SoltarMineral()
    {
        GameObject go = Instantiate(mineralPref, transform.position, transform.rotation );
        Destroy(gameObject);
        go.GetComponent<Rigidbody2D>().AddForce (new Vector3(Random.Range(-force, force), force, 0));
    }

}
