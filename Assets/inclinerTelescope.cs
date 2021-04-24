using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inclinerTelescope : MonoBehaviour
{
    GameObject upperPart;
    bool pointUpwards;
    bool pointDownwards;

    float higherInclination;
    float lowerInclination;
    float midPoint;

    // Start is called before the first frame update
    void Start()
    {
        upperPart = this.gameObject;
        pointDownwards = false;
        pointUpwards = false;

        higherInclination = 335;
        lowerInclination = 50;
        midPoint = 180;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = upperPart.transform.rotation.eulerAngles.x;

        float speed = 10f;
        float absoluteAngleBonus = Time.deltaTime * speed;

        //butées limitant le mouvement du telescope
        if (angle > midPoint && angle < higherInclination)
        {
            pointUpwards = false;
            pointDownwards = true;
        }
        if (angle < midPoint && angle > lowerInclination)
        {
            pointUpwards = true;
            pointDownwards = false;
        }


        //mouvement
        if (pointUpwards)
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
