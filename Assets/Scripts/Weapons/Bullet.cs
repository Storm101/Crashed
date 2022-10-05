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

    //Wait until the timer runs out, then destroy the bullet
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= projectileLife)
            Destroy(gameObject);
    }

    //If the bullet hits an object, destroy the bullet
    private void OnTriggerEnter(Collider other)
    {
        if (isEnemyBullet)
        {
            //If the bullet hits the player, decrease their health by the bullet's damage
            if (other.tag == "Player")
                other.GetComponent<PlayerHealth>().health -= damage;
            //If the bullet hits an enemy, decrease their health by the bullet's damage
            if (other.tag == "Ship")
                other.GetComponent<ShipHealth>().health -= damage;
        }
        else
        {
            if (other.tag == "Enemy")
                other.GetComponent<EnemyHealth>().health -= damage;
        }


        //Destroy bullet
        Destroy(gameObject);
    }
}
