using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CollectionSystem : MonoBehaviour
{
    public GameObject Part1;
    public GameObject Part2;
    public GameObject Part3;
    public Toggle Toggle1;
    public Toggle Toggle2;
    public Toggle Toggle3;
    public GameObject Ship;
    public int PartsCollected;
    public int PartsNeeded = 3;

    void OnTriggerEnter(Collider col)
    {
        if(Part1)
        {
            if (col.gameObject.name == Part1.name)
            {
                Toggle1.isOn = true;
                Part1 = null;
                Destroy(col.gameObject);
                PartsCollected++;
            }
        }
        if (Part2)
        {
            if (col.gameObject.name == Part2.name)
            {
                Toggle2.isOn = true;
                Part2 = null;
                Destroy(col.gameObject);
                PartsCollected++;
            }
        }
        if (Part3)
        {
            if (col.gameObject.name == Part3.name)
            {
                Toggle3.isOn = true;
                Part3 = null;
                Destroy(col.gameObject);
                PartsCollected++;
            }
        }
        if (Ship)
        {
            if(PartsNeeded-PartsCollected == 0)
            {
                Debug.Log("Ship has been repaired!");
            }
            else
            {
                PartsNeeded = PartsNeeded - PartsCollected;
                PartsCollected = 0;
            }
        }
        
    }
}
