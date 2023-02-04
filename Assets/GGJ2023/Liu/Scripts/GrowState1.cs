using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowState1 : BaseState
{

    public GameObject state1Prefab;

    private GameObject currentModel;
    
    public override void Enter()
    {
       
        currentModel = Instantiate(state1Prefab, transform);
        Debug.Log("Enter grow state1");
    }

    public override void Exit()
    {
        Destroy(currentModel);
        Debug.Log("Exit grow state1");
    }

    public override void UpdateState()
    {
        //Debug.Log("Update grow state1");
    }
}
