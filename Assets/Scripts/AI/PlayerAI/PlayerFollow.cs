using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerFollow : AIState
{
    [SerializeField] private Player thisPlayer;
    [SerializeField] private AnimalInstance followingAnimal; //the animal to follow
    [SerializeField] private GameObject animalObj;
    [SerializeField] private bool canEnter; //the status of the state
    [SerializeField] private bool canExit;//status of state

    public override void Enter(NavMeshAgent a)
    {
        base.Enter(a);//set the NavMeshAgent
        Follow();
    }

    private void Follow()
    {
        if (thisPlayer.followingAnimals != null || thisPlayer.InFarm)
        {
            animalObj = null;
            followingAnimal = null;
            canExit = true; 
            canEnter = false;
        }
        else if (followingAnimal != null && thisPlayer.HaveAllFood())
        {
            canEnter = true;
            canExit = false;
            if (agent != null)
            {
                agent.ResetPath();
                agent.SetDestination(animalObj.transform.position);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Animal") && followingAnimal == null)
        {
            animalObj = other.gameObject;
            followingAnimal = animalObj.GetComponent<AnimalInstance>();
            canEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject==animalObj)
        {
            animalObj = null;
            followingAnimal = null;
            canExit = true;
            canEnter= false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
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
