using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBase : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;//the nav mesh agent
    [SerializeField] AIState startState;//the state to start on
    [SerializeField] public AIState currentState;//the state object currently in
    [SerializeField] private bool haveNextState;
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rd;

    public void Start()
    {
        currentState = startState;
        currentState.Enter(agent); //the first state for object
        rd = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

            //test if any of the next states in the current state is available and if current state is exitable, switch to the first state that is enterable
            foreach (AIState state in currentState.states)
        {
            //Debug.Log(currentState.CanExit());
            if (currentState.CanExit() && state.CanEnter())
            {
                currentState = state;
                currentState.Enter(agent);
                haveNextState = true;
                return;
            }
            else
            {
                haveNextState = false;
            }
        }
        if (!haveNextState && currentState.CanExit())
        {
            currentState.Enter(agent);
        }


        if (agent.gameObject.GetComponent<Player>())
        {
            //Debug.Log(rd.velocity + "  , " + (rd.velocity.x != 0 && rd.velocity.z != 0));
            if (rd.velocity.x != 0 && rd.velocity.z != 0)
            {

                anim.SetBool("Running", true);
            }
            else
            {

                anim.SetBool("Running", false);
            }

        }
    }
}
