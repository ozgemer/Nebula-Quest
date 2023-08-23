using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;

    void Start()
    {
        player = GameObject.Find("Player");
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    public void Follow()
    {
        agent.SetDestination(player.transform.position);
        if (Vector3.Distance(transform.position, player.transform.position) < 2.5f)
            agent.isStopped = true;
        else
            agent.isStopped = false;
    }

    public void Stop()
    {
        agent.isStopped = true;

    }

    public void Continue()
    {
        agent.isStopped = false;
    }
}
