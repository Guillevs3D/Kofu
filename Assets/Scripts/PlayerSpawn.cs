using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject spawnLevelPrevious;
    public GameObject spawnLevelNext;
    public GameObject playerReference;

    public Scenes prevScene;
    public Scenes nextScene;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.prevoisScene == prevScene)
        {
            playerReference.transform.position = spawnLevelPrevious.transform.position;
        }

        if (GameManager.prevoisScene == nextScene)
        {
            playerReference.transform.position = spawnLevelNext.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
