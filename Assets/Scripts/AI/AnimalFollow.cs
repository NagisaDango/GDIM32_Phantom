using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalFollow : AIState
{
    [SerializeField] private GameObject player; //the player object to follow
    private Player playerReference;

    [SerializeField] private bool canEnter; //the status of the state
    [SerializeField] private bool canExit;//status of state
    [SerializeField] private bool wolfInRange;//a bool to check if wolf is in range
    [SerializeField] private AnimalInstance thisAnimal;
    public bool infarm;



    public override void Enter(NavMeshAgent a)
    {
        base.Enter(a);//set the NavMeshAgent
        Follow();
    }

    private void Update()
    {
        Follow();
    }

    //Follow the player
    private void Follow()
    {
        if (player == null)
        {
            canExit = true;
            canEnter = false;
        }
        //if player not follow by other and is in and wolf is not in range make the state enterable and follow the player
        else if (player != null && !wolfInRange && !infarm && (playerReference.followingAnimals == thisAnimal || playerReference.followingAnimals == null) && thisAnimal.canFollow)
        {
            canEnter = true;
            canExit = false;
            if (agent != null)
            {
                thisAnimal.isFollowing = true;
                agent.ResetPath();
                agent.SetDestination(player.transform.position);
            }
        }
        else
        {
            thisAnimal.isFollowing = false;
            canEnter=false;
            canExit = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if wolf is in the trigger range, set target to null, set playerInRange to true, and make this state exitable and not enterable
        if (other.gameObject.CompareTag("Wolf"))
        {
            player = null;
            wolfInRange = true;
            canEnter = false;
            canExit = true;
        }
        //if there is player in range and wolf is not in range, set the state enterable and not exitable, set the target to the object in range
        else if (other.gameObject.CompareTag("Player") && !wolfInRange && !infarm)
        {
            canExit = false;
            if(player == null)
            {
                player = other.gameObject;
                playerReference = other.gameObject.GetComponent<Player>();
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        //when the wolf leaves the range, set wolfInRange to false and make the state enterable
        if (other.gameObject.CompareTag("Wolf"))
        {
            Invoke("AllowEnter", 2);//after chase by wolf the animal needs 2 second to enter this state
        }
        if (other.gameObject.CompareTag("Player"))
        {
            canExit = true;
            player = null;
        }
    }
    //when disable not enterable and exitable infarm
    private void OnDisable()
    {
        infarm = true;
        canEnter = false;
        canExit = true;
    }

    public void AllowEnter()
    {
        canEnter = true;
        wolfInRange = false;
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
