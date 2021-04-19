using Photon.Pun;
using UnityEngine;
public class ElevatorDetection : MonoBehaviour
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
        if (!_timerIsRunning) return;
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
            PhotonView photonView= PhotonView.Get(this);
            photonView.RPC("CallToSwitchRoom",RpcTarget.MasterClient);

        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(!other.GetType().IsEquivalentTo(typeof(CharacterController))) return;
        _timeBeforeElevate = DefaultTime;
        if(_numberOfPersonIn<=0)_timerIsRunning = true;
        _numberOfPersonIn++;
        


    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.GetType().IsEquivalentTo(typeof(CharacterController))) return;
        _numberOfPersonIn--;
        if (_numberOfPersonIn <= 0) _timerIsRunning = false;

    }

    [PunRPC]
    public void CallToSwitchRoom()
    {
        SwitchingRoom.SwitchRoom();
    } 
}
