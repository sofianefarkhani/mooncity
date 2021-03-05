using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TV_controller : MonoBehaviour
{

    public VideoPlayer ekrant;
    private float economisateurDekrant = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    private void OnTriggerEnter(Collider other)
    {
        ekrant.playbackSpeed = 1;
    }

    private void OnTriggerExit(Collider other)
    {
        ekrant.playbackSpeed = 0;
    }
}
