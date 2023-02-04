using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowStateController : MonoBehaviour
{
    public BaseStateMachine stateMachine;
    public GrowState1 state1;
    public GrowState2 state2;
    public GrowState3 state3;
    private float time;
    private const int upgrade1 = 10;
    private const int upgrade2 = 20;
    private void Start()
    {
        stateMachine.SwitchState(state1);
        time = 0.0f;
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
            time = 0;
            stateMachine.SwitchState(state1);
        }


        if(time < upgrade2)
        {
            time += Time.deltaTime;
            if (time > upgrade1 && time < upgrade2)
            {
                stateMachine.SwitchState(state2);
            }
            else if (time > upgrade2)
            {
                stateMachine.SwitchState(state3);
            }
        }

    }


}
