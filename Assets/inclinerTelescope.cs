using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inclinerTelescope : MonoBehaviour
{
    public bool pointUpwards;
    public bool pointDownwards;

    float higherInclination;
    float lowerInclination;
    float midPoint;
    GameObject upperPart;

    // Start is called before the first frame update
    void Start()
    {
        upperPart = this.gameObject;
        pointDownwards = false;
        pointUpwards = false;

        higherInclination = 60;
        lowerInclination = 300;
        midPoint = 180;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = upperPart.transform.rotation.eulerAngles.x;

        

        float speed = 10f;
        float absoluteAngleBonus = Time.deltaTime * speed;


        
        //mouvement
        if (pointUpwards)//&& angle-absoluteAngleBonus>this.higherInclination
        {
            upperPart.transform.Rotate(-absoluteAngleBonus, 0, 0);
            
            
        }
        else if (pointDownwards)
        {
            upperPart.transform.Rotate(absoluteAngleBonus, 0, 0);
        }
    }

    //GETTERS SETTERS
    void setPointUpwards(bool turnUp)
    {
        this.pointUpwards = turnUp;
    }

    void setSenseOfRotationToTrigo(bool turnDown)
    {
        this.pointDownwards = turnDown;
    }
}
