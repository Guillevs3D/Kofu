using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum Scenes {Menu, Nivel_1, Nivel_2 };

public class GameManager : MonoBehaviour
{
    [Header("Audio Controller")]
    public AudioController audioControl;
    [Header("Paneles")]
    public GameObject loadingPanel;
    public GameObject panelGameOver;
    [Header("Script")]
    public MoveCharacter moveCharacter;
    public LifePlayer lifePlayer;
    [Header("Change Scenes")]
    public static GameManager instance;
    public static Scenes prevoisScene;
    public Scenes currentScenes;

    private void Awake()
    {
        //Esto nos permite acceder al gameManager desde cualquier sitio.
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        loadingPanel.SetActive(false);
        panelGameOver.SetActive(false);
    }

    public void NewGame()
    {
        print("Cambio Nivel");
        Time.timeScale = 1;
        prevoisScene = Scenes.Menu;
        SceneManager.LoadScene("Nivel_1");
        loadingPanel.SetActive(true);
        PlayerPrefs.DeleteAll();
    }    
    public void ExitButton()
    {
        Application.Quit();
    }

}
