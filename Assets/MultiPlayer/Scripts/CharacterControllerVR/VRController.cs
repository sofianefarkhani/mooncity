using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Photon.Pun;

public class VRController : MonoBehaviourPun
{
    public float sens = 0.1f;
    public float maxSpeed = 6.0f;
    public float gravity = 30.0f;

    public SteamVR_Action_Boolean jumpPress = null;
    public SteamVR_Action_Vector2 moveValue = null;


    public SteamVR_Action_Boolean snapRight = null;
    public SteamVR_Action_Boolean snapLeft = null;

    /*
    public SteamVR_Action_Boolean buttonA = null;
    public SteamVR_Action_Boolean buttonB = null;

    public SteamVR_Action_Boolean buttonX = null;
    public SteamVR_Action_Boolean buttonY = null;
    */

    public float speed = 3.0f;

    private CharacterController characterController = null;
    private Transform cameraRig = null;
    private Transform head = null;

    public Transform groundCheck;
    public float groundDistance =0;
    public static bool isGrounded = false;
    public static bool isSolid = false;
    public LayerMask groundMask;
    public LayerMask solidMask;

    public GameObject cam;
    public GameObject ctl;
    public GameObject ctr;

    [SerializeField]
    private float directionDampTime = 0.25f;
    Animator animator;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        cameraRig = SteamVR_Render.Top().origin;
        head = SteamVR_Render.Top().head;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            cam.SetActive(false);
            ctl.SetActive(false);
            ctr.SetActive(false);
            return;
        }

        if (photonView.IsMine == true)
        {
            cam.SetActive(true);
            ctl.SetActive(true);
            ctr.SetActive(true);
        }

        HandleControlers();
        HandleHead();
        HandleHeight();
        CalculateMov();
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isSolid = Physics.CheckSphere(groundCheck.position, groundDistance, solidMask);

    }

    private void HandleControlers()
    {
        
        if (snapLeft.GetStateUp(SteamVR_Input_Sources.Any))
        {
            transform.eulerAngles -= new Vector3(0.0f, 36, 0.0f);
            Debug.Log("Snap turn left;");
        }
        else if (snapRight.GetStateUp(SteamVR_Input_Sources.Any))
        {
            transform.eulerAngles += new Vector3(0.0f, 36, 0.0f);
            Debug.Log("Snap turn right;");
        }



        /*
        if (buttonA.GetStateUp(SteamVR_Input_Sources.Any))
        {
            Debug.Log("Button A pressed;");
        }
        if (buttonB.GetStateUp(SteamVR_Input_Sources.Any))
        {
            Debug.Log("Button B pressed;");
        }
        if (buttonX.GetStateUp(SteamVR_Input_Sources.Any))
        {
            Debug.Log("Button X pressed;");
        }
        if (buttonY.GetStateUp(SteamVR_Input_Sources.Any))
        {
            Debug.Log("Button Y pressed;");
        }
        */

    }

    private void HandleHead()
    {
        //The rig should not move along the camera rotations
        //Store current
        Vector3 oldPosition = cameraRig.position;
        Quaternion oldRotation = cameraRig.rotation;

        //Rotarion
        transform.eulerAngles = new Vector3(0.0f, head.rotation.eulerAngles.y, 0.0f);

        //restore
        cameraRig.position = oldPosition;
        cameraRig.rotation = oldRotation;
    }

    private void CalculateMov()
    {
        //Figure out movement orientation
        Quaternion orientation = Calculateorientation();
        Vector3 movement = Vector3.zero;

        //If not moving
        if (moveValue.axis.magnitude == 0)
            speed = 0;

        // Add, clamp
        speed += moveValue.axis.magnitude * sens;
        speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);

        //Orientation
        movement += orientation * (speed * Vector3.forward);

        

        //Jump
        
        if (jumpPress.GetStateUp(SteamVR_Input_Sources.Any) && VRController.isSolid == true)
        {
            Debug.Log("jump");
            StartCoroutine(EnableJump());
        }
        

        //Gravity
        movement.y -= gravity * Time.deltaTime;


        //Apply
        characterController.Move(movement * Time.deltaTime);



        // set the Animator Parameters
        //animator.SetFloat("Speed", speed);
        //animator.SetFloat("Direction", moveValue.axis.x, directionDampTime, Time.deltaTime);

    }
    
    IEnumerator EnableJump()
    {
        gravity = -gravity * 1.5f;
        yield return new WaitForSeconds(0.5f);
        gravity = -gravity / 1.5f;
        gravity = -gravity;
        yield return new WaitForSeconds(0.5f);
        gravity = -gravity;
        gravity = -gravity/2;
        yield return new WaitForSeconds(0.3f);
        gravity = -gravity * 2;

    }
    

    private void HandleHeight()
    {
        //Get the head in the local space
        float headheight = Mathf.Clamp(head.localPosition.y, 1, 2);
        characterController.height = headheight;

        //Cut in half
        Vector3 newcenter = Vector3.zero;
        newcenter.y = characterController.height/2;
        newcenter.y += characterController.skinWidth;

        //Move capsule in local space
        newcenter.x = head.localPosition.x;
        newcenter.z = head.localPosition.z;

        //Rotate
        newcenter = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newcenter;

        //Apply
        characterController.center = newcenter;
    }

    private Quaternion Calculateorientation()
    {
        float rotation = Mathf.Atan2(moveValue.axis.x, moveValue.axis.y);
        rotation *= Mathf.Rad2Deg;

        Vector3 orientationEuler = new Vector3(0, head.eulerAngles.y + rotation, 0);
        return Quaternion.Euler(orientationEuler);

    }
}