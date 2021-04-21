using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;
public class ElevatorDetection : MonoBehaviourPun
{
    private const float DefaultTime = 10; 
    private static float _timeBeforeElevate = DefaultTime;
    [SerializeField]
    private List<int> playersIn;
    // Start is called before the first frame update
    private void Start()
    {
        
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
            _timeBeforeElevate = DefaultTime;
            foreach (var player in playersIn.Where(player => PhotonNetwork.LocalPlayer.ActorNumber.Equals(player)))
            {
                SwitchingRoom.SwitchRoom();
            }
            playersIn.Clear();

        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(!other.GetType().IsEquivalentTo(typeof(CharacterController))) return;//On s'assure que OnTriggerEnter est trigger uniquement par le Collider lié au CharacterController
        _timeBeforeElevate = DefaultTime; 
        if (other.gameObject.GetPhotonView().IsMine) playersIn.Add(other.gameObject.GetPhotonView().Owner.ActorNumber);



    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.GetType().IsEquivalentTo(typeof(CharacterController))) return;//On s'assure que OnTriggerExit est trigger uniquement par le Collider lié au CharacterController
        if (other.gameObject.GetPhotonView().IsMine) playersIn.Remove(other.gameObject.GetPhotonView().Owner.ActorNumber);

    }
    
}
