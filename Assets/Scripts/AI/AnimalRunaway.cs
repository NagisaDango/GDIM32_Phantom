using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalRunaway : AIState
{
    [SerializeField] private GameObject wolf;//the wolf chasing
    [SerializeField] private bool wolfInRange;//if wolf is in range
    [SerializeField] private bool canEnter;//status of state
    [SerializeField] private bool canExit;

    public override void Enter(NavMeshAgent a)
    {
        base.Enter(a);//set the agent
        Runaway();
    }

    private void Update()
    {
        Runaway();
    }

    private void Runaway()
    {
        //if wolf is not in range, this state not enterable
        if (!wolfInRange)
        {
            canEnter = false;
        }
        else
        {
            //if wolf in range, run from the wolf to the opposite side of wolf
            if (agent != null)
            {
                agent.ResetPath();
                agent.SetDestination(this.transform.position + (this.transform.position - wolf.transform.position) * 2);
            }
        }
    }

    public void AllowExit()
    {
        canExit = true;
    }

    private void OnTriggerStay(Collider other)
    {
        //When wolf is in range make this state canEnter
        if (other.gameObject.CompareTag("Wolf"))
        {
            wolfInRange = true;
            canEnter = true;
            wolf = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //when wolf out of range keep run away until two second
        if (other.gameObject.CompareTag("Wolf"))
        {
            wolfInRange = false;
            canEnter = false;
            Invoke("AllowExit", 2);
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
        if (agent != null)
            agent.isStopped = true;
    }
}
