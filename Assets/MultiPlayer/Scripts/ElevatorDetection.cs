using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
public class ElevatorDetection : MonoBehaviour
{
    private const float DefaultTime = 10; 
    private float _timeBeforeElevate = DefaultTime;
    private int _numberOfPersonIn = 0;
    private bool _timerIsRunning = false;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (_numberOfPersonIn <= 0) return;
        if (_timeBeforeElevate > 0)
        {
            _timeBeforeElevate -= Time.smoothDeltaTime;
            Debug.Log("Timer :"+_timeBeforeElevate);
            Debug.Log(_timerIsRunning);
        }
        else
        {
            Debug.Log("We are ready to go !");
            _timerIsRunning = false;
            _numberOfPersonIn = 0;
            _timeBeforeElevate = DefaultTime;
            if(PlayerManager.IsInOrOutOfElevator) SwitchingRoom.SwitchRoom();
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.GetType().IsEquivalentTo(typeof(CharacterController))) return;
        _timeBeforeElevate = DefaultTime;
        _numberOfPersonIn++;
        PlayerManager.IsInOrOutOfElevator = true;
        

    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.GetType().IsEquivalentTo(typeof(CharacterController))) return;
        _numberOfPersonIn--;
        PlayerManager.IsInOrOutOfElevator = false;

    }
}
