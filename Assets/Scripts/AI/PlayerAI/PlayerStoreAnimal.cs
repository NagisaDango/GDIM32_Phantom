using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerStoreAnimal : AIState
{
    [SerializeField] Player thisPlayer;
    [SerializeField] private bool canEnter; //the status of the state
    [SerializeField] private bool canExit;//status of state

    public override void Enter(NavMeshAgent a)
    {
        base.Enter(a);
        StoreBack();
    }

    // Update is called once per frame
    void Update()
    {
        //if the player is in farm and there is animal following the player, enterable
        if (thisPlayer.InFarm && thisPlayer.followingAnimals != null)
        {
            canEnter = true;
            canExit = false;
        }
        else
        {
            canEnter = false;
            canExit = true;
        }
    }

    //store the animal following the player to farm
    private void StoreBack()
    {
        thisPlayer.StoreToFarm(thisPlayer.followingAnimals);
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
