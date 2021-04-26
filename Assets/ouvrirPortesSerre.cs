using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ouvrirPortesSerre : MonoBehaviour
{
    public bool opening;
    public bool closing;

    GameObject doorL;
    GameObject doorR;
    
    float maxOp = 0f;
    float minOp = -35f;
    float speed = 1f;


    // Start is called before the first frame update
    void Start()
    {
        doorL = GameObject.Find("doorLSerre");
        doorR = GameObject.Find("doorRSerre");
        closing = true;
        opening = false;
    }

    // Update is called once per frame
    void Update()
    {
        float leftDoorX = doorL.transform.localPosition.x;
        print("OPENING: "+leftDoorX);

        if (opening)
        {
            print((new Vector3(-speed, 0, 0)));
            doorL.transform.localPosition += new Vector3(speed, 0, 0);
            doorR.transform.localPosition += new Vector3(-speed, 0, 0);
            if (leftDoorX + speed > maxOp)
            {
                opening = false;
            }
        }
        if (closing)
        {
            doorL.transform.localPosition += new Vector3(-speed, 0, 0);
            doorR.transform.localPosition += new Vector3(speed, 0, 0);
            if (leftDoorX - speed < minOp)
            {
                closing = false;
            }
        }

    }
}
