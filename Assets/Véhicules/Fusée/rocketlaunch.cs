using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class rocketlaunch : MonoBehaviour
{
    public float dist;
    public ParticleSystem ps1;
    public ParticleSystem ps2;
    public Light l;
    
    bool trig = true;
    float gamma = 0.4f;
    float speed = 0 ;
    // Start is called before the first frame update
    void Start()
    {
       
        //GameObject rocket = new GameObject();
        print("Fusée : "+gameObject.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        

        if(trig == true )
        {
            if (speed <= 10000)
            {
                speed += gamma + Time.deltaTime;
            }
           
            
            transform.position += new Vector3(0, Time.deltaTime * speed, 0);
            if (transform.position.y >= dist )
            {
                trig = false;
                Destroy(gameObject);
                
            }
        }
    }

    public void go()
    {
        trig = true;
        ps1.Play();
        ps2.Play();
        l.intensity = 100;

    }
}
