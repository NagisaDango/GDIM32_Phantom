using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerChaseWolf : AIState
{
    [SerializeField] private Player thisPlayer;
    [SerializeField] private bool canEnter; //the status of the state
    [SerializeField] private bool canExit;//status of state
    [SerializeField] private bool caughtWolf;
    [SerializeField] float wanderRadius; //The range for object to wander
    Vector3 wanderTarget; //destination of wander

    public override void Enter(NavMeshAgent a)
    {
        base.Enter(a);//set the NavMeshAgent
        thisPlayer = GetComponent<Player>();
        Chase();
    }

    // Update is called once per frame
    void Update()
    {
        Chase();
    }

    private void Chase()
    {
        if(thisPlayer.followingAnimals != null && thisPlayer.HaveAllFood())
        {
            canExit = true;
            canEnter = false;
        }
        else if(thisPlayer.followingAnimals == null && !caughtWolf)
        {
            canEnter = true;
            canExit = false;
            if (agent != null)
            {
                agent.ResetPath();
                agent.SetDestination(thisPlayer.wolf.transform.position);
            }
        }
        else
        {
            if (Vector3.Distance(agent.pathEndPosition, this.transform.position) <= 3)
            {
                wanderTarget = this.transform.position + new Vector3(Random.Range(-wanderRadius, wanderRadius), 0, Random.Range(-wanderRadius, wanderRadius));
                agent.SetDestination(wanderTarget);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Wolf"))
        {
            caughtWolf = true;
            Invoke("ChaseAgain", 15);
        }
    }

    private void ChaseAgain()
    {
        caughtWolf = false;
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
    }
}
