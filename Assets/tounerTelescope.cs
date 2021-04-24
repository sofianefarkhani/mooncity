using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tounerTelescope : MonoBehaviour
{

    public bool rotating;
    public bool trigonometricRotation;
    float rotationSpeed;

    GameObject telescope;

    // Start is called before the first frame update
    void Start()
    {
        telescope = this.gameObject;
        rotating = false;
        trigonometricRotation = true;
        rotationSpeed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = telescope.transform.rotation.eulerAngles.y;
        float absoluteAngleBonus = Time.deltaTime * rotationSpeed;


        if (trigonometricRotation)
        {
            telescope.transform.Rotate(0, -absoluteAngleBonus, 0);
        }
        else
        {
            telescope.transform.Rotate(0, absoluteAngleBonus, 0);
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