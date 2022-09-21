using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Door : MonoBehaviour
{
    [Tooltip("What enemy prefabs to spawn")]
    public GameObject enemyPrefab;

    [Tooltip("Where to spawn the enemies")]
    public GameObject[] enemySpawnPoints;

    [Tooltip("How many waves of how many enemies")]
    public int[] waves;

    private bool inWave;
    private MeshRenderer m_Renderer;
    private BoxCollider m_Collider;
    private BoxCollider m_Trigger;

    public bool locked;
    public GameObject lockIndicator;
    private MeshRenderer lockIndicatorColour;
    private bool up;

    private int CurrentWave = -1;
    private Animator animator;

    public GameObject Player;

    private void Start() {
        BoxCollider[] boxColliders = GetComponents<BoxCollider>();
        if (boxColliders.Length != 2) {
            Debug.LogError("Wrong amount of colliders");
        }
        if (boxColliders[0].isTrigger) {
            m_Trigger = boxColliders[0];
            m_Collider = boxColliders[1];
        }
        else {
            m_Trigger = boxColliders[1];
            m_Collider = boxColliders[0];
        }
        m_Renderer = GetComponent<MeshRenderer>();
        //m_Renderer.enabled = false;
        m_Collider.enabled = false;
        m_Trigger.enabled = true;
        inWave = false;
        animator = GetComponent<Animator>();
        lockIndicatorColour = lockIndicator.GetComponent<MeshRenderer>();
        lockIndicatorColour.material.color = Color.green;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            inWave = true;
            //m_Renderer.enabled = true;
            m_Trigger.enabled = false;
            //m_Collider.enabled = true;
            //animator.Play("Base Layer.Door", 0, -1);
            locked = true;
        }
    }

    private void LateUpdate() {
        if (inWave) {
            if (GameManager.Instance.EnemiesLeft == 0) {
                if (CurrentWave == waves.Length-1) {
                    //m_Renderer.enabled = false;
                    //m_Collider.enabled = false;
                    locked = false;
                    inWave = false;
                }
                else {
                    CurrentWave++;
                    for (int i = 0; i < waves[CurrentWave]; i++) {
                        Instantiate(enemyPrefab, enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length - 1)].transform.position, enemySpawnPoints[0].transform.rotation);
                    }
                }
            }
        }
        if (locked) {
            m_Collider.enabled = true;
            if (up == true) {
                animator.Play("Base Layer.Door", 0, -1);
                up = false;
            }
            lockIndicatorColour.material.color = Color.red;
        }
        else {
            m_Collider.enabled = false;
            float i = Vector3.Distance(transform.position, Player.transform.position);
            if (!up && i < 10) {
                //animator.Play("Base Layer.DoorReverse", 0, -1);
                up = true;
                animator.SetBool("OpenDoor", true);
            }
            else if (up && i > 10.1f) {
                up = false;
                //animator.Play("Base Layer.Door", 0, -1);
                animator.SetBool("OpenDoor", false);
            }
            lockIndicatorColour.material.color = Color.green;
        }
    }
}