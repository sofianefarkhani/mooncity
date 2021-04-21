using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;
public class ElevatorDetection : MonoBehaviourPun
{
    private const float DefaultTime = 10; 
    private static float _timeBeforeElevate = DefaultTime;
    private static int _numberOfPersonIn;
    [SerializeField]
    private bool _timerIsRunning;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_timerIsRunning ) return;
        if (_timeBeforeElevate > 0)
        {
            _timeBeforeElevate -= Time.smoothDeltaTime;
            Debug.Log("Timer :"+_timeBeforeElevate);
        }
        else
        {
            Debug.Log("We are ready to go !");
            _timerIsRunning = false;
            _numberOfPersonIn = 0;
            _timeBeforeElevate = DefaultTime;
            SwitchingRoom.SwitchRoom();
            

        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(!other.GetType().IsEquivalentTo(typeof(CharacterController))) return;
        if(other.gameObject.GetPhotonView().IsMine)Debug.Log(PhotonNetwork.LocalPlayer.NickName+"entered the elevator");
        

    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.GetType().IsEquivalentTo(typeof(CharacterController))) return;
        if(PlayerManager.LocalPlayerInstance.gameObject.Equals(other.gameObject)) Debug.Log(PlayerManager.LocalPlayerInstance.name+"exited the elevator");

    }
    
}
