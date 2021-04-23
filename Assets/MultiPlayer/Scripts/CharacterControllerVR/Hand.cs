using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour
{
    public SteamVR_Action_Boolean grabAction = null;

    private SteamVR_Behaviour_Pose pose = null;
    private FixedJoint joint = null;

    private Interact currentInteract = null;
    public List<Interact> contactInteract = new List<Interact>();

    private void Awake()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        joint = GetComponent<FixedJoint>();
    }

    private void Update()
    {
        //Down
        if (grabAction.GetStateDown(pose.inputSource))
        {
            print(pose.inputSource + "Trigger down");
            Pickup();
        }

        //Up
        if (grabAction.GetStateUp(pose.inputSource))
        {
            print(pose.inputSource + "Trigger up");
            Drop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Interact"))
            return;
        contactInteract.Add(other.gameObject.GetComponent<Interact>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Interact"))
            return;
        contactInteract.Remove(other.gameObject.GetComponent<Interact>());
    }

    public void Pickup()
    {
        //Get the nearest interact
        currentInteract = GetNearestInteract();

        //Null check
        if (!currentInteract)
            return;

        //Already held ? check
        if (currentInteract.activehand)
            currentInteract.activehand.Drop();

        //Posiiton
        currentInteract.transform.position = transform.position;

        //Attach fixed joint
        Rigidbody targetbody = currentInteract.GetComponent<Rigidbody>();
        joint.connectedBody = targetbody;

        //Set attive hand
        currentInteract.activehand = this;
    }

    public void Drop()
    {
        //Null check
        if (!currentInteract)
            return;

        //Apply velocity
        Rigidbody targetBody = currentInteract.GetComponent<Rigidbody>();
        targetBody.velocity = pose.GetVelocity();
        targetBody.angularVelocity = pose.GetAngularVelocity();

        //Detach
        joint.connectedBody = null;

        //clear
        currentInteract.activehand = null;
        currentInteract = null;
    }

    private Interact GetNearestInteract()
    {
        Interact nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach (Interact interact in contactInteract)
        {
            distance = (interact.transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = interact;
            }
        }

        return nearest;
    }
}
