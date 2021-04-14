using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ElevatorDetection : MonoBehaviour
{
    private const float DefaultTime = 10; 
    private static float _timeBeforeElevate = DefaultTime;
    private int _numberOfPersonIn = 0;

    private bool _timerIsRunning = false;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_timerIsRunning || _numberOfPersonIn <= 0) return;
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
            _timeBeforeElevate = 0;
            SwitchingRoom.SwitchRoom();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_timerIsRunning)
        {
            Debug.Log("First person to enter !");
            _timerIsRunning = true;
            _numberOfPersonIn = 1;
        }
        else
        {
            _timeBeforeElevate = DefaultTime;
            _numberOfPersonIn++;
            Debug.Log("Another person is in ! Number Person in :"+_numberOfPersonIn);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (_numberOfPersonIn <= 0)
        {
            Debug.Log("There's nobody left inside !");
            _timerIsRunning = false;
        }
        else
        {
            _numberOfPersonIn--;
            Debug.Log("One person has left the elevator ! Number of person left : "+_numberOfPersonIn);
        }
        
    }
}
