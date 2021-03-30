using Photon.Pun;
using UnityEngine;

public class BodyTracking : MonoBehaviourPun
{
    private static float DefaultHeight =1.6f;// = 1.62 is the normal height of a human;
    public float rotationWhenCrouched;

    public GameObject Head;//these keep track of our body objects.
    public GameObject CameraRig;
    public GameObject BodyRoot;
    public GameObject HeadRoot;
    public GameObject Torso;
    public GameObject Hips;

    //add some new public varibles:
    public IK LeftArm;
    public IK RightArm;

    public Transform FeetRoot;
    private Vector3 PastPos;

    private Quaternion HeadRotation;//these are all offset varibles to make sure evrything stays in place.
    private Quaternion TorsoRotation;
    private Vector3 TorsoOffset;
    private Vector3 HipOffset;
    private Quaternion HipOffsetRot;
    private Quaternion TorsoOffsetRotation;
    private Quaternion HipOffsetRotation;

    void Awake()
    {
        HeadRotation = HeadRoot.transform.rotation;
        TorsoRotation = BodyRoot.transform.rotation;
        TorsoOffset = Torso.transform.position - HeadRoot.transform.position;
        HipOffset = Hips.transform.position - HeadRoot.transform.position;
        HipOffsetRot = Hips.transform.rotation;
        TorsoOffsetRotation = Torso.transform.rotation;
        HipOffsetRotation = Torso.transform.rotation;

    }

    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        //Add the following code right under this line of code:
        BodyRoot.transform.position = new Vector3(Head.transform.position.x, CameraRig.transform.position.y, Head.transform.position.z);

        //this code controlls the rotation of the hips, movment of the legs, and height of the feet.
        if ((BodyRoot.transform.position - PastPos).magnitude > .005f)
        {//If we have moved far enough
            FeetRoot.GetComponent<FootMovment>().HeightMultiplyer += .01f;//update foot position

            if (Quaternion.Angle(Quaternion.LookRotation(BodyRoot.transform.position - PastPos), Quaternion.Euler(0, Head.transform.rotation.eulerAngles.y, 0)) < 100)
            {//if we move forward 
                Hips.transform.rotation = Quaternion.RotateTowards(Hips.transform.rotation, Quaternion.Euler(0, Quaternion.LookRotation(BodyRoot.transform.position - PastPos).eulerAngles.y, 0) * HipOffsetRot, 3);
                FeetRoot.rotation = Quaternion.RotateTowards(FeetRoot.rotation, Quaternion.Euler(0, Quaternion.LookRotation(BodyRoot.transform.position - PastPos).eulerAngles.y, 0), 3);
                FeetRoot.GetComponent<FootMovment>().WalkM((BodyRoot.transform.position - PastPos).magnitude);
            }
            else
            {//if we move backwards 
                Hips.transform.rotation = Quaternion.RotateTowards(Hips.transform.rotation, Quaternion.Euler(0, Quaternion.LookRotation(-(BodyRoot.transform.position - PastPos)).eulerAngles.y, 0) * HipOffsetRot, 3);
                FeetRoot.rotation = Quaternion.RotateTowards(FeetRoot.rotation, Quaternion.Euler(0, Quaternion.LookRotation(-(BodyRoot.transform.position - PastPos)).eulerAngles.y, 0), 3);
                FeetRoot.GetComponent<FootMovment>().WalkM(-(BodyRoot.transform.position - PastPos).magnitude);
            }
        }
        else
        {
            Hips.transform.rotation = Quaternion.RotateTowards(Hips.transform.rotation, Quaternion.Euler(0, BodyRoot.transform.rotation.eulerAngles.y, 0) * HipOffsetRot, 2.5f);
            FeetRoot.rotation = Quaternion.RotateTowards(FeetRoot.rotation, Quaternion.Euler(0, BodyRoot.transform.rotation.eulerAngles.y, 0), 2);

            FeetRoot.GetComponent<FootMovment>().HeightMultiplyer -= .01f;//lower feet when not moving
        }

        PastPos = BodyRoot.transform.position;



        for (int i = 0; i < 5; i++)
        {
            if (Quaternion.Angle(Quaternion.Euler(0, BodyRoot.transform.rotation.eulerAngles.y, 0), Quaternion.Euler(0, Head.transform.rotation.eulerAngles.y, 0)) > 90)
            {
                BodyRoot.transform.rotation = Quaternion.RotateTowards(BodyRoot.transform.rotation, Quaternion.Euler(0, Head.transform.rotation.eulerAngles.y, 0) * TorsoRotation, 3);
            }
            else
            {
                if (RightArm.CantReach)
                {
                    BodyRoot.transform.rotation = Quaternion.RotateTowards(BodyRoot.transform.rotation, Quaternion.Euler(0, -90, 0) * Quaternion.Euler(0, Quaternion.FromToRotation(Vector3.forward, RightArm.Target.position - BodyRoot.transform.position).eulerAngles.y, 0) * TorsoRotation, 3);
                }
                if (LeftArm.CantReach)
                {
                    BodyRoot.transform.rotation = Quaternion.RotateTowards(BodyRoot.transform.rotation, Quaternion.Euler(0, 90, 0) * Quaternion.Euler(0, Quaternion.FromToRotation(Vector3.forward, LeftArm.Target.position - BodyRoot.transform.position).eulerAngles.y, 0) * TorsoRotation, 3);
                }
            }
            RightArm.UpdateIK();
            LeftArm.UpdateIK();
        }

        //And good luck understanding these, I barely understand my own math but it works. Explained Simply this makes you crouch when the headset gets close to the ground.
        Torso.transform.position = BodyRoot.transform.rotation * Quaternion.Euler((1 - (Head.transform.position.y - CameraRig.transform.position.y) / DefaultHeight) * rotationWhenCrouched, 0, 0) * (TorsoOffset) + Head.transform.position - (Quaternion.Euler(0, Head.transform.rotation.eulerAngles.y, 0) * TorsoRotation * Vector3.forward * (0.3f) * (FixEuler(Head.transform.rotation.eulerAngles.x) / 180));
        Hips.transform.position = BodyRoot.transform.rotation * Quaternion.Euler((1 - (Head.transform.position.y - CameraRig.transform.position.y) / DefaultHeight) * rotationWhenCrouched, 0, 0) * (HipOffset) + Head.transform.position - (Quaternion.Euler(0, Head.transform.rotation.eulerAngles.y, 0) * TorsoRotation * Vector3.forward * (0.3f) * (FixEuler(Head.transform.rotation.eulerAngles.x) / 180));
        Torso.transform.rotation = BodyRoot.transform.rotation * Quaternion.Euler((1 - (Head.transform.position.y - CameraRig.transform.position.y) / DefaultHeight) * rotationWhenCrouched, 0, 0) * TorsoOffsetRotation;

        HeadRoot.transform.position = Head.transform.position;
        HeadRoot.transform.rotation = Head.transform.rotation * HeadRotation;
    }

    float FixEuler(float angle)
    {
        if (angle < 180)
        {
            return angle;
        }
        else
        {
            return angle - 360;
        }
    }

}