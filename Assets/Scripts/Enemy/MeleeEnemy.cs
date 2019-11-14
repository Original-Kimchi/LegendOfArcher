using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : Enemy
{
    private NavMeshAgent agent;
    private void Start()
    {
        DefaultSetting();
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        Loop();
        if((player.position - transform.position).sqrMagnitude <= 25)
        {
            
        }
        agent.SetDestination(player.position);
    }

    private void 
}