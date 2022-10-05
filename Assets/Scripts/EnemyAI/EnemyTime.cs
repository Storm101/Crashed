using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTime : MonoBehaviour
{
    private float timer;
    public float totalTime;
    public float enemyMult;
    public bool inWave;
    private bool startwave = false;
    private bool SpawningEnemy;

    [Tooltip("What enemy prefabs to spawn")]
    public GameObject enemyPrefab;

    [Tooltip("Where to spawn the enemies")]
    public GameObject[] enemySpawnPoints;

    public Image ProgressFill;
    public Slider Progress;

    private void Update() {
        Progress.value = timer / totalTime;
        ProgressFill.fillAmount = timer / totalTime;
    }

    public void LateUpdate() {
        Progress.gameObject.SetActive(inWave);

        if (inWave) {
            timer += Time.deltaTime;

            if (startwave == false) {
                StartCoroutine(SpawnEnemy());
                GetComponent<ShipHealth>().health = GetComponent<ShipHealth>().maxHealth;
                startwave = true;
            }

            if (timer >= totalTime) { 
                inWave = false;
                timer = 0;
            }
        }

        else if (startwave) {
            StopCoroutine(SpawnEnemy());
            startwave = false;
        }
    }

    IEnumerator SpawnEnemy() {
        while (true) {
            if (GameManager.Instance.EnemiesLeft < (Mathf.Round(timer / totalTime * enemyMult)) + 1) {
                Instantiate(enemyPrefab, enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length - 1)].transform.position, enemySpawnPoints[0].transform.rotation);
            }
            yield return new WaitForSeconds(2);
        }
    }
}
