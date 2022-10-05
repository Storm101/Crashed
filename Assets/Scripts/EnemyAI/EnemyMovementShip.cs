using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyShooting))]
public class EnemyMovementShip : MonoBehaviour
{
    //Variables
    private NavMeshAgent agent;
    public GameObject player;
    public GameObject realPlayer;
    private float stopDistance;
    private bool shoot;
    private EnemyShooting shooting;

    [Tooltip("How softly the enemies turn")]
    public float turnDampening = 5;

    [Tooltip("How close to get to the player on average")]
    public float averageStopDistance = 4;

    [Tooltip("How much the stop distance varies")]
    public float stopDistanceVariation = 2;

    [Tooltip("The arms and gun of the enemy")]
    public GameObject arms;

    [Tooltip("The distance the enemy can detect you from any direction")]
    public float detectionRange = 3f;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Ship");
        
        if (player == null) { Debug.Log("Player not found"); }
        if (agent == null) { Debug.Log("NavMeshAgent not found"); }
        
        stopDistance = Random.Range(averageStopDistance - stopDistanceVariation, averageStopDistance + stopDistanceVariation);

        shooting = GetComponent<EnemyShooting>();
       
        realPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update() {
        float distance = Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), new Vector2(transform.position.x, transform.position.z));

        //Look at player
        var lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        arms.transform.LookAt(player.transform.position + Vector3.up);
        arms.transform.rotation = Quaternion.Euler(arms.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, arms.transform.rotation.eulerAngles.z);
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

        if (Vector2.Distance(new Vector2(realPlayer.transform.position.x, realPlayer.transform.position.z), new Vector2(transform.position.x, transform.position.z)) < detectionRange) {
            player = realPlayer;
        }
    }

    private void FixedUpdate() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit)) {
            if (hit.collider.gameObject == player) {
                shoot = true;
            }
        }
    }
}
