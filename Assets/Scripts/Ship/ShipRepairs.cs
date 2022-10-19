using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShipRepairs : MonoBehaviour
{
    //Manager variables
    private GameManager gameManager;

    //Timer variables
    private float timer;
    public float totalRepairTime;
    public float currentRepairTimeTotal;

    //Manager variables
    public float enemyMult;
    public bool inWave;
    private bool waveStarted = false;
    private int partsRepaired;
    private ShipHealth health;

    //private bool SpawningEnemy;

    //Enemy variables
    [Tooltip("What enemy prefabs to spawn")]
    public GameObject enemyPrefab;
    [Tooltip("Where to spawn the enemies")]
    public GameObject[] enemySpawnPoints;

    //Ship variables
    public Text timeText;
    public Slider Progress;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();

        health = transform.GetChild(0).GetComponent<ShipHealth>();
    }

    private void Update() {
        //Set the progress bar's timer to the timer divided by the repairTime
        TimeSpan t = TimeSpan.FromSeconds(currentRepairTimeTotal * 60 - timer);
        timeText.text = string.Format("{0:D2}:{1:D2}",
                        t.Minutes,
                        t.Seconds);

        Progress.value = timer;
    }

    public void LateUpdate() {
        Progress.gameObject.SetActive(inWave);

        //If the player is in a wave, start increasing the timer.
        if (inWave) {
            timer += Time.deltaTime;

            if (waveStarted == false) {
                StartCoroutine(SpawnEnemy());
                health.health = health.maxHealth;
                health.healthVisible = true;
                waveStarted = true;
            }

            //If the wave timer reaches the currentRepairTimeTotal, end the wave and reset the timer.
            if (timer >= currentRepairTimeTotal * 60) { 
                inWave = false;
                timer = 0;

                if (totalRepairTime <= 0 && !gameManager.alertActive)
                {
                    gameManager.GetComponent<AlertBox>().AlertPopup("Ship successfully repaired! \n Press \"e\" on the ship to take off!");
                    gameManager.shipRepaired = true;
                }
            }
        }
        else if (waveStarted) {
            StopCoroutine(SpawnEnemy());
            waveStarted = false;
            health.healthVisible = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Player")
        {
            if (other.GetComponent<CollectionSystem>().partsHolding >= 1)
            {
                inWave = true;

                totalRepairTime -= other.GetComponent<CollectionSystem>().partsHolding;
                currentRepairTimeTotal = other.GetComponent<CollectionSystem>().partsHolding;

                Progress.maxValue = currentRepairTimeTotal * 60;

                other.GetComponent<CollectionSystem>().partsHolding = 0;
            }
        }
    }

    IEnumerator SpawnEnemy() {
        while (true) {
            if (GameManager.Instance.ShipEnemiesLeft < Mathf.Round(timer / (currentRepairTimeTotal * 60) * enemyMult) + 1) {
                Instantiate(enemyPrefab, enemySpawnPoints[UnityEngine.Random.Range(0, enemySpawnPoints.Length - 1)].transform.position, enemySpawnPoints[0].transform.rotation);
            }
            yield return new WaitForSeconds(2);
        }
    }
}
