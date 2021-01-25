using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandAnimator : MonoBehaviour
{
    public SteamVR_Action_Single grabAction = null;

    private Animator animator = null;
    private SteamVR_Behaviour_Pose pose = null;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        pose = GetComponentInParent<SteamVR_Behaviour_Pose>();

        grabAction[pose.inputSource].onChange += Grab;
    }

    private void OnDestroy()
    {
        grabAction[pose.inputSource].onChange -= Grab;
    }

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("Point", true);
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("Point", false);
    }

    private void Grab(SteamVR_Action_Single action, SteamVR_Input_Sources source, float axis, float delta)
    {
        animator.SetFloat("GrabBlend", axis);
    }
}
