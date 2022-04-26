using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Manager : MonoBehaviour {
    [SerializeField] float tickTime = 1f;
    [SerializeField] NavMeshAgent[] agents;
    Transform player;
    float tickTimer;

    private void Start() {
        tickTimer = tickTime;
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update() {
        tickTimer -= Time.deltaTime;

        if(tickTimer <= 0) {
            tickTimer += tickTime;

            foreach (NavMeshAgent agent in agents) {
                agent.destination = player.position;
            }
        }
    }

    public void StartAI() {
        foreach (NavMeshAgent agent in agents) {
            agent.gameObject.SetActive(true);
        }
    }
}
