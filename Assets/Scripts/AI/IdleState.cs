using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : AIState
{
    [SerializeField] float minTime; //the minimum time to run
    [SerializeField] float maxTime; //the max time to run
    [SerializeField] float minIdleTime;//the min time Idle state will last
    [SerializeField] float maxIdleTime;//the max time Idle state will last
    [SerializeField] private bool canEnter;//if this state can be enter
    [SerializeField] private bool canExit;//if this state can be exit

    private void Start()
    {
        canEnter = true;
    }

    public override void Enter(NavMeshAgent a)
    {
        base.Enter(a);//set the nav mesh
        a.isStopped = true;//stop the agent
        canEnter = false;
        canExit = false;
        Invoke("AllowExit", Random.Range(minIdleTime, maxIdleTime));//after the random amount of idle time, execute AllowExit
    }

    //if the state can be enter or not
    public override bool CanEnter()
    {
        return canEnter;
    }
    //if the state can be exist or not
    public override bool CanExit()
    {
        return canExit;
    }
    //allow this state to be enter
    public void AllowEnter()
    {
        canEnter = true;
    }
    //allow this state to be exit
    public void AllowExit()
    {
        canExit = true;
        Invoke("AllowEnter", Random.Range(minTime, maxTime));//make this state avalible again in a random amount of time
    }

    public override void Exit()
    {
        if (agent != null)
            agent.isStopped = true;
    }
}