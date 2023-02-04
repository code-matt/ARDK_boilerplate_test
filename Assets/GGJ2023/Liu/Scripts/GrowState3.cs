using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowState3 : BaseState
{
    public GameObject state3Prefab;

    private GameObject currentModel;

    public override void Enter()
    {

        currentModel = Instantiate(state3Prefab, transform);
        Debug.Log("Enter grow state3");
    }

    public override void Exit()
    {
        Destroy(currentModel);
        Debug.Log("Exit grow state3");
    }

    public override void UpdateState()
    {
        //Debug.Log("Update grow state3");
    }
}
