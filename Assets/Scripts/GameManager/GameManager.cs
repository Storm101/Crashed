using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }
    
    public bool Grounded = false;

    public bool[] groupDetection;

    private int gameProgression = 0;
    //1, 2, 3 are items
    //4 is boss
    //5 is win

    public Toggle[] checklist;

    public void CollectItem(GameObject item) {
        item.SetActive(false);
        if (gameProgression < checklist.Length) {
            checklist[gameProgression].isOn = true;
            gameProgression++;
        }
        else {
            Debug.LogError("More items than checklists");
        }
    }

    private void Update() {
        if (gameProgression == 5) {
            //win
        }
    }
}
