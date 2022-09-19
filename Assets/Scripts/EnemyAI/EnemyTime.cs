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
    private bool SpawningEnemy;

    [Tooltip("What enemy prefabs to spawn")]
    public GameObject enemyPrefab;

    [Tooltip("Where to spawn the enemies")]
    public GameObject[] enemySpawnPoints;

    public Slider Progress;

    private void Update() {
        Progress.value = timer / totalTime;
    }

    public void LateUpdate() {
        Progress.gameObject.SetActive(inWave);

        if (inWave) {
            timer += Time.deltaTime;

            if (GameManager.Instance.EnemiesLeft < (Mathf.Round((timer / totalTime) * 10)+1)*enemyMult && !SpawningEnemy) {
                StartCoroutine(SpawnEnemy());
            }

            if (timer >= totalTime) { 
                inWave = false;
                timer = 0;
            }
        }
    }

    IEnumerator SpawnEnemy() {
        SpawningEnemy = true;
        yield return new WaitForSeconds(2);
        Instantiate(enemyPrefab, enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length - 1)].transform.position, enemySpawnPoints[0].transform.rotation);
        SpawningEnemy = false;
    }
}
