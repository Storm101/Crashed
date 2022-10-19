using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    //Lifetime variables
    public float projectileLife;
    private float timer;

    //Damage variable
    public float damage;

    public bool isEnemyBullet;


    public GameObject HitWall;
    public GameObject HitEnemy;
    public GameObject HitShip;

    //Wait until the timer runs out, then destroy the bullet
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= projectileLife)
            Destroy(gameObject);
    }

    //If the bullet hits an object, destroy the bullet
    private void OnCollisionEnter(Collision other) {
        ContactPoint contact = other.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);

        if (isEnemyBullet)
        {
            //If the bullet hits the player, decrease their health by the bullet's damage
            if (other.gameObject.tag == "Player")
                other.gameObject.GetComponent<PlayerHealth>().health -= damage;
            //If the bullet hits an enemy, decrease their health by the bullet's damage
            else if (other.gameObject.tag == "Ship") {
                other.gameObject.GetComponent<ShipHealth>().health -= damage;
                GameObject hitParticle = Instantiate(HitShip);
                hitParticle.transform.position = transform.position;
                hitParticle.transform.rotation = rotation;
            }
            else {
                GameObject hitParticle = Instantiate(HitWall);
                hitParticle.transform.position = transform.position;
                hitParticle.transform.rotation = rotation;
            }
        }
        else
        {
            if (other.gameObject.tag == "Enemy") {
                other.gameObject.GetComponent<EnemyHealth>().health -= damage;
                GameObject hitParticle = Instantiate(HitEnemy);
                hitParticle.transform.position = transform.position;
                hitParticle.transform.rotation = rotation;
            }
            else if (other.gameObject.tag == "Ship") {
                GameObject hitParticle = Instantiate(HitShip);
                hitParticle.transform.position = transform.position;
                hitParticle.transform.rotation = rotation;
            }
            else {
                GameObject hitParticle = Instantiate(HitWall);
                hitParticle.transform.position = transform.position;
                hitParticle.transform.rotation = rotation;
            }
        }


        //Destroy bullet
        Destroy(gameObject);
    }
}
