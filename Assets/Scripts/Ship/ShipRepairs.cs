using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //private bool SpawningEnemy;

    //Enemy variables
    [Tooltip("What enemy prefabs to spawn")]
    public GameObject enemyPrefab;
    [Tooltip("Where to spawn the enemies")]
    public GameObject[] enemySpawnPoints;

    //Ship variables
    public Image ProgressFill;
    public Slider Progress;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
    }

    private void Update() {
        //Set the progress bar's timer to the timer divided by the repairTime
        Progress.value = timer / currentRepairTimeTotal;
        
    }

    public void LateUpdate() {
        Progress.gameObject.SetActive(inWave);

        //If the player is in a wave, start increasing the timer.
        if (inWave) {
            timer += Time.deltaTime;

            if (waveStarted == false) {
                StartCoroutine(SpawnEnemy());
                GetComponent<ShipHealth>().health = GetComponent<ShipHealth>().maxHealth;
                waveStarted = true;
            }

            //If the wave timer reaches the currentRepairTimeTotal, end the wave and reset the timer.
            if (timer >= currentRepairTimeTotal * 60) { 
                inWave = false;
                timer = 0;

                if (totalRepairTime <= 0 && !gameManager.alertActive)
                {
                    gameManager.GetComponent<AlertBox>().AlertPopup("Ship successfully repaired! \n Press \"e\" on the ship to take off!");
                }
            }
        }
        else if (waveStarted) {
            StopCoroutine(SpawnEnemy());
            waveStarted = false;
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
            if (GameManager.Instance.EnemiesLeft < Mathf.Round(timer / (currentRepairTimeTotal * 60) * enemyMult) + 1) {
                Instantiate(enemyPrefab, enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length - 1)].transform.position, enemySpawnPoints[0].transform.rotation);
            }
            yield return new WaitForSeconds(2);
        }
    }
}
