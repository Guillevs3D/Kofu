using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistencia : MonoBehaviour
{
    static Persistencia instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        PlayerPrefs.DeleteAll();
    }
}
