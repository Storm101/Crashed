using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("GameManager not found");
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.LogWarning("Multiple GameManagers found");
            Destroy(this);
            return;
        }

        Time.timeScale = 1;
    }

    public bool Grounded = false;

    public bool[] groupDetection;

    //private int gameProgression = 0;
    //1, 2, 3 are items
    //4 is boss
    //5 is win

    public int EnemiesLeft;
    public int ShipEnemiesLeft;

    public bool alertActive = false;

    public bool shipRepaired;

    private void Update()
    {
        //Used in waves
        EnemiesLeft = 0;
        ShipEnemiesLeft = 0;

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}