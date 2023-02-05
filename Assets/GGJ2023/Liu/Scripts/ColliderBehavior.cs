using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Perform some action, and add some check logic here such as destory the object
        if(other.name == "player")
        {
            DestoryParent();
        }
        //Debug.Log(other + "Collider is inside the trigger of another collider");
    }

    public void DestoryParent()
    {
        Destroy(transform.parent.gameObject);
    }
}
