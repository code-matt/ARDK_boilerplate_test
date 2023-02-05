using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MattsColliderBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        TestRootBehavior rootBehavior = GetComponent<TestRootBehavior>();
        MainGame mainGame = FindObjectOfType<MainGame>();

        GrowStateController growController = GetComponentInChildren<GrowStateController>();

        if(rootBehavior.ownerPlayer.ARDK_id == mainGame._self.Identifier.ToString())
            return;

        if(growController.stateMachine.getState().getCurrentStateIndex() > 2)
            return;

        // if(rootBehavior.ownerPlayer.ARDK_id == mainGame._self.Identifier.ToString());
        // {
            Debug.Log("Destroy a root with owner: " + rootBehavior.ownerPlayer.ARDK_id + " and my local self id is: " + mainGame._self.Identifier.ToString());
            DestoryParent();
        // }
    }

    public void DestoryParent()
    {   
        Destroy(gameObject);
    }
}
