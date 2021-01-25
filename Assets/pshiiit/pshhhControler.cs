using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pshhhControler : MonoBehaviour
{

    public AudioSource aus;
    public ParticleSystem ps;

    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("n")) //test
        {
            depressur();
        }
    }
    */


    void depressur()
    {
        aus.Play();
        ps.Play();
    }

}
