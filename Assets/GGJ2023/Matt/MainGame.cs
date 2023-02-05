// Copyright 2022 Niantic, Inc. All Rights Reserved.

using System;

using Niantic.ARDK.AR;
using Niantic.ARDK.AR.ARSessionEventArgs;
using Niantic.ARDK.AR.HitTest;
using Niantic.ARDK.AR.Networking;
using Niantic.ARDK.AR.Networking.ARNetworkingEventArgs;
using Niantic.ARDK.Extensions;
using Niantic.ARDK.Networking;
using Niantic.ARDK.Networking.HLAPI;
using Niantic.ARDK.Networking.HLAPI.Authority;
using Niantic.ARDK.Networking.HLAPI.Data;
using Niantic.ARDK.Networking.HLAPI.Object;
using Niantic.ARDK.Networking.HLAPI.Object.Unity;
using Niantic.ARDK.Networking.HLAPI.Routing;
using Niantic.ARDK.Networking.MultipeerNetworkingEventArgs;
using Niantic.ARDK.Utilities;
using Niantic.ARDK.Utilities.Input.Legacy;

using UnityEngine;
using UnityEngine.UI;

// namespace Niantic.ARDKExamples.PongHLAPI
// {
  /// <summary>
  /// Controls the game logic and creation of objects
  /// </summary>
  public class MainGame:
    MonoBehaviour
  {


    [SerializeField]
    public NetworkedUnityObject rootPrefab = null;

    [SerializeField]
    public Canvas loading_screen_canvas = null;

    [SerializeField]
    public Canvas game_canvas = null;

    public TheMainBrain mainBrain = null;

    public Color myColor;


    public Text[] scoreElements;

    public Boolean peerConnected = false;




    /*

    /// Prefabs to be instantiated when the game starts
    [SerializeField]
    private NetworkedUnityObject playingFieldPrefab = null;

    [SerializeField]
    private NetworkedUnityObject ballPrefab = null;

    [SerializeField]
    private NetworkedUnityObject playerPrefab = null;

    */







    /// Reference to the StartGame button
    [SerializeField]
    // private GameObject startGame = null;

    // [SerializeField]
    private Button joinButton = null;

    [SerializeField]
    private FeaturePreloadManager preloadManager = null;

    /// Reference to AR Camera, used for hit test
    [SerializeField]
    public Camera _camera = null;

    /// References to game objects after instantiation
    private GameObject _ball;

    private GameObject _player;
    private GameObject _playingField;

    /// The score
    public Text score;

    /// HLAPI Networking objects
    private IHlapiSession _manager;

    public IAuthorityReplicator _auth;
    private MessageStreamReplicator<Vector3> _hitStreamReplicator;



    public MessageStreamReplicator<Vector3> _rootPlantedReplicator;




    private INetworkedField<string> _scoreText;
    private int _redScore;
    private int _blueScore;
    private INetworkedField<Vector3> _fieldPosition;
    private INetworkedField<byte> _gameStarted;

    /// Cache your location every frame
    private Vector3 _location;

    /// Some fields to provide a lockout upon hitting the ball, in case the hit message is not
    /// processed in a single frame
    private bool _recentlyHit = false;

    private int _hitLockout = 0;

    public IARNetworking _arNetworking;
    // private BallBehaviour _ballBehaviour;

    private bool _isHost;
    private IPeer _self;

    private bool _gameStart;
    private bool _synced;

    private void Start()
    {
      // startGame.SetActive(false);
      ARNetworkingFactory.ARNetworkingInitialized += OnAnyARNetworkingSessionInitialized;

      if (preloadManager.AreAllFeaturesDownloaded())
        OnPreloadFinished(true);
      else
        preloadManager.ProgressUpdated += PreloadProgressUpdated;
    }

    private void PreloadProgressUpdated(FeaturePreloadManager.PreloadProgressUpdatedArgs args)
    {
      if (args.PreloadAttemptFinished)
      {
        preloadManager.ProgressUpdated -= PreloadProgressUpdated;
        OnPreloadFinished(args.FailedPreloads.Count == 0);
      }
    }

    private void OnPreloadFinished(bool success)
    {
      if (success)
        joinButton.interactable = true;
      else
        Debug.LogError("Failed to download resources needed to run AR Multiplayer");
    }

    // When all players are ready, create the game. Only the host will have the option to call this
    public void StartGame()
    {

    }

    // Instantiate game objects
    public void InstantiateObjects(Vector3 position)
    {

    }

    internal void PlantRoot(Vector3 position)
    {
        TestRootBehavior behavior = rootPrefab.NetworkSpawn
        (
          _arNetworking.Networking,
          position,
          Quaternion.identity
        ).DefaultBehaviour as TestRootBehavior;
        behavior.Controller = this;
    }

    internal void RootPlanted(string ARDK_id, GameObject rootObject)
    {
      Debug.Log("A root was planted by: " + ARDK_id);
    }

    //
    internal void RootFinished(string ARDK_id, GameObject rootObject)
    {
      Debug.Log("A root finished for ARDK_id: " + ARDK_id);
      PlayerObject player = mainBrain.GetPlayerByARDKID(ARDK_id);
      player.score += 1;
      // int playerIndex = mainBrain.players.FindIndex(p => p == player);

      if (_self.Identifier.ToString() == ARDK_id) {
        scoreElements[0].text = player.score.ToString();
      } else {
        scoreElements[1].text = player.score.ToString();
      }
    }

    internal void RootDestroyed(string ARDK_id, string networkedPrefabID)
    {

    }
  
    private void Update()
    {

    }

    // Every updated frame, get our location from the frame data and move the local player's avatar
    private void OnFrameUpdated(FrameUpdatedArgs args)
    {
      _location = MatrixUtils.PositionFromMatrix(args.Frame.Camera.Transform);

      if (_player == null)
        return;

      var playerPos = _player.transform.position;
      playerPos.x = _location.x;
      _player.transform.position = playerPos;

      // todo for nametag or scorebar above head...
    }

    private void OnPeerStateReceived(PeerStateReceivedArgs args)
    {
      if (!peerConnected && _self.Identifier != args.Peer.Identifier) {
        PlayerObject player = mainBrain.CreatePlayer(args.Peer.Identifier.ToString());
        Debug.Log("Added player for ARDK_id:" + player.ARDK_id);
        peerConnected = true;
      }
      // if (_self.Identifier != args.Peer.Identifier)
      // {
      //   if (args.State == PeerState.Stable)
      //   {
      //     _synced = true;

      //     if (_isHost)
      //     {
      //       // startGame.SetActive(true);
      //       // InstantiateObjects(_location);
      //     }
      //     else
      //     {
      //       // InstantiateObjects();
      //     }
      //   }

      //   return;
      // }
      string message = args.State.ToString();
      score.text = message;
      Debug.Log("We reached state " + message);
    }

    private void OnDidConnect(ConnectedArgs connectedArgs)
    {

      loading_screen_canvas.gameObject.SetActive(false);
      game_canvas.gameObject.SetActive(true);

      _isHost = connectedArgs.IsHost;
      _self = connectedArgs.Self;

      _manager = new HlapiSession(19244);

      var group = _manager.CreateAndRegisterGroup(new NetworkId(4321));
      _auth = new GreedyAuthorityReplicator("pongHLAPIAuth", group);

      _auth.TryClaimRole(_isHost ? Role.Authority : Role.Observer, () => {}, () => {});

      var authToObserverDescriptor =
        _auth.AuthorityToObserverDescriptor(TransportType.ReliableUnordered);


      _scoreText = new NetworkedField<string>("scoreText", authToObserverDescriptor, group);
      _scoreText.ValueChanged += OnScoreDidChange;

      PlayerObject player = mainBrain.CreatePlayer(_self.Identifier.ToString());
      Debug.Log("Added player for my own ARDK_id:" + player.ARDK_id);

      // vgood example below \
      // _gameStarted = new NetworkedField<byte>("gameStarted", authToObserverDescriptor, group);

      // _gameStarted.ValueChanged +=
      //   value =>
      //   {
      //     _gameStart = Convert.ToBoolean(value.Value.Value);

          // if (_gameStart)
            // _ball = FindObjectOfType<BallBehaviour>().gameObject;
        // };

// TODO this is how can do destroys...
#pragma warning disable 0618
      _rootPlantedReplicator =
        new MessageStreamReplicator<Vector3>
        (
          "rootMessageStream",
          _arNetworking.Networking.AnyToAnyDescriptor(TransportType.ReliableOrdered),
          group
        );
#pragma warning restore 0618
      _rootPlantedReplicator.MessageReceived +=
        (args) =>
        {
          Debug.Log("A SEED WAS PLAN");
        };

    }

    private void OnScoreDidChange(NetworkedFieldValueChangedArgs<string> args)
    {
      score.text = args.Value.GetOrDefault();
    }

    private void OnAnyARNetworkingSessionInitialized(AnyARNetworkingInitializedArgs args)
    {
      _arNetworking = args.ARNetworking;
      _arNetworking.PeerStateReceived += OnPeerStateReceived;

      _arNetworking.ARSession.FrameUpdated += OnFrameUpdated;
      _arNetworking.Networking.Connected += OnDidConnect;
    }

    private void OnDestroy()
    {
      ARNetworkingFactory.ARNetworkingInitialized -= OnAnyARNetworkingSessionInitialized;

      if (_arNetworking != null)
      {
        _arNetworking.PeerStateReceived -= OnPeerStateReceived;
        _arNetworking.ARSession.FrameUpdated -= OnFrameUpdated;
        _arNetworking.Networking.Connected -= OnDidConnect;
      }
    }
  }
// }
