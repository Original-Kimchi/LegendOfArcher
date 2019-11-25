using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : Enemy
{
    private NavMeshAgent agent;
    
    private void Start()
    {
        // transform.LookAt(player.position);
        DefaultSetting();
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        
        if((player.position - transform.position).sqrMagnitude <= 4)
        {
            Invoke("Attack", 2f);
        }
        if(gameObject.activeSelf)
            agent.SetDestination(player.position);
    }
}