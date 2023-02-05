// Copyright 2022 Niantic, Inc. All Rights Reserved.

using System;

using Niantic.ARDK.Networking;
using Niantic.ARDK.Networking.HLAPI.Data;
using Niantic.ARDK.Networking.HLAPI.Object.Unity;

using UnityEngine;

// namespace Niantic.ARDKExamples.PongHLAPI
// {
  /// <summary>
  /// Class that handles the ball's behaviour
  /// Only the host can affect the ball's properties, all other players must listen
  /// </summary>
  [RequireComponent(typeof(AuthBehaviour))]
  public class TestRootBehavior: NetworkedBehaviour
  {
    internal MainGame Controller = null;

    // Cache the floor level
    private Vector3 _initialPosition;

    private Color theColor;

    // Flags for whether the game has started and if the local player is the host
    private bool _gameStart;
    private bool _isHost;

    private PlayerObject ownerPlayer = null;
    
    // Store the start location of the ball
    private void Start()
    {
      // Controller.RootPlanted(Owner.Auth.Identifier.ToString(), gameObject);
      // Debug.Log("Start root prefab...");
      // _initialPosition = transform.position;
      Debug.Log("Trying to find player: " + Owner.SpawningPeer.Identifier.ToString());
      // Controller.gameObject.TryGetComponent<TheMainBrain>(out mainBrain);
      // if (mainBrain) {
      //   Debug.Log("MainBrain found...");
      //   PlayerObject ownerPlayer = mainBrain.GetPlayerByARDKID("abcdefg");
      ownerPlayer = Controller.mainBrain.GetPlayerByARDKID(Owner.SpawningPeer.Identifier.ToString());
      //   // PlayerObject ownerPlayer = Controller.gameObject.GetComponentInChildren<TheMainBrain>().GetPlayerByARDKID(Owner.Auth.Identifier.ToString());
      if (ownerPlayer != null) {
        Debug.Log("got player..");
        theColor = ownerPlayer.color;
        Debug.Log("The color of the tree should be: " + theColor.ToString());
      } else {
        Debug.Log("owner player not found..");
      }
      // } else {
      //   Debug.Log("mainBrain was null..");
      // }
    }
    private void Update()
    {

    }

    protected override void SetupSession(out Action initializer, out int order)
    {
      initializer = () =>
      {
        var auth = Owner.Auth;
        var descriptor = auth.AuthorityToObserverDescriptor(TransportType.UnreliableUnordered);

        new UnreliableBroadcastTransformPacker
        (
          "netTransform",
          transform,
          descriptor,
          TransformPiece.Position,
          Owner.Group
        );
      };

      order = 0;
    }
  }
// }
