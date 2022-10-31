using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItems : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if (other.GetComponent<PlayerHealth>().health != other.GetComponent<PlayerHealth>().maxHealth) {
                other.GetComponent<PlayerHealth>().health += 25;
                Destroy(gameObject);
            }
        }
    }
}
