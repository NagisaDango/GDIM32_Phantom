using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfHunt: AIState
{
    [SerializeField] private GameObject target; //the target to chase after
    [SerializeField] private bool canEnter; //the status of the state
    [SerializeField] private bool canExit;//status of state
    [SerializeField] private bool playerInRange;//a bool to check if player is in range


    public override void Enter(NavMeshAgent a)
    {
        base.Enter(a);//set the NavMeshAgent
        //if there is target and player is not in range set the status to enterable, and once entered, go to the target
        if (target != null && !playerInRange)
        {
            canEnter = true;
            if (agent != null)
            {
                agent.SetDestination(target.transform.position);
            }
        }
        //if there is no target, can't enter this state and exit this state
        else
        {
            canExit = true;
            canEnter = false;
        }
    }

    private void Update()
    {
        //if there is no target, set the state exitable
        if (target == null)
        {
            canExit = true;
        }
        //if the state is enterable and player is not in range, go to the target
        else if (agent != null && canEnter == true && !playerInRange)
        {
            agent.ResetPath();
            agent.SetDestination(target.transform.position);
        }
        else
        {
            canExit = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if player is in the trigger range, set target to null, set playerInRange to true, and make this state exitable and not enterable
        if (other.gameObject.CompareTag("Player"))
        {
            target = null;
            playerInRange = true;
            canEnter = false;
            canExit = true;
        }
        //if there is animal in range and player is not in range, set the state enterable and not exitable, set the target to the object in range
        else if (other.gameObject.CompareTag("Animal") && !playerInRange)
        {
            canExit = false;
            canEnter = true;
            target = other.gameObject;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        //when the player leaves the range, set playerInRange to false and make the state enterable
        if (other.gameObject.CompareTag("Player"))
        {
            Invoke("AllowEnter", 2);
            //canEnter = true;
        }
    }

    public void AllowEnter()
    {
        canEnter = true;
        playerInRange = false;
    }

    public override bool CanEnter()
    {
        return canEnter;
    }

    public override void Exit()
    {
        if (agent != null)
            agent.isStopped = true;
    }

    public override bool CanExit()
    {
        return canExit;
        /*
        if(target == null)
            return true;
        return false;*/
    }
}