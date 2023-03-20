using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerGoShop : AIState
{
    [SerializeField] Player thisPlayer;
    [SerializeField] Transform shopPosition;
    [SerializeField] private bool canEnter; //the status of the state
    [SerializeField] private bool canExit;//status of state

    // Update is called once per frame
    void Update()
    {
        GoShop(shopPosition);
    }

    private void GoShop(Transform shopPosition)
    {
        if (!thisPlayer.HaveAllFood())
        {
            canEnter = true;
            canExit = false;
            if (agent != null)
            {
                agent.ResetPath();
                agent.SetDestination(shopPosition.position);
            }
        }
        else
        {
            canEnter = false;
            canExit = true;
        }
    }

    public override void Enter(NavMeshAgent a)
    {
        base.Enter(a);
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
