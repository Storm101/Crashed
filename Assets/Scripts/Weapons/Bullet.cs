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

    [SerializeField]
    private AudioClip[] wallHitNoises;
    [SerializeField]
    private AudioClip[] playerHitNoises;
    [SerializeField]
    private AudioClip[] enemyHitNoises;
    [SerializeField]
    private AudioClip[] shipHitNoises;
    [SerializeField]
    private AudioSource audioSource;

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

        GameObject audio = new GameObject("SFX", typeof(AudioSource));
        audio.GetComponent<AudioSource>().outputAudioMixerGroup = audioSource.outputAudioMixerGroup;

        if (isEnemyBullet)
        {
            //If the bullet hits the player, decrease their health by the bullet's damage
            if (other.gameObject.tag == "Player") {
                other.gameObject.GetComponent<PlayerHealth>().health -= damage;

                audio.GetComponent<AudioSource>().clip = wallHitNoises[Random.Range(0, playerHitNoises.Length)];
            }

            //If the bullet hits an enemy, decrease their health by the bullet's damage
            else if (other.gameObject.tag == "Ship") {
                other.gameObject.GetComponent<ShipHealth>().health -= damage;
                GameObject hitParticle = Instantiate(HitShip);
                hitParticle.transform.position = transform.position;
                hitParticle.transform.rotation = rotation;

                audio.GetComponent<AudioSource>().clip = wallHitNoises[Random.Range(0, shipHitNoises.Length)];
            }

            else {
                GameObject hitParticle = Instantiate(HitWall);
                hitParticle.transform.position = transform.position;
                hitParticle.transform.rotation = rotation;

                audio.GetComponent<AudioSource>().clip = wallHitNoises[Random.Range(0, wallHitNoises.Length)];
            }
        }
        else
        {
            if (other.gameObject.tag == "Enemy") {
                other.gameObject.GetComponent<EnemyHealth>().health -= damage;
                GameObject hitParticle = Instantiate(HitEnemy);
                hitParticle.transform.position = transform.position;
                hitParticle.transform.rotation = rotation;

                audio.GetComponent<AudioSource>().clip = wallHitNoises[Random.Range(0, enemyHitNoises.Length)];
            }

            else if (other.gameObject.tag == "Ship") {
                GameObject hitParticle = Instantiate(HitShip);
                hitParticle.transform.position = transform.position;
                hitParticle.transform.rotation = rotation;

                audio.GetComponent<AudioSource>().clip = wallHitNoises[Random.Range(0, shipHitNoises.Length)];
            }

            else {
                GameObject hitParticle = Instantiate(HitWall);
                hitParticle.transform.position = transform.position;
                hitParticle.transform.rotation = rotation;

                audio.GetComponent<AudioSource>().clip = wallHitNoises[Random.Range(0, wallHitNoises.Length)];
            }
        }

        audio.GetComponent<AudioSource>().Play();

        //Destroy bullet
        Debug.Log(audio.GetComponent<AudioSource>().clip.length);
        Destroy(audio, audio.GetComponent<AudioSource>().clip.length);
        Destroy(gameObject);
    }
}
