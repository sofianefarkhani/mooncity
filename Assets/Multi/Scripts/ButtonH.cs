using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ButtonH : MonoBehaviourPun
{
    [System.Serializable]
    public class ButtonEvent : UnityEvent { }

    public float pressLength;
    public bool pressed;
    public ButtonEvent downEvent;

    Vector3 startPos;
    Rigidbody rb;

    private bool isOpening = false;

    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // If our distance is greater than what we specified as a press
        // set it to our max distance and register a press if we haven't already
        float distance = Vector3.Distance(transform.position, startPos);
        if (!isOpening) { 
        if (distance >= pressLength)
        {
            // Prevent the button from going past the pressLength
            if (!pressed)
            {

                this.photonView.RPC("Click", RpcTarget.All);

            }
        }
        else
        {
            // If we aren't all the way down, reset our press
            pressed = false;
            this.GetComponent<Collider>().enabled = true;
        }
        }
    }

    [PunRPC]
    public void Click()
    {
        this.GetComponent<Collider>().enabled = false;
        pressed = true;
        isOpening = true;
        // If we have an event, invoke it        
        downEvent?.Invoke();
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(3.0f);
        isOpening = false;
    }
}