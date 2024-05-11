using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIState : MonoBehaviour
{
    protected NavMeshAgent agent; //nav mesh agent
    public List<AIState> states; //the available states to run 

    public void Awake()
    {
        if (states.Count == 0)
            Debug.Log("no state");


    }

    public virtual bool CanEnter()
    {
        return true;
    }

    public virtual bool CanExit()
    {
        return true;
    }

    public virtual void Enter(NavMeshAgent a)
    {
        agent = a;
        
    }

    public virtual void Exit()
    {
        
    }
}