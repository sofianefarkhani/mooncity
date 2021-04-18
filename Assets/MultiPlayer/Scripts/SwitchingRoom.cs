using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class SwitchingRoom : MonoBehaviourPunCallbacks
{
    #region Private Serializable Fields

    /// <summary>
    /// The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created.
    /// </summary>
    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;
    [Tooltip("The name of the room to go, can be a new one or an existing one")]
    [SerializeField]
    private string nameRoom;
    
    [Tooltip("The name of the scene to load in the next room.")] [SerializeField]
    private string nameRoomScene;
    #endregion
    
    #region Private Fields

    
    
    /// <summary>
    /// Keep track of the current process. Since connection is asynchronous and is based on several callbacks from Photon,
    /// we need to keep track of this to properly adjust the behavior when we receive call back by Photon.
    /// Typically this is used for the OnConnectedToMaster() callback.
    /// </summary>
    private static bool _isConnecting;
    

    #endregion
    
    #region MonoBehaviour CallBacks
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    #endregion

    #region Public Methods


    /// <summary>
    /// The idea here, it's when you leave a room, you indicate to photon that you still want to connect to a room.
    /// So what when the player get back to the Master Server, it will connecting him to the other room by the room's name.
    /// </summary>
    public static void SwitchRoom()
    {
        
        _isConnecting=PhotonNetwork.LeaveRoom();

    }


    #endregion

    #region MonoBehaviourPunCallbacks Callbacks


    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");

        // we don't want to do anything if we are not attempting to join a room.
        // this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
        // we don't want to do anything.
        if (_isConnecting)
        {
            // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
            PhotonNetwork.JoinRoom(nameRoom);
            _isConnecting = false;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        _isConnecting = false;
        
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(nameRoom, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");

        // #Critical: We only load if we are the first player, else we rely on `PhotonNetwork.AutomaticallySyncScene` to sync our instance scene.
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("We load the "+nameRoom+" room");


            // #Critical
            // Load the Room Level.
            PhotonNetwork.LoadLevel(nameRoomScene);
        }
            
            
    }
    

    #endregion

}
