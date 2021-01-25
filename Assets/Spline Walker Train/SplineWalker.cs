using Photon.Pun;
using UnityEngine;

public class SplineWalker : MonoBehaviourPunCallbacks
{

	public BezierSpline spline;

	public float duration;

	public bool lookForward;

	public SplineWalkerMode mode;

	public float progress;
	private bool goingForward = true;

	public bool stop = true;

	[PunRPC]
	public void stateButton()
	{
		stop = !stop;
		this.photonView.RPC("changeValue", RpcTarget.All, progress);
	}

	[PunRPC]
	public void changeValue(float aProgress)
	{
		this.progress = aProgress;
	}

	void Start()
	{
		progress = 0;
		Vector3 position = spline.GetPoint(progress);
		transform.localPosition = position;
		transform.LookAt(position + spline.GetDirection(progress));
	}

	/*
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{

		if (stream.IsWriting)
		{
			// We own this player: send the others our data
			stream.SendNext(progress);
		}
		else
		{
			// Network player, receive data
			this.progress = (float)stream.ReceiveNext();
		}

	}
	*/


	private void Update () {	

		if (!stop)
		{

			if (goingForward)
			{
				progress += (Time.deltaTime / duration);
				if (progress > 1f)
				{
					if (mode == SplineWalkerMode.Once)
					{
						progress = 1f;
					}
					else if (mode == SplineWalkerMode.Loop)
					{
						progress -= 1f;
					}
					else
					{
						progress = 2f - progress;
						goingForward = false;
					}
				}
			}
			else
			{
				progress -= Time.deltaTime / duration;
				if (progress < 0f)
				{
					progress = -progress;
					goingForward = true;
				}
			}
		}

		Vector3 position = spline.GetPoint(progress);
		transform.localPosition = position;
		if (lookForward)
		{
			transform.LookAt(position + spline.GetDirection(progress));
		}
		
		
	}
}