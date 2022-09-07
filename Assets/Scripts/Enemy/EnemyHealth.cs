using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyHealth : MonoBehaviour
{
    public float health = 25;
    public float maxHealth = 25;

    public GameObject deathParticles;

    private void Update() {
        if (health <= 0) {
            GameObject deathParticle = Instantiate(deathParticles);
            deathParticle.transform.position = transform.position;
            Object.Destroy(gameObject);
        }

        if (health > maxHealth) {
            health = maxHealth;
        }

        if (health < maxHealth) {
            GameManager.Instance.groupDetection[GetComponent<EnemyMovement>().enemyGroup] = true;
        }
    }
}
