using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipHealth : MonoBehaviour
{
    public float health = 500;
    public float maxHealth = 500;

    public GameObject deathParticles;
    public Slider healthSlider;
    public RectTransform healthRect;

    private GameObject[] EnemyList;



    private void Update() {
        healthSlider.value = health / maxHealth;
        healthRect.offsetMax = new Vector2(health / maxHealth * 15, 0);

        if (health <= 0) {
            GameObject deathParticle = Instantiate(deathParticles);
            deathParticle.transform.position = transform.position;
            EnemyList = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject Enemy in EnemyList)
            {
                try
                {
                    Enemy.GetComponent<EnemyMovementShip>().player = GameObject.FindGameObjectWithTag("Player");
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
            Object.Destroy(gameObject);
            
        }

        if (health > maxHealth) {
            health = maxHealth;
        }
    }
}
