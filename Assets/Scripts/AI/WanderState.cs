using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderState : AIState
{
    [SerializeField] float wanderRadius; //The range for object to wander
    Vector3 wanderTarget; //destination of wander


    public override void Enter(NavMeshAgent a)
    {
        base.Enter(a);
        Wander();
        agent.isStopped = false;
    }

    //set the destination to a random position within range
    public void Wander()
    {
        wanderTarget = this.transform.position + new Vector3(Random.Range(-wanderRadius, wanderRadius), 0, Random.Range(-wanderRadius, wanderRadius));
        agent.SetDestination(wanderTarget);
    }

    private void Update()
    {
        //if the wander not complete run Wander
        if (agent != null && agent.pathStatus != NavMeshPathStatus.PathComplete)
        {
            Wander();
        }
    }


    public override bool CanExit()
    {
        //if the path set is complete exitable
        if (agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            return true;
        }
        return false;
    }

    //Stop the agent when called
    public override void Exit()
    {
        agent.isStopped = true;
    }

}
