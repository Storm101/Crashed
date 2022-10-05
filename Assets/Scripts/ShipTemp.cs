using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTemp : MonoBehaviour
{
    public GameObject win;

    private EnemyTime enemy;
    private bool wave = false;

    private void Awake() {
        enemy = GetComponent<EnemyTime>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if (other.GetComponent<CollectionSystem>().PartsCollected == 1 && other.GetComponent<CollectionSystem>().PartsNeeded == 1) {
                enemy.inWave = true;
                wave = true;

                //win.SetActive(true);
            }
        }
    }

    private void Update() {
        if (wave == true && enemy.inWave == false) {
            win.SetActive(true);
        }
    }
}
