using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyShooting))]
public class EnemyMovement : MonoBehaviour
{
    //Variables
    private NavMeshAgent agent;
    public bool playerDetected = true;
    private GameObject player;
    private float stopDistance;
    private bool shoot;
    private EnemyShooting shooting;

    [Tooltip("How softly the enemies turn")]
    public float turnDampening;

    [Tooltip("How many raycasts the enemy does")]
    public int rayResolution;

    [Tooltip("How close to get to the player on average")]
    public float averageStopDistance;

    [Tooltip("How much the stop distance varies")]
    public float stopDistanceVariation;

    [Tooltip("The group the enemy is apart of")]
    public int enemyGroup;

    [Tooltip("How far the enemy can view the player from")]
    public int viewDistance;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");
        
        if (enemyGroup < 0 || enemyGroup > GameManager.Instance.groupDetection.Length) { Debug.Log("Enemy group out of range"); }
        if (player == null) { Debug.Log("Player not found"); }
        if (agent == null) { Debug.Log("NavMeshAgent not found"); }
        
        stopDistance = Random.Range(averageStopDistance - stopDistanceVariation, averageStopDistance + stopDistanceVariation);

        shooting = GetComponent<EnemyShooting>();
    }

    private void Update() {
        StartCoroutine("Detect");
        float distance = Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), new Vector2(transform.position.x, transform.position.z));

        if (playerDetected) {
            GameManager.Instance.groupDetection[enemyGroup] = true;

            //Look at player
            var lookPos = player.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnDampening);

            //if far from player, go towards player
            if (distance > stopDistance) {
                agent.isStopped = false;
                agent.destination = player.transform.position;
            }

            else if (distance < stopDistance - 1) {
                agent.isStopped = false;
                agent.destination = transform.position - (transform.forward * 5);
            }

            else {
                agent.isStopped = true;
            }

            if (shoot) {
                shooting.Shoot = true;
            }
        }

        else if (GameManager.Instance.groupDetection[enemyGroup]) {
            playerDetected = true;
        }

        if (distance < 1.5f) {
            playerDetected = true;
        }
    }

    IEnumerator Detect() {
        RaycastHit hit;

        for (int i = 0; i < rayResolution; i++) {
            Debug.DrawRay(transform.position, transform.forward + (transform.right / (i - rayResolution/2))* 1.5f, color: Color.red, 0.1f);
            if (Physics.Raycast(transform.position, transform.forward + (transform.right/(i-rayResolution/2))*1.5f, out hit)) {
                if (hit.collider.tag == "Player" && hit.distance < viewDistance) {
                    shoot = true;
                    playerDetected = true;
                    yield return null;
                }
            }
        }
        if (Physics.Raycast(transform.position, transform.forward, out hit)) {
            if (hit.collider.tag == "Player") {
                playerDetected = true;
                shoot = true;
                yield return null;
            }
        }
        shoot = false;
        yield return new WaitForSeconds(.1f);
    }
}
