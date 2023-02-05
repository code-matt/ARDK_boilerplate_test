// Copyright 2022 Niantic, Inc. All Rights Reserved.

using System;

using Niantic.ARDK.Networking;
using Niantic.ARDK.Networking.HLAPI.Data;
using Niantic.ARDK.Networking.HLAPI.Object.Unity;

using UnityEngine;

namespace Niantic.ARDKExamples.PongHLAPI
{
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
    
    // Store the start location of the ball
    private void Start()
    {
      _initialPosition = transform.position;
      theColor = Controller.mainBrain.GetPlayerByARDKID(Owner.Auth.Identifier.ToString()).color;
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
}
