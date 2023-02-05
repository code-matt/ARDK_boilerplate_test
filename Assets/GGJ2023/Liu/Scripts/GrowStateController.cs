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
    private const int upgrade1 = 5;
    private const int upgrade2 = 10;
    public Color randomColor;

    private void Start()
    {
        time = 0.0f;
        randomColor = Random.ColorHSV();
        stateMachine.SwitchState(state1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            time = 0;
            stateMachine.SwitchState(state1);
        }


        if (time < upgrade2)
        {
            time += Time.deltaTime;
            if (time > upgrade1 && time < upgrade2 && stateMachine.getState()!=state2)
            {
                stateMachine.SwitchState(state2);
            }
            //else if (time > upgrade2)
            //{
            //    stateMachine.SwitchState(state3);
            //}
        }

    }

}
