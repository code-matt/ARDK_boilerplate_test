using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Niantic.ARDK.Utilities.Input.Legacy;

public class TouchManager : MonoBehaviour
{
        [SerializeField]
    private MainGame mainGamePrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void TouchBegan(Touch touch)
    {
        mainGamePrefab.InstantiateObjects();
    }

    // Update is called once per frame
    void Update()
    {
        //If there is no touch, we're not going to do anything
        if(PlatformAgnosticInput.touchCount <= 0)
        {
            return;
        }

        //If we detect a new touch, call our 'TouchBegan' function
        var touch = PlatformAgnosticInput.GetTouch(0);
        if(touch.phase == TouchPhase.Began)
        {
            TouchBegan(touch);
        }
    }
}
