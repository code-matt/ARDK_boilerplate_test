using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Niantic.ARDK.AR.HitTest;
using Niantic.ARDK.Networking.HLAPI.Authority;
using Niantic.ARDK.Networking.HLAPI.Object.Unity;
using Niantic.ARDK.Utilities;
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
        // mainGamePrefab.InstantiateObjects();
        // mainGamePrefab._rootPlantedReplicator.SendMessage(new Vector3(), mainGamePrefab._auth.PeerOfRole(Role.Authority));

        // If the ARSession isn't currently running, its CurrentFrame property will be null
        var currentFrame = mainGamePrefab._arNetworking.ARSession.CurrentFrame;
        if (currentFrame == null)
            return;

        // Hit test from the touch position
        var results =
            mainGamePrefab._arNetworking.ARSession.CurrentFrame.HitTest
            (
                mainGamePrefab._camera.pixelWidth,
                mainGamePrefab._camera.pixelHeight,
                touch.position,
                ARHitTestResultType.All
            );

        if (results.Count == 0)
            return;

        var closestHit = results[0];
        var position = closestHit.WorldTransform.ToPosition();

        if (CheckDistance(2, mainGamePrefab.mainBrain.roots, position)) {
            mainGamePrefab.PlantRoot(position);
        } else {
            Debug.Log("A root is too close...");
        }
        // mainGamePrefab.InstantiateObjects(position);
        // mainGamePrefab._rootPlantedReplicator.SendMessage(position);
    }

    public static bool CheckDistance(float distance, List<GameObject> gameObjects, Vector3 position)
    {
        foreach (GameObject go in gameObjects)
        {
            float currentDistance = Vector3.Distance(go.transform.position, position);
            if (currentDistance < distance)
            {
                return false;
            }
        }
        return true;
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
