using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfRunaway : AIState
{
    [SerializeField] private GameObject player;
    [SerializeField] private bool playerInRange;
    [SerializeField] private bool canEnter;
    [SerializeField] private bool canExit;


    public override void Enter(NavMeshAgent a)
    {
        base.Enter(a);
        if (playerInRange)
        {
            if (agent != null)
            {
                agent.ResetPath();
                agent.SetDestination(this.transform.position + (this.transform.position - player.transform.position) * 2);
                canExit = false;
            }
        }
        else
        {
            canExit = true;
            canEnter = false;
        }
    }

    private void Update()
    {
        if (!playerInRange)
        {
            canEnter = false;
            Invoke("AllowExit", 2);
            //canExit = true;
        }
        else //if (playerInRange&&agent != null && canExit!=true)
        {
            if (agent != null)
            {
                agent.ResetPath();
                agent.SetDestination(this.transform.position + (this.transform.position - player.transform.position) * 2);
            }
        }
    }

    public void AllowExit()
    {
        canExit = true;
    }

    private void OnTriggerStay(Collider other)
    {
        //When player is in range make this state canEnter
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            canEnter = true;
            //canExit = false; 
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            canEnter = false;
            //canExit = true;
        }
    }


    public override bool CanEnter()
    {
        return canEnter;
    }

    public override bool CanExit()
    {
        return canExit;
    }

    public override void Exit()
    {
        Debug.Log(4234);
        if (agent != null)
            agent.isStopped = true;
    }
}

