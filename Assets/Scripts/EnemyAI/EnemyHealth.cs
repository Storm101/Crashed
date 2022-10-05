using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (GetComponent<EnemyMovement>() != null) {
                GameManager.Instance.groupDetection[GetComponent<EnemyMovement>().enemyGroup] = true;
            }
            else if (GetComponent<EnemyMovementShip>() != null) {
                GetComponent<EnemyMovementShip>().player = GetComponent<EnemyMovementShip>().realPlayer;
            }
        }
    }
}
