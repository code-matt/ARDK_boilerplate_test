using UnityEngine;
using UnityEngine.UI;

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

        mainGamePrefab.PlantRoot(position);
        // mainGamePrefab.InstantiateObjects(position);
        // mainGamePrefab._rootPlantedReplicator.SendMessage(position);
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
