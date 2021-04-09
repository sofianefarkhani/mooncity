using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class ButtonV : MonoBehaviourPun
{
    [System.Serializable]
    public class ButtonEvent : UnityEvent { }

    public float pressLength;
    public bool triggerPress;
    public ButtonEvent downEvent;
    private bool pressed;
    Vector3 startPos;
    Rigidbody rb;

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // If our distance is greater than what we specified as a press
        // set it to our max distance and register a press if we haven't already
        var position = transform.position;
        float distance = Mathf.Abs(position.y - startPos.y);
        if(triggerPress) this.photonView.RPC("Click",RpcTarget.All);
        if (distance >= pressLength)
        {
            // Prevent the button from going past the pressLength

            position = new Vector3(position.x, startPos.y - pressLength, position.z);
            transform.position = position;
            if (!pressed)
            {

                this.photonView.RPC("Click", RpcTarget.All);

            }
        }
        else
        {
            // If we aren't all the way down, reset our press
            pressed = false;
        }
        // Prevent button from springing back up past its original position
        if (position.y > startPos.y)
        {
            position = new Vector3(position.x, startPos.y, position.z);
            transform.position = position;
        }
    }

    [PunRPC]
    public void Click()
    {
        triggerPress = false;
        pressed = true;
        // If we have an event, invoke it
        downEvent?.Invoke();
    }
}