using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowState3 : BaseState
{
    public GameObject state3Prefab;
    private GameObject currentModel;
    private GrowStateController controller;
    public int growthIndex = 3;

    public override void Enter()
    {

        currentModel = Instantiate(state3Prefab, transform);
        controller = GetComponent<GrowStateController>();
        Renderer renderer = currentModel.GetComponent<Renderer>();
        renderer.material.color = controller.randomColor;

        TestRootBehavior rootBehavior = GetComponentInParent<TestRootBehavior>();
        MainGame mainGame = FindObjectOfType<MainGame>();
        mainGame?.RootFinished(rootBehavior.Owner.SpawningPeer.Identifier.ToString(), rootBehavior.gameObject);
    }

    public override void Exit()
    {

    }

    public override int getCurrentStateIndex () {
        return growthIndex;
    }

    public override void UpdateState()
    {
        //Debug.Log("Update grow state3");
    }
}
