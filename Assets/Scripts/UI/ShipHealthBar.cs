using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealthBar : MonoBehaviour
{
    private GameObject Player;

    private void Awake() {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        transform.LookAt(Player.transform.position + transform.up*1.375f);
    }
}
