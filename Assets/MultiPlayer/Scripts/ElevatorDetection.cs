using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;
public class ElevatorDetection : MonoBehaviourPun
{
    [SerializeField]
    private float defaultTime = 10; 
    private static float _timeBeforeElevate;
    [SerializeField]
    private List<int> playersIn;
    // Start is called before the first frame update
    private void Start()
    {
        _timeBeforeElevate = defaultTime;
    }

    // Update is called once per frame
    private void Update()
    {
        if (playersIn.Count==0) return;
        if (_timeBeforeElevate > 0)
        {
            _timeBeforeElevate -= Time.smoothDeltaTime;
            Debug.Log("Timer :"+_timeBeforeElevate);
        }
        else
        {
            Debug.Log("We are ready to go !");
            _timeBeforeElevate = defaultTime;
            foreach (var player in playersIn.Where(player => PhotonNetwork.LocalPlayer.ActorNumber.Equals(player)))
            {
                SwitchingRoom.SwitchRoom(false);
            }
            playersIn.Clear();

        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(!other.GetType().IsEquivalentTo(typeof(CharacterController))) return;//On s'assure que OnTriggerEnter est trigger uniquement par le Collider lié au CharacterController
        _timeBeforeElevate = defaultTime; 
        if (other.gameObject.GetPhotonView().IsMine) playersIn.Add(other.gameObject.GetPhotonView().Owner.ActorNumber);



    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.GetType().IsEquivalentTo(typeof(CharacterController))) return;//On s'assure que OnTriggerExit est trigger uniquement par le Collider lié au CharacterController
        if (other.gameObject.GetPhotonView().IsMine) playersIn.Remove(other.gameObject.GetPhotonView().Owner.ActorNumber);

    }
    
}
