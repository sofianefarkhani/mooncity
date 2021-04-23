using Photon.Pun;
using UnityEngine;
using Valve.VR;

//[ExecuteInEditMode]
public class HandControle : MonoBehaviourPun
{
    public Transform Index1;
    public Transform Index2;
    public Transform Index3;

    public Transform Middle1;
    public Transform Middle2;
    public Transform Middle3;

    public Transform Ring1;
    public Transform Ring2;
    public Transform Ring3;

    public Transform Pinky1;
    public Transform Pinky2;
    public Transform Pinky3;

    public Transform Thumb1;
    public Transform Thumb2;

    public SteamVR_Action_Boolean grabPinch = null;
    public SteamVR_Action_Boolean grapGrip = null;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        UpdateHand();
    }

    public void UpdateHand()
    {

        if (grabPinch.GetStateUp(SteamVR_Input_Sources.Any))
        {

            Index1.localEulerAngles = new Vector3(0, 0, 50);

            Debug.Log("Grip appuie ");

        }
        
        if(grabPinch.GetStateDown(SteamVR_Input_Sources.Any))
        {

            Index1.localEulerAngles = new Vector3(1.182f, 0, 1.339f);
            //Index2.localEulerAngles = new Vector3(0.081f, 0.056f, 1.283f);
            //Index3.localEulerAngles = new Vector3(-1.263f, 0, -2.622f);

            Thumb1.localEulerAngles = new Vector3(48.974f, 55.677f, 15.464f);
            //Thumb2.localEulerAngles = new Vector3(-39.064f, -61.531f, 35.433f);

        }

        if (grapGrip.GetStateUp(SteamVR_Input_Sources.Any))
        {

            Middle1.localEulerAngles = new Vector3(0, 0, 50);
            Ring1.localEulerAngles = new Vector3(0, 0, 50);
            Pinky1.localEulerAngles = new Vector3(0, 0, 50);

            Debug.Log("Pinch appuie ");

        }
        
        if(grapGrip.GetStateDown(SteamVR_Input_Sources.Any))
        {

            Middle1.localEulerAngles = new Vector3(1.182f, 0, 1.232f);
            //Middle2.localEulerAngles = new Vector3(0.074f, 0.051f, 1.181f);
            //Middle3.localEulerAngles = new Vector3(-1.256f, 0, -2.413f);

            Ring1.localEulerAngles = new Vector3(1.182f, 0, 1.416f);
            //Ring2.localEulerAngles = new Vector3(0.086f, 0.059f, 1.357f);
            //Ring3.localEulerAngles = new Vector3(-1.268f, 0, -2.773f);

            Pinky1.localEulerAngles = new Vector3(1.182f, 0, 1.778f);
            //Pinky2.localEulerAngles = new Vector3(0.11f, 0.075f, 1.703f);
            //Pinky3.localEulerAngles = new Vector3(-1.292f, 0, -3.482f);

            Thumb1.localEulerAngles = new Vector3(48.974f, 55.677f, 15.464f);
            //Thumb2.localEulerAngles = new Vector3(-39.064f, -61.531f, 35.433f);

        }


    }

}