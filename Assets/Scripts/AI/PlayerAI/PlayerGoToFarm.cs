using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerGoFarm : AIState
{
    [SerializeField] private Player thisPlayer;
    //[SerializeField] private Transform farmTrans;
    [SerializeField] private bool canEnter; //the status of the state
    [SerializeField] private bool canExit;//status of state

    public override void Enter(NavMeshAgent a)
    {
        base.Enter(a);
    }

    private void GoToFarm(Transform farmTransform)
    {
        if (thisPlayer.InFarm)
        {
            canEnter = false;
            canExit = true;
        }
        else if (thisPlayer.followingAnimals != null)
        {
            canEnter = true;
            canExit = false;
            if (agent != null)
            {
                agent.ResetPath();
                agent.SetDestination(farmTransform.position);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        GoToFarm(thisPlayer.farmPos);
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
