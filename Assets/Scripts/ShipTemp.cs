using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTemp : MonoBehaviour
{
    public GameObject win;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if (other.GetComponent<CollectionSystem>().PartsCollected == 1 && other.GetComponent<CollectionSystem>().PartsNeeded == 1) {
                win.SetActive(true);
            }
        }
    }
}
