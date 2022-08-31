using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    //Variables
    private NavMeshAgent agent;
    private bool playerDetected = true;
    private GameObject player;

    [Tooltip("How softly the enemies turn")]
    public float turnDampening;

    [Tooltip("How close to get to the player")]
    public float stopDistance;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null) { Debug.Log("Player not found"); }
        if (agent == null) { Debug.Log("NavMeshAgent not found"); }
    }

    private void Update() {
        if (playerDetected) {

            //Look at player
            var lookPos = player.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnDampening);

            float distance = Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), new Vector2(transform.position.x, transform.position.z));

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
        }
    }
}
