using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowState1 : BaseState
{

    public GameObject state1Prefab;
    private GameObject currentModel;
    private GrowStateController controller;

    public GameObject existPrefab;
    public string name = "0";
    //private Material material;


    public override void Enter()
    {
        
        currentModel = Instantiate(state1Prefab, transform);
        controller = GetComponent<GrowStateController>();
        Renderer renderer = currentModel.GetComponent<Renderer>();
        renderer.material.color = controller.randomColor;

    }

    public override void Exit()
    {
        Destroy(currentModel);
        Destroy(Instantiate(existPrefab, transform), 1.0f);
        
        Debug.Log("Exit grow state1");
    }

    public override void UpdateState()
    {
        
        //Debug.Log("Update grow state1");
    }
}
