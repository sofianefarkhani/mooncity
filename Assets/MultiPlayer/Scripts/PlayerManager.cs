using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

using Photon.Pun;

/// <summary>
/// Player manager.
/// Handles fire Input and Beams.
/// </summary>
public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{
    #region IPunObservable implementation


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(Health);
        }
        else
        {
            // Network player, receive data
            this.Health = (float)stream.ReceiveNext();
        }

    }


    #endregion

    #region Public Fields

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    [Tooltip("The current Health of our player")]
    public float Health = 1f;
    #endregion


    #region Private Fields

    #endregion

    #region MonoBehaviour CallBacks


#if !UNITY_5_4_OR_NEWER
    /// <summary>See CalledOnLevelWasLoaded. Outdated in Unity 5.4.</summary>
    void OnLevelWasLoaded(int level)
    {
        this.CalledOnLevelWasLoaded(level);
    }
#endif

#if UNITY_5_4_OR_NEWER
    public override void OnDisable()
    {
        // Always call the base to remove callbacks
        base.OnDisable();
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }
#endif


    void CalledOnLevelWasLoaded(int level)
    {

    }



    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
    /// </summary>
    void Awake()
    {
        // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
        if (photonView.IsMine)
        {
            PlayerManager.LocalPlayerInstance = this.gameObject;
        }
        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(this.gameObject);

    }

    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during initialization phase.
    /// </summary>
    void Start()
    {

    }

    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity on every frame.
    /// </summary>
    void Update()
    {

    }

    #endregion

    #region Custom

    #endregion

    #region Private Methods

#if UNITY_5_4_OR_NEWER
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
    {
        this.CalledOnLevelWasLoaded(scene.buildIndex);
    }
#endif



    #endregion

}
