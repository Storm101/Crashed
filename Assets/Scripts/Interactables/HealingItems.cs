using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItems : MonoBehaviour
{
    public float bobTime;
    private bool bobUp = true;
    private void Update()
    {
        if (bobTime < 0.5f && bobUp)
            bobTime += Time.deltaTime;
        else if (bobTime >= 0.5f && bobUp)
            bobUp = false;

        if (bobTime > -0.5f && !bobUp)
            bobTime -= Time.deltaTime;
        else if (bobTime <= -0.5f)
            bobUp = true;

        transform.Translate(new Vector3(0, bobTime, 0) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if (other.GetComponent<PlayerHealth>().health != other.GetComponent<PlayerHealth>().maxHealth) {
                other.GetComponent<PlayerHealth>().health += 25;
                Destroy(gameObject);
            }
        }
    }
}
