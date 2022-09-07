using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit)) {
            if (hit.collider.tag == "Enemy") {
                hit.collider.gameObject.GetComponent<EnemyHealth>().health = 0;
            }
        }
    }
}
