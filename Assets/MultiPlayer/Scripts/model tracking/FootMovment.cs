using Photon.Pun;
using UnityEngine;

public class FootMovment : MonoBehaviourPun
{
    public float StrideLength;
    public float StepHeight;
    public Transform LeftFoot;//set these to the foot target game objects we created earlyer  
    public Transform RightFoot;

    public float HeightMultiplyer;

    private float StridePosition;
    private Vector3 FootOffsetL;
    private Vector3 FootOffsetR;
    private float Length;

    void Start()
    {
        //sets the offset of our feet
        FootOffsetL = LeftFoot.localPosition;
        FootOffsetR = RightFoot.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        PositionFoot(LeftFoot, FootOffsetL, false);
        PositionFoot(RightFoot, FootOffsetR, true);

        UpdateHeight();

    }
    void PositionFoot(Transform Foot, Vector3 FootOffset, bool side)
    {

        float height;
        if (side)
        {
            height = Mathf.Sin(StridePosition * 2 * Mathf.PI) * StepHeight;

            if (fixStride(1 - StridePosition) < .5f) Length = (StrideLength / 2) - (StridePosition * 2) * StrideLength + (StrideLength / 2) + StrideLength / 2;
            else Length = ((StridePosition - .5f) * 2) * StrideLength + (StrideLength / 2);
            if (height < 0) height = 0;
        }
        else
        {
            height = -Mathf.Sin(StridePosition * 2 * Mathf.PI) * StepHeight;
            if (fixStride(StridePosition) > .5f) Length = ((StridePosition - .5f) * 2) * StrideLength - (StrideLength / 2);
            else Length = -(StridePosition * 2) * StrideLength + StrideLength / 2;
            if (height < 0) height = 0;
        }
        Foot.localPosition = FootOffset + new Vector3(0, height * HeightMultiplyer, Length);
    }
    float fixStride(float stride)
    {
        if (StridePosition > 1)
        {
            return StridePosition -= 1;
        }
        else if (StridePosition < 0)
        {
            return StridePosition += 1;
        }
        else
        {
            return stride;
        }
    }
    public void WalkM(float Meeters)//a method called to move the legs when you move.
    {
        StridePosition += Meeters / (StrideLength * 2);
        while (true)
        {
            if (StridePosition > 1)
            {
                StridePosition--;
            }
            else if (StridePosition < 0)
            {
                StridePosition++;
            }
            else
            {
                break;
            }
        }
    }
    private void UpdateHeight()//this function keeps the height multiplyer within a certan value.
    {
        if (HeightMultiplyer > 1)
        {
            HeightMultiplyer = 1;
        }
        else if (HeightMultiplyer < 0)
        {
            HeightMultiplyer = 0;
        }
    }
}