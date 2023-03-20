using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerSeek : AIState
{
    [SerializeField] private Player thisPlayer;
    [SerializeField] private AnimalFollow seekingAnimal; //the animal to follow
    [SerializeField] private GameObject animalObj;
    [SerializeField] private bool canEnter; //the status of the state
    [SerializeField] private bool canExit;//status of state
    
    void Update()
    {
        Seek();
    }

    public override void Enter(NavMeshAgent a)
    {
        base.Enter(a);//set the NavMeshAgent
        Seek();
    }

    private void Seek()
    {
        if(thisPlayer.followingAnimals==null && thisPlayer.HaveAllFood())
        {
            seekingAnimal = FindObjectOfType<AnimalFollow>();
            animalObj = seekingAnimal.gameObject;
            canEnter = true;
            canExit = false;
            if (agent != null)
            {
                agent.ResetPath();
                agent.SetDestination(animalObj.transform.position);
            }
        }
        else
        {
            canEnter = false;
            canExit = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
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
