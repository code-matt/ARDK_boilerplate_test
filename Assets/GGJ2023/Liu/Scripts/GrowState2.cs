using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowState2 : BaseState
{
    public GameObject state2Prefab;
    private GameObject currentModel;
    private GrowStateController controller;
    public GameObject existPrefab;

    public override void Enter()
    {

        currentModel = Instantiate(state2Prefab, transform);
        controller = GetComponent<GrowStateController>();
        Renderer renderer = currentModel.GetComponent<Renderer>();
        renderer.material.color = controller.randomColor;
        TestRootBehavior rootBehavior = GetComponentInParent<TestRootBehavior>();
        MainGame mainGame = FindObjectOfType<MainGame>();
        mainGame.RootFinished(rootBehavior.Owner.SpawningPeer.Identifier.ToString(), rootBehavior.gameObject);
        Debug.Log("Enter grow state2");
    }

    public override void Exit()
    {
        Destroy(currentModel);
        Destroy(Instantiate(existPrefab, transform), 1.0f);
        Debug.Log("Exit grow state2");
    }

    public override void UpdateState()
    {
        //Debug.Log("Update grow state2");
    }
}
