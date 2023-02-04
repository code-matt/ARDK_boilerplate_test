using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowState2 : BaseState
{
    public GameObject state2Prefab;

    private GameObject currentModel;

    public override void Enter()
    {

        currentModel = Instantiate(state2Prefab, transform);
        Debug.Log("Enter grow state2");
    }

    public override void Exit()
    {
        Destroy(currentModel);
        Debug.Log("Exit grow state2");
    }

    public override void UpdateState()
    {
        Debug.Log("Update grow state2");
    }
}
