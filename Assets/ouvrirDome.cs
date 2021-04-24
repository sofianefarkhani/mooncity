using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ouvrirDome : MonoBehaviour
{ 
    bool domeOpens;
    bool domeCloses;
    bool domeIsOpened;
    bool domeIsClosed;

    float initPos;
    float speedMultiplier;

    float maxOpeningValue; 
    float minOpeningValue;

    GameObject partieRotative;
    GameObject domeDroit;
    GameObject domeGauche;
    Vector3 center;

    // Start is called before the first frame update
    void Start()
    {
        domeOpens = false;
        domeCloses = false;
        domeIsOpened = false;
        domeIsClosed = true;

        partieRotative = GameObject.Find("observatoirefinal/partieRotative");
        domeDroit = GameObject.Find("observatoirefinal/partieRotative/dome_droit");
        domeGauche = GameObject.Find("observatoirefinal/partieRotative/dome_gauche");

        speedMultiplier = 1f;

        this.maxOpeningValue = 5;
        this.minOpeningValue = 0.1f;

        //the position of the center of rotation
        center = partieRotative.transform.position;
        initPos = domeGauche.transform.position.magnitude;
    }

    // Update is called once per frame
    void Update()
    {

        bool domeRotates = partieRotative.GetComponent<tournerObservatoire>().rotating;
        float rotationAngle = partieRotative.transform.rotation.eulerAngles.y;

        // print(rotationAngle);



        if (!domeRotates) //the dome cannot open and rotate at the same time
        {
            Vector3 leftHalf = domeGauche.transform.position;
            Vector3 relativePositionLeftHalf = leftHalf - center;

            float normL = relativePositionLeftHalf.magnitude;

            float magnitudeBonus = Time.deltaTime * speedMultiplier;
            float newAbsoluteX = magnitudeBonus * Mathf.Cos(rotationAngle * Mathf.PI / 180);
            float newAbsoluteZ = magnitudeBonus * Mathf.Sin(rotationAngle * Mathf.PI / 180);



            if (domeOpens)
            {
                domeIsClosed = false;
                if (normL < this.maxOpeningValue)
                {
                    domeGauche.transform.position += new Vector3(newAbsoluteX, 0, -newAbsoluteZ);
                    domeDroit.transform.position += new Vector3(-newAbsoluteX, 0, newAbsoluteZ);
                }
                else
                {
                    domeIsOpened = true;
                }
            }

            if (domeCloses)
            {
                domeIsOpened = false;
                if (normL > minOpeningValue)
                {
                    domeGauche.transform.position += new Vector3(-newAbsoluteX, 0, newAbsoluteZ);
                    domeDroit.transform.position += new Vector3(newAbsoluteX, 0, -newAbsoluteZ);
                }
                else
                {
                    domeIsClosed = true;
                }
            }
        }
    }

    void openDome()
    {
        this.domeCloses = false;
        this.domeOpens = true;
    }

    void closeDome()
    {
        this.domeOpens= false;
        this.domeCloses = true;
    }
}