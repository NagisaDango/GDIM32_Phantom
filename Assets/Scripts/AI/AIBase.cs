using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBase : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;//the nav mesh agent
    [SerializeField] AIState startState;//the state to start on
    [SerializeField] public AIState currentState;//the state object currently in

    public void Awake()
    {
        currentState = startState;
        currentState.Enter(agent); //the first state for object
    }

    private void Update()
    {
        //test if any of the next states in the current state is available and if current state is exitable, switch to the first state that is enterable
        foreach (AIState state in currentState.states)
        {
            if (currentState.CanExit() && state.CanEnter())
            {
                currentState = state;
                currentState.Enter(agent);
                return;
            }
        }
    }
}
