using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tournerObservatoire : MonoBehaviour
{
    public bool rotating;
    public bool trigonometricRotation;
    float rotationSpeed;
    
    GameObject top;
    
    // Start is called before the first frame update
    void Start()
    {
        top = GameObject.Find("observatoirefinal/partieRotative");
        rotating = false;
        trigonometricRotation = true;
        rotationSpeed = 10 * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotating)
        {
            if (trigonometricRotation)
            {
                top.transform.Rotate(0, this.rotationSpeed, 0);
            }
            else
            {
                top.transform.Rotate(0, -this.rotationSpeed, 0);
            }
        }



    }

    //GETTERS SETTERS

    void setRotating(bool rotates)
    {
        this.rotating = rotates;
    }

    void setSenseOfRotationToTrigo(bool trigoRotation)
    {
        this.trigonometricRotation = trigoRotation;
    }

}
