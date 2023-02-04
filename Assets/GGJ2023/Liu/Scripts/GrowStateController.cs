using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowStateController : MonoBehaviour
{
    public BaseStateMachine stateMachine;
    public GrowState1 state1;
    public GrowState2 state2;
    public GrowState3 state3;
    private void Start()
    {
        stateMachine.SwitchState(state1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            stateMachine.SwitchState(state2);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            stateMachine.SwitchState(state3);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            stateMachine.SwitchState(state1);
        }
    }
}
