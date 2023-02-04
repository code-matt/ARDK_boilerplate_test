using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStateMachine : MonoBehaviour
{

    private BaseState currentState = null;
    // Start is called before the first frame update

    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
        }
        
    }

    public void SwitchState(BaseState newState)
    {
        if (currentState!=null)
        {
            currentState.Exit();
        }
        
        currentState = newState;
        currentState.Enter();
    }
    
}
